using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace General.Core.Data
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// db 上下文
        /// </summary>
        DbContext DbContext { get; }
        /// <summary>
        /// dbset 实体
        /// </summary>
        DbSet<TEntity> Entities { get; }

        IQueryable<TEntity> Table { get; }

        TEntity GetById(object id);

        object Insert(TEntity entity, bool isSave = true);

        int Update(TEntity entity, bool isSave = true);

        int Delete(TEntity entity, bool isSave = true);
    }
}