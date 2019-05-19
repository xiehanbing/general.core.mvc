using System;
using System.Linq;
using General.Core.Librs;
using General.Entity.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace General.Services.User
{
    public class SysUserService : ISysUserService
    {
        private readonly Core.Data.IRepository<Entity.User.SysUser> _repository;
        private readonly Core.Data.IRepository<Entity.User.SysUserToken> _sysusertokenRepository;
        private IMemoryCache _memoryCache;
        private const string MODEL_KEY = "General.services.users.{0}";
        private const string MODEL_KEY_TOKEN = "General.services.users.tokens.{0}";
        public SysUserService(Core.Data.IRepository<Entity.User.SysUser> repository, IMemoryCache memoryCache, Core.Data.IRepository<Entity.User.SysUserToken> sysusertokenRepository)
        {
            _repository = repository;
            _memoryCache = memoryCache;
            _sysusertokenRepository = sysusertokenRepository;
        }
        /// <summary>
        /// <see cref="ISysUserService.ValidateUser(string,string,string)"/>
        /// </summary>
        public (bool success, string message, string token, Entity.User.SysUser user) ValidateUser(string account, string password, string r)
        {
            var user = GetByAccount(account);
            if (user == null)
                return (false, "用户名或密码错误", null, null);
            if (user.Enable)
            {
                return (false, "账号已被冻结", null, null);
            }
            if (user.LoginLock)
            {
                if (user.AllowLoginTime != null && user.AllowLoginTime > DateTime.Now)
                {
                    return (false, "账号已被锁定" + ((user.AllowLoginTime.Value - DateTime.Now).TotalMinutes + 1).ToString("F0") + "分钟", null, null);
                }
            }
            var md5password = EncryptorHelper.GetMd5(user.Password + r);
            if (password.Equals(md5password, StringComparison.InvariantCultureIgnoreCase))
            {
                user.LoginLock = false;
                user.LoginFailedNum = 0;
                user.AllowLoginTime = null;
                user.Enable = false;
                user.LastLoginTime = DateTime.Now;
                //登录日志记录
                user.SysUserLoginLogs.Add(new SysUserLoginLog()
                {
                    Id = Guid.NewGuid(),
                    IpAddress = "",
                    LoginTime = DateTime.Now,
                    Message = "登录成功",
                    UserId = user.UserGuid
                });
                //单点登录，移除旧的登录token

                var userToken = new SysUserToken()
                {
                    Id = Guid.NewGuid(),
                    ExpireTime = DateTime.Now.AddHours(1),
                    SysUserId = user.UserGuid

                };
                user.SysUserToken.Add(userToken);
                _repository.DbContext.SaveChanges();
                return (true, "登录成功", userToken.Id.ToString(), user);
            }
            //登录日志记录
            user.SysUserLoginLogs.Add(new SysUserLoginLog()
            {
                Id = Guid.NewGuid(),
                IpAddress = "",
                LoginTime = DateTime.Now,
                Message = "登录密码错误",
                UserId = user.UserGuid
            });

            user.LoginFailedNum++;
            if (user.LoginFailedNum > 5)
            {
                user.LoginLock = true;
                user.AllowLoginTime = DateTime.Now.AddHours(2);

            }
            _repository.DbContext.SaveChanges();
            return (false, "用户名或密码错误", null, null);
        }

        public Entity.User.SysUser GetByAccount(string account)
        {
            return _repository.Table.FirstOrDefault(o => o.Account.Equals(account) && !o.IsDeleted);
        }

        public bool Add(Entity.User.SysUser user)
        {
            return _repository.Insert(user) != null;
        }

        public Entity.User.SysUser GetLogged(string token)
        {
            if (!Guid.TryParse(token, out Guid tokenId))
            {
                return null;
            }
            //先查 用户token 对应的缓存 
            if (_memoryCache.TryGetValue<Guid>(string.Format(MODEL_KEY_TOKEN, tokenId.ToString()),
                out Guid userId))
            {
                //再查用户id 对应的用哪个户信息
                if (_memoryCache.TryGetValue<Entity.User.SysUser>(string.Format(MODEL_KEY, userId),
                    out Entity.User.SysUser userInfo))
                {
                    return userInfo;
                }
            }

            var tokenInfo = _sysusertokenRepository.Table.Include(x => x.SysUser).FirstOrDefault(o => o.Id.Equals(tokenId));
            if (tokenInfo == null)
            {
                return null;
            }
            //缓存
            _memoryCache.Set(string.Format(MODEL_KEY, tokenInfo.SysUserId), tokenInfo.SysUser,
                DateTimeOffset.Now.AddHours(1));
            _memoryCache.Set(string.Format(MODEL_KEY_TOKEN,tokenId), tokenInfo.SysUserId,
                DateTimeOffset.Now.AddHours(1));

            return tokenInfo.SysUser;
        }
    }
}