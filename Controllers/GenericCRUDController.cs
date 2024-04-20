using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppManager.Models;

namespace WebAppManager.Controllers
{
    public class GenericCRUDController<T> : Controller where T : BaseEntities
    {
        #region Private Fields

        private readonly WebappmanagerContext _context;

        #endregion Private Fields

        #region Public Constructors

        public GenericCRUDController(WebappmanagerContext context)
        {
            _context = context;
        }

        #endregion Public Constructors



        #region Public Methods

        /// <summary>
        /// Tạo danh sách dữ liệu mới
        /// </summary>
        /// <param name="entityData"> Danh sách dữ liệu mới cần tạo </param>
        /// <returns> Danh sách thông tin dữ liệu đã tạo </returns>
        [HttpPost]
        public virtual async Task<IActionResult> CreateMultiAsync(List<T> listEntityData)
        {
            try
            {
                listEntityData.ForEach(record => record.UpdatedAt = DateTime.Now);
                await _context.Set<T>().AddRangeAsync(listEntityData);
                await _context.SaveChangesAsync();
                return CreatedAtAction("CreateMultiAsync", new { Id = string.Join(",", listEntityData.Select(l => l.Id)) }, listEntityData);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Tạo 1 dòng dữ liệu mới
        /// </summary>
        /// <param name="entityData"> Thông tin dữ liệu cần tạo </param>
        /// <returns> Trả về thông tin dũ liệu đã tạo </returns>
        [HttpPost]
        public virtual async Task<IActionResult> CreateSingleAsync(T entityData)
        {
            try
            {
                entityData.UpdatedAt = DateTime.Now;
                await _context.Set<T>().AddAsync(entityData);
                await _context.SaveChangesAsync();
                return CreatedAtAction("CreateSingleAsync", new { Id = entityData.Id }, entityData);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Xóa nhiều dòng dữ liệu
        /// </summary>
        /// <param name="ids"> Danh sách ID cần xóa </param>
        /// <returns> Danh sách ID đã xoá </returns>
        [HttpDelete]
        public virtual async Task<IActionResult> DeleteMultiAsync(List<string> ids)
        {
            try
            {
                ids.ForEach(async id =>
                {
                    var record = await _context.Set<T>().FindAsync(id);
                    if (record == null) throw new Exception("Không tìm thấy ID: " + id);
                    _context.Set<T>().Remove(record);
                });
                await _context.SaveChangesAsync();
                return Ok(ids);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Xóa 1 dòng dữ liệu
        /// </summary>
        /// <param name="id"> ID cần xoá </param>
        /// <returns> Trả về ID đã xoá </returns>
        [HttpDelete]
        public virtual async Task<IActionResult> DeleteSingleAsync(string id)
        {
            try
            {
                var record = await _context.Set<T>().FindAsync(id);
                if (record == null) return NotFound(id);
                _context.Set<T>().Remove(record);
                await _context.SaveChangesAsync();
                return Ok(id);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns> Danh sách tất cả </returns>
        [HttpGet]
        public virtual async Task<IActionResult> GetAllListAsync()
        {
            try { return Ok(await _context.Set<T>().ToListAsync()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Lấy dữ liệu theo ID
        /// </summary>
        /// <param name="id"> Số ID cần tìm </param>
        /// <returns> Danh sách với ID cần tìm </returns>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetDetailByIdAsync(string id)
        {
            try
            {
                var listResult = await _context.Set<T>().FindAsync(id);
                if (listResult == null) return NotFound(id);
                return Ok(listResult);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Cập nhật nhiều dòng dữ liệu
        /// </summary>
        /// <param name="listEntityData"> Danh sách dữ liệu cần cập nhật </param>
        /// <returns> Trả về danh sách vừa cập nhật </returns>
        [HttpPut]
        public virtual async Task<IActionResult> UpdateMultiAsync(List<T> listEntityData)
        {
            try
            {
                listEntityData.ForEach(async record =>
                {
                    if (!(await IsEntityExists(record.Id))) throw new Exception("Không tìm thấy ID: " + record.Id);
                    record.UpdatedAt = DateTime.Now;
                    _context.Set<T>().Update(record);
                });
                await _context.SaveChangesAsync();
                return Ok(listEntityData);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Cập nhật 1 dòng dữ liệu
        /// </summary>
        /// <param name="entityData"> Dữ liệu cần cập nhật </param>
        /// <returns> Trả về dòng dữ liệu vừa cập nhật </returns>
        [HttpPut]
        public virtual async Task<IActionResult> UpdateSingleAsync(T entityData)
        {
            try
            {
                if (!(await IsEntityExists(entityData.Id))) return NotFound(entityData.Id);
                entityData.UpdatedAt = DateTime.Now;
                _context.Set<T>().Update(entityData);
                await _context.SaveChangesAsync();
                return Ok(entityData);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        #endregion Public Methods



        #region Private Methods

        /// <summary>
        /// Kiểm tra có tồn tại dòng dữ liệu với ID nhập vào hay không
        /// </summary>
        /// <param name="id"> ID cần kiểm tra </param>
        /// <returns> Kết quả True nếu tồn tại </returns>
        /// <exception cref="Exception"> </exception>
        private async Task<bool> IsEntityExists(string id)
        {
            try { return await _context.Set<T>().AnyAsync(r => r.Id == id); }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        #endregion Private Methods
    }
}
