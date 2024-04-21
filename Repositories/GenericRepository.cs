using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAppManager.Models;
using WebAppManager.Repositories.Interfaces;

namespace WebAppManager.Repositories
{
    public class GenericRepository<TEntity>(WebappmanagerContext context) : IGenericRepository<TEntity> where TEntity : BaseEntities
    {
        #region Private Fields

        private readonly WebappmanagerContext _context = context;

        #endregion Private Fields



        #region Public Methods

        /// <summary>
        /// Tạo 1 dòng dữ liệu
        /// </summary>
        /// <param name="entityData"> Dữ liệu cần tạo </param>
        /// <returns> </returns>
        public async Task CreateAsync(TEntity entityData)
        {
            await _context.Set<TEntity>().AddAsync(entityData);
        }

        /// <summary>
        /// Tạo nhiều dòng dữ liệu
        /// </summary>
        /// <param name="entityDatas"> Danh sách dữ liệu cần tạo </param>
        /// <returns> </returns>
        public async Task CreateMultiAsync(List<TEntity> entityDatas)
        {
            await _context.Set<TEntity>().AddRangeAsync(entityDatas);
        }

        /// <summary>
        /// Xoá bỏ hẳn dữ liệu
        /// </summary>
        /// <param name="entityData"> Dữ liệu cần xóa </param>
        /// <returns> </returns>
        public async Task DangerDeleteAsync(TEntity entityData)
        {
            await Task.Run(() => _context.Set<TEntity>().Remove(entityData));
        }

        /// <summary>
        /// Xoá bỏ hẳn nhiều dữ liệu
        /// </summary>
        /// <param name="entityDatas"> Danh sách dữ liệu cần xóa </param>
        /// <returns> </returns>
        public async Task DangerDeleteMultiAsync(List<TEntity> entityDatas)
        {
            await Task.Run(() => _context.Set<TEntity>().RemoveRange(entityDatas));
        }

        /// <summary>
        /// Ẩn dữ liệu
        /// </summary>
        /// <param name="entityData"> Dữ liệu cần ẩn </param>
        /// <returns> </returns>
        public async Task DeleteAsync(TEntity entityData)
        {
            await Task.Run(() => _context.Set<TEntity>().Update(entityData));
        }

        /// <summary>
        /// Ẩn nhiều dữ liệu
        /// </summary>
        /// <param name="entityDatas"> Danh sách dữ liệu cần ẩn </param>
        /// <returns> </returns>
        public async Task DeleteMultiAsync(List<TEntity> entityDatas)
        {
            await Task.Run(() => _context.Set<TEntity>().UpdateRange(entityDatas));
        }

        /// <summary>
        /// Lấy dòng dữ liệu theo ID
        /// </summary>
        /// <param name="id"> ID dòng cần lấy </param>
        /// <returns> 1 dòng dữ liệu theo ID truyền vào </returns>
        public async Task<TEntity?> GetByIdAsync(string id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Lấy 1 dòng dữ liệu theo điều kiện
        /// </summary>
        /// <param name="filter"> Điều kiện cần lấy </param>
        /// <returns> 1 dòng dữ liệu theo điều kiện truyền vào </returns>
        public async Task<TEntity?> GetDataAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            if (filter is null) return await _context.Set<TEntity>().FirstOrDefaultAsync();
            return await _context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        /// <summary>
        /// Lấy nhiều dòng dữ liệu theo điều kiện truyền vào
        /// </summary>
        /// <param name="filter"> Điều kiện cần lấy </param>
        /// <returns> Danh sách dữ liệu theo điều kiện truyền vào </returns>
        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            if (filter is null) return await _context.Set<TEntity>().ToListAsync();
            return await _context.Set<TEntity>().Where(filter).ToListAsync();
        }

        /// <summary>
        /// Cập nhật 1 dòng dữ liệu
        /// </summary>
        /// <param name="entityData"> Dữ liệu cần cập nhật </param>
        /// <returns> </returns>
        public async Task UpdateAsync(TEntity entityData)
        {
            await Task.Run(() => _context.Set<TEntity>().Update(entityData));
        }

        /// <summary>
        /// Cập nhật nhiều dữ liệu
        /// </summary>
        /// <param name="entityDatas"> Danh sách dữ liệu cần cập nhật </param>
        /// <returns> </returns>
        public async Task UpdateMultiAsync(List<TEntity> entityDatas)
        {
            await Task.Run(() => _context.Set<TEntity>().UpdateRange(entityDatas));
        }

        #endregion Public Methods
    }
}
