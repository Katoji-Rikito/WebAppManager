/** @format */

// Làm giao diện thời tiết, đồng hồ
// Thêm mục chọn nơi muốn xem, mặc định là hải phòng
// Thêm thiết lập vị trí để chọn, chọn mặc định
// thêm nút cập nhật danh sách tới link https://bulk.openweathermap.org/sample/

fetch("../content/openweathermap/ListCity.json")
	.then((response) => response.json())
	.then((listData) => {
		listData.forEach((element) => {
			element.lon = element.coord.lon;
			element.lat = element.coord.lat;
			delete element.coord;
		});

		CallServer_POST("/Home/UpdateOpenWeatherMapCity", true, listData, (data) => {
			console.log(data);
			//fetch("../content/openweathermap/ListCity1.json").then((response) => response.json()).then((data) => console.log(data));
		});
	});
