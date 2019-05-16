﻿using System.Linq;
using General.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace General.Entity
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly GeneralDbContext _dbContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public EfRepository(GeneralDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// dbcontext
        /// </summary>
        public DbContext DbContext => _dbContext;
        /// <summary>
        /// dbset
        /// </summary>
        public DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

        /// <summary>
        /// 只做查询
        /// </summary>
        public IQueryable<TEntity> Table => Entities;
        /// <summary>
        /// 查询 根据id主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(object id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }
        /// <summary>
        /// 插入 返回 插入的实体对象
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        public object Insert(TEntity entity, bool isSave = true)
        {
            var data = Entities.Add(entity);
            if (isSave)
            {
                _dbContext.SaveChanges();
            }

            return data;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        public int Update(TEntity entity, bool isSave = true)
        {
            var success = 0;
            if (isSave)
            {
                success = _dbContext.SaveChanges();
            }
            return success;
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        public int Delete(TEntity entity, bool isSave = true)
        {
            Entities.Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}