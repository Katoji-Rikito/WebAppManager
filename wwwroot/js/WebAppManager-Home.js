/** @format */

// Làm giao diện thời tiết, đồng hồ
// Thêm mục chọn nơi muốn xem, mặc định là hải phòng
// Thêm thiết lập vị trí để chọn, chọn mặc định
// thêm nút cập nhật danh sách tới link https://bulk.openweathermap.org/sample/

fetch("../content/openweathermap/ListCity.json")
    .then((response) => response.json())
    .then((listData) => {
        let callIt = false;
        debugger;
        listData.forEach((record) => {
            if (record.hasOwnProperty("coord"))
            {
                callIt = true;
                record.lon = record.coord?.lon;
                record.lat = record.coord?.lat;
                delete record.coord;
            }
        });

        if (callIt)
            CallToServer("POST", "/Home/UpdateOpenWeatherMapCity", true, undefined, undefined, listData, (data) => {
                console.log(data);

                //fetch("../content/openweathermap/ListCity1.json").then((response) => response.json()).then((data) => console.log(data));
            });
    });
