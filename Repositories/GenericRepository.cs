using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAppManager.Models;
using WebAppManager.Repositories.Interfaces;

namespace WebAppManager.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntities
    {
        #region Private Fields

        private readonly WebappmanagerContext _context;
        private readonly DbSet<TEntity> _dbSetEntity;

        #endregion Private Fields

        #region Public Constructors

        public GenericRepository(WebappmanagerContext context)
        {
            _context = context;
            _dbSetEntity = _context.Set<TEntity>();
        }

        #endregion Public Constructors



        #region Public Methods

        /// <summary>
        /// Tạo 1 dòng dữ liệu
        /// </summary>
        /// <param name="entityData"> Dữ liệu cần tạo </param>
        /// <returns> Dữ liệu vừa tạo </returns>
        /// <exception cref="Exception"> </exception>
        public async Task<TEntity> CreateAsync(TEntity entityData)
        {
            try
            {
                entityData.IsActive = true;
                entityData.UpdatedAt = DateTime.Now;
                await _dbSetEntity.AddAsync(entityData);
                return entityData;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Tạo nhiều dòng dữ liệu
        /// </summary>
        /// <param name="entityDatas"> Danh sách dữ liệu cần tạo </param>
        /// <returns> Danh sách dữ liệu vừa tạo </returns>
        /// <exception cref="Exception"> </exception>
        public async Task<IEnumerable<TEntity>> CreateMultiAsync(List<TEntity> entityDatas)
        {
            try
            {
                entityDatas.ForEach(record =>
                {
                    record.IsActive = true;
                    record.UpdatedAt = DateTime.Now;
                });
                await _dbSetEntity.AddRangeAsync(entityDatas);
                return entityDatas;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Xoá bỏ hẳn dữ liệu
        /// </summary>
        /// <param name="entityData"> Dữ liệu cần xóa </param>
        /// <returns> Dữ liệu vừa xóa </returns>
        /// <exception cref="Exception"> </exception>
        public async Task<TEntity> DangerDeleteAsync(TEntity entityData)
        {
            try
            {
                await Task.Run(() => _dbSetEntity.Remove(entityData));
                return entityData;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Xoá bỏ hẳn nhiều dữ liệu
        /// </summary>
        /// <param name="entityDatas"> Danh sách dữ liệu cần xóa </param>
        /// <returns> Danh sách dữ liệu vừa xóa </returns>
        /// <exception cref="Exception"> </exception>
        public async Task<IEnumerable<TEntity>> DangerDeleteMultiAsync(List<TEntity> entityDatas)
        {
            try
            {
                await Task.Run(() => _dbSetEntity.RemoveRange(entityDatas));
                return entityDatas;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Ẩn dữ liệu
        /// </summary>
        /// <param name="entityData"> Dữ liệu cần ẩn </param>
        /// <returns> Dữ liệu vừa ẩn </returns>
        /// <exception cref="Exception"> </exception>
        public async Task<TEntity> DeleteAsync(TEntity entityData)
        {
            try
            {
                entityData.IsActive = false;
                entityData.UpdatedAt = DateTime.Now;
                await Task.Run(() => _dbSetEntity.Update(entityData));
                return entityData;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Ẩn nhiều dữ liệu
        /// </summary>
        /// <param name="entityDatas"> Danh sách dữ liệu cần ẩn </param>
        /// <returns> Danh sách dữ liệu vừa ẩn </returns>
        /// <exception cref="Exception"> </exception>
        public async Task<IEnumerable<TEntity>> DeleteMultiAsync(List<TEntity> entityDatas)
        {
            try
            {
                entityDatas.ForEach(record =>
                {
                    record.IsActive = false;
                    record.UpdatedAt = DateTime.Now;
                });
                await Task.Run(() => _dbSetEntity.UpdateRange(entityDatas));
                return entityDatas;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Lấy dòng dữ liệu theo ID
        /// </summary>
        /// <param name="id"> ID dòng cần lấy </param>
        /// <returns> Dữ liệu của ID vừa truyền vào </returns>
        /// <exception cref="Exception"> </exception>
        public async Task<TEntity?> GetByIdAsync(string id)
        {
            try
            {
                return await _dbSetEntity.FindAsync(id);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Lấy 1 dòng dữ liệu theo điều kiện. Nếu không có điều kiện nào thì trả về FirstOrDefaultAsync()
        /// </summary>
        /// <param name="filter"> Điều kiện cần lấy (Nếu có) </param>
        /// <returns> Dữ liệu của điều kiện vừa truyền vào </returns>
        /// <exception cref="Exception"> </exception>
        public async Task<TEntity?> GetDataAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            try
            {
                if (filter is null) return await _dbSetEntity.FirstOrDefaultAsync();
                return await _dbSetEntity.FirstOrDefaultAsync(filter);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Lấy nhiều dòng dữ liệu theo điều kiện truyền vào. Nếu không có điều kiện truyền vào thì
        /// lấy toàn bộ bảng
        /// </summary>
        /// <param name="filter"> Điều kiện cần lấy (Nếu có) </param>
        /// <returns> Danh sách dữ liệu theo điều kiện vừa truyền vào </returns>
        /// <exception cref="Exception"> </exception>
        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            try
            {
                if (filter is null) return await _dbSetEntity.ToListAsync();
                return await _dbSetEntity.Where(filter).ToListAsync();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Kiểm tra xem đã tồn tại bản ghi hay chưa
        /// </summary>
        /// <param name="filter"> Điều kiện cần tìm </param>
        /// <returns> True nếu đã tồn tại </returns>
        /// <exception cref="Exception"> </exception>
        public async Task<bool> IsRecordExists(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                if (filter is null) throw new Exception(CommonMessages.NeedExpression);
                return await _dbSetEntity.AnyAsync(filter);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Cập nhật 1 dòng dữ liệu
        /// </summary>
        /// <param name="entityData"> Dữ liệu cần cập nhật </param>
        /// <returns> Dữ liệu vừa cập nhật </returns>
        /// <exception cref="Exception"> </exception>
        public async Task<TEntity> UpdateAsync(TEntity entityData)
        {
            try
            {
                entityData.UpdatedAt = DateTime.Now;
                await Task.Run(() => _dbSetEntity.Update(entityData));
                return entityData;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Cập nhật nhiều dữ liệu
        /// </summary>
        /// <param name="entityDatas"> Danh sách dữ liệu cần cập nhật </param>
        /// <returns> Danh sách dữ liệu vừa cập nhật </returns>
        /// <exception cref="Exception"> </exception>
        public async Task<IEnumerable<TEntity>> UpdateMultiAsync(List<TEntity> entityDatas)
        {
            try
            {
                entityDatas.ForEach(record => record.UpdatedAt = DateTime.Now);
                await Task.Run(() => _dbSetEntity.UpdateRange(entityDatas));
                return entityDatas;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        #endregion Public Methods
    }
}
