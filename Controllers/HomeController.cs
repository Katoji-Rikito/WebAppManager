using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WebAppManager.Models;
using WebAppManager.Models.DTOs;

namespace WebAppManager.Controllers
{
    public class HomeController : BaseController<HomeController, DsNguontien>
    {
        #region Public Methods

        public async Task<IActionResult> Privacy()
        {
            return await Task.Run(View);
        }

        /// <summary>
        /// Cập nhật danh sách thành phố của OpenWeatherMap
        /// </summary>
        /// <param name="listData">Danh sách thành phố</param>
        /// <returns>Kết quả lưu</returns>
        [HttpPost]
        public async Task<IActionResult> UpdateOpenWeatherMapCityAsync([FromBody] List<OpenWeatherMapCityDto> listData)
        {
            string contentFile = JsonSerializer.Serialize(listData);
            await System.IO.File.WriteAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/content/openweathermap", "ListCity1.json"), contentFile, encoding: Encoding.UTF8);

            return await Task.Run(() => Created(string.Empty, listData));
        }
        #endregion Public Methods
    }
}
