/** @format */

// Làm giao diện thời tiết, đồng hồ
// Thêm mục chọn nơi muốn xem, mặc định là hải phòng
// Thêm thiết lập vị trí để chọn, chọn mặc định
// thêm nút cập nhật danh sách tới link https://bulk.openweathermap.org/sample/

//fetch("../content/openweathermap/ListCity.json")
//    .then((response) => response.json())
//    .then((listData) => {
//        let callIt = false;
//        debugger;
//        listData.forEach((record) => {
//            if (record.hasOwnProperty("coord"))
//            {
//                callIt = true;
//                record.lon = record.coord?.lon;
//                record.lat = record.coord?.lat;
//                delete record.coord;
//            }
//        });

//        if (callIt)
//            CallToServer("POST", "/Home/UpdateOpenWeatherMapCity", true, undefined, undefined, listData, (data) => {
//                console.log(data);

//                //fetch("../content/openweathermap/ListCity1.json").then((response) => response.json()).then((data) => console.log(data));
//            });
//    });

///**
// * Hàm tải dữ liệu thành phố của OpenWeatherMap
// * @returns Danh sách thành phố
// */
//const LoadCity = () => CallToServer("GET", "../content/openweathermap/cities.json", false, undefined, undefined, undefined, (data) => {

//});

//$(() => LoadCity());

///** Store chứa dữ liệu thành phố */
//const store_City = new DevExpress.data.ArrayStore({
//    data: [],
//    errorHandler: (error) => console.log("store_City error", error),
//    key: "id",
//});

/** View hiển thị thông tin thời tiết */
const view_Weather = $("#view_Weather").dxForm({
    items: [ {
        itemType: "tabbed",
        name: "ViewOpenWeatherMap",
        tabPanelOptions: {
            animationEnabled: true,
            loop: true,
            repaintChangesOnly: true,
            showNavButtons: true,
            swipeEnabled: true,
        },
        tabs: [ {
            icon: "fa-solid fa-cloud-sun",
            title: "Thông tin thời tiết",
            items:[],
        }, {
            icon: "fa-solid fa-city",
            title: "Thông tin thành phố",
            items: [],
        } ],
    } ],
    labelMode: "floating",
    requiredMessage: "{0} là bắt buộc",
    scrollingEnabled: true,
    showOptionalMark: true,
}).dxForm("instance");