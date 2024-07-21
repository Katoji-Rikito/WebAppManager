﻿/** @format */

// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// KHAI BÁO CÁC BIẾN ---------------------------------------------------------------------------------------------------
const requestController = new AbortController();

// CÁC THIẾT LẬP MẶC ĐỊNH ---------------------------------------------------------------------------------------------------
/**
 * Thêm padding-top cho phần hiển thị nội dung
 */
document.addEventListener("DOMContentLoaded", function () {
    // Điều chỉnh padding-top của nội dung chính
    const adjustMainPaddingTop = () =>
        $("#mainContent")?.css("padding-top", $("#navbarApp").height() + "px");

    // Điều chỉnh padding-top khi tải trang
    adjustMainPaddingTop();

    // Điều chỉnh padding-top khi cửa sổ thay đổi kích thước
    window.addEventListener("resize", adjustMainPaddingTop);
});

/**
 * Thiết lập thời gian
 */
moment.locale("vi");

/**
 * Loại bỏ thông báo bản quyền DevExtreme 24.1
 */
//$(document).ready(() => $("#Layer_1").click());

// CÁC HÀM HỖ TRỢ KIỂM TRA ---------------------------------------------------------------------------------------------------
/**
 * Kiểm tra giá trị truyền vào có phải là một hàm không
 * @param {any} value Giá trị cần kiểm tra
 * @returns True nếu là một hàm
 */
function IsFunction(value) {
    return value instanceof Function;
}

/**
 * Kiểm tra giá trị có tồn tại hay không
 * @param {any} value Giá trị cần kiểm tra
 * @returns True nếu đã tồn tại
 */
function IsThisDefined(value) {
    return value !== undefined && value !== null;
}

/**
 * Kiểm tra giá trị có trống hay rỗng không
 * @param {any} value Giá trị cần kiểm tra
 * @returns True nếu trống
 */
function IsThisNullOrEmpty(value) {
    return !IsThisDefined(value) || value === "";
}

// CÁC HÀM HỖ TRỢ CHUỖI ---------------------------------------------------------------------------------------------------
/**
 * Viết hoa chữ cái đầu tiên của chuỗi string
 * @param {string} value Chuỗi cần viết hoa
 * @returns Chuỗi đã viết hoa chữ cái đầu tiên
 */
function CapitalizeString(value) {
    if (IsThisNullOrEmpty(value))
        return value;
    return value?.charAt(0)?.toUpperCase() + value?.slice(1);
}

// CÁC HÀM HỖ TRỢ GỌI SERVER ---------------------------------------------------------------------------------------------------
/**
 * Hàm lấy dữ liệu từ server
 * @param {string} callURL URL cần gọi
 * @param {boolean} showNotify Hiện thông báo không? True là hiện
 * @param {function} actionSuccess Hàm sẽ thực thi nếu thành công
 * @param {function} actionFail Hàm sẽ thực thi nếu thất bại
 * @returns Thực hiện lấy dữ liệu về server
 */
function CallServer_GET(
    callURL = "",
    showNotify = false,
    actionSuccess = null,
    actionFail = null
) {
    return axios.get(callURL, { signal: requestController.signal })
        .then((res) => {
            console.log(res);
            if (showNotify)
                DevExpress.ui.notify("Thành công", "success", 3000);
            // Chạy hàm truyền vào khi thành công (nếu có)
            if (IsFunction(actionSuccess))
                actionSuccess(res.data);
        })
        .catch((err) => {
            console.log(err);
            DevExpress.ui.notify(`Thất bại: ${err.response.status} ${err.response.statusText} - ${err.response.data}`, "error", 3000);
            // Chạy hàm truyền vào khi thất bại (nếu có)
            if (IsFunction(actionFail))
                actionFail(err);
        });
}

/**
 * Hàm gửi dữ liệu về server
 * @param {string} callURL URL cần gọi
 * @param {boolean} showNotify Hiện thông báo không? True là hiện
 * @param {object} inputArgs Tham số cần gửi về server
 * @param {function} actionSuccess Hàm sẽ thực thi nếu thành công
 * @param {function} actionFail Hàm sẽ thực thi nếu thất bại
 * @returns Thực hiện gửi dữ liệu về server
 */
function CallServer_POST(
    callURL = "",
    showNotify = false,
    inputArgs = {},
    actionSuccess = null,
    actionFail = null
) {
    return axios.post(callURL, inputArgs, { headers: { "Content-Type": "application/json" }, signal: requestController.signal })
        .then((res) => {
            console.log(res);
            if (showNotify)
                DevExpress.ui.notify(`(${res.status} ${res.statusText}) Thành công`, "success", 3000);
            // Chạy hàm truyền vào khi thành công (nếu có)
            if (IsFunction(actionSuccess))
                actionSuccess(res.data);
        })
        .catch((err) => {
            console.log(err);
            DevExpress.ui.notify(`Thất bại (${err.response.status} ${err.response.statusText}) với dữ liệu trả về: ${JSON.stringify(err.response.data)}`, "error", 3000);
            // Chạy hàm truyền vào khi thất bại (nếu có)
            if (IsFunction(actionFail))
                actionFail(err);
        });
}

// HIỂN THỊ THỜI GIAN THỰC ---------------------------------------------------------------------------------------------------
setInterval(() => $("#digitalClock")?.text(CapitalizeString(moment()?.format("dddd, DD/MM/YYYY HH:mm:ss A"))), 500);

// MÀN HÌNH LOADING ---------------------------------------------------------------------------------------------------
const appLoadingPanel = $("#appLoadingPanel").dxLoadPanel({
    focusStateEnabled: true, // Chỉ định xem thành phần UI có thể được tập trung hay không
    hideOnOutsideClick: true, // Ẩn nếu click vào vùng ngoài
    hideOnParentScroll: true, // Ẩn nếu cuộn phần tử cha
    hint: "Đang tải dữ liệu . . .", // Chỉ định văn bản cho gợi ý xuất hiện khi người dùng tạm dừng trên thành phần UI
    hoverStateEnabled: true, // Chỉ định xem thành phần giao diện người dùng có thay đổi trạng thái hay không khi người dùng tạm dừng trên đó
    indicatorSrc: "/content/images/loading.gif", // Nguồn thay cho hình ảnh mặc định
    message: "Đang tải dữ liệu . . .",
    position: { my: "center", at: "center", of: "#mainContent" }, // Vị trí hiển thị
    shading: true, // Tạo bóng nền hay không
    shadingColor: "rgba(0,0,0,0.25)", // Màu bóng nền
    showIndicator: true, // Hiện ảnh loading
    showPane: true, // Hiện bảng
}).dxLoadPanel("instance");

$(document)
    .ajaxStart(() => {
        $("#notifyText")?.text("(Đang tải dữ liệu . . .)");
        appLoadingPanel?.show();
    })
    .ajaxStop(() => {
        $("#notifyText")?.text("");
        appLoadingPanel?.hide();
    });