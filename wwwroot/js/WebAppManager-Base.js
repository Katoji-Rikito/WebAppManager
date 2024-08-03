/** @format */

// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// KHAI BÁO CÁC BIẾN ---------------------------------------------------------------------------------------------------
/** Điều khiển request, chủ yếu để abort() */
const requestController = new AbortController();

// CÁC THIẾT LẬP MẶC ĐỊNH ---------------------------------------------------------------------------------------------------
/** Thêm padding-top cho phần hiển thị nội dung */
document.addEventListener("DOMContentLoaded", function () {
    /** Điều chỉnh padding-top của nội dung chính */
    const adjustMainPaddingTop = function () {
        $("#mainContent")?.css("padding-top", $("#navbarApp")?.height() + "px");
    }

    /** Điều chỉnh padding-top khi tải trang */
    adjustMainPaddingTop();

    /** Điều chỉnh padding-top khi cửa sổ thay đổi kích thước */
    window.addEventListener("resize", adjustMainPaddingTop);
});

/** Thiết lập thời gian */
moment.locale("vi");

/** Loại bỏ thông báo bản quyền DevExtreme 24.1+ */
$(document).ready(() => $("#Layer_1")?.click());

/** Màn hình tải trang */
const appLoadingPanel = $("#appLoadingPanel")
    .dxLoadPanel({
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
    })
    .dxLoadPanel("instance");

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
    if (IsThisNullOrEmpty(value)) return value;
    return value?.charAt(0)?.toUpperCase() + value?.slice(1);
}

// CÁC HÀM HỖ TRỢ GỌI SERVER ---------------------------------------------------------------------------------------------------
/**
 * Gọi hoặc gửi dữ liệu về server
 * @param {string} callMethod Phương thức HTTP: GET: Đọc, POST: Tạo, PUT: Cập nhật, DELETE: Xoá. Mặc định: "GET"
 * @param {string} callURL URL cần gọi, lưu ý bỏ đuôi Async. Mặc định: ""
 * @param {boolean} isShowNotify Hiện thông báo không? True là hiện. Mặc định: true
 * @param {object} getArgs Tham số cần gửi về server theo method GET (nếu có). Mặc định: null
 * @param {object} postArgs Tham số cần gửi về server theo method POST (nếu có). Mặc định: null
 * @param {function} actionSuccess Hàm sẽ thực thi nếu thành công. Mặc định: null
 * @param {function} actionFail Hàm sẽ thực thi nếu thất bại. Mặc định: null
 * @returns Trả về promise axios (?)
 */
function CallToServer(
    callMethod = "GET",
    callURL = "",
    isShowNotify = true,
    getArgs = null,
    postArgs = null,
    actionSuccess = null,
    actionFail = null
) {
    $("#notifyText")?.text("(Đang tải dữ liệu . . .)");
    appLoadingPanel?.show();
    return axios({
        method: callMethod,
        url: callURL,
        params: getArgs, // Chỉ dùng với method GET
        data: postArgs, // Chỉ dùng với method POST
    })
        .then((res) => {
            console.log("Thành công: ", res);

            // Thông báo kết quả trả về
            if (isShowNotify)
                DevExpress.ui.notify(
                    `(${res.status} - ${res.statusText}) Thành công`,
                    "success",
                    3000,
                );

            // Chạy hàm truyền vào khi thành công (nếu có)
            if (IsFunction(actionSuccess)) actionSuccess(res.data);
        })
        .catch((err) => {
            console.log("Lỗi: ", err);

            // Thông báo lỗi
            if (err.response.data?.includes("<!DOCTYPE html>"))
                $("#mainContent").html(err.response.data);
            else
                DevExpress.ui.notify(
                    `(${err.response.status} - ${err.response.statusText}) Thất bại với dữ liệu trả về: ${JSON.stringify(err.response.data)}`,
                    "error",
                    3000,
                );

            // Chạy hàm truyền vào khi thất bại (nếu có)
            if (IsFunction(actionFail)) actionFail(err);
        })
        .finally(function () {
            $("#notifyText")?.text("");
            appLoadingPanel?.hide();
        });
}

// THANH ĐIỀU HƯỚNG =================================================================================
/** Nút menu */
const dropdownbtn_MenuApp = $("#dropdownbtn_MenuApp")
    .dxDropDownButton({
        dataSource: [
            {
                icon: "home",
                text: "Trang chủ",
                onClick: (e) => window.location.replace("/"),
            },
        ],
        disabled: !isUserLogedIn,
        dropDownOptions: {
            dragEnabled: true,
            dragOutsideBoundary: true,
            enableBodyScroll: true,
            height: "auto",
            position: { my: "left top", at: "left bottom", of: "#dropdownbtn_MenuApp" },
            resizeEnabled: true,
            width: "auto",
        },
        hint: "Danh sách chức năng phần mềm, hiện tại đang ở " + navbar_Title,
        icon: "menu",
        noDataText: "Vô lý ???",
        stylingMode: "contained",
        text: navbar_Title,
        type: "normal",
        useItemTextAsTitle: false,
    })
    .dxDropDownButton("instance");

/** Đồng hồ và nút đăng xuất */
const dropdownbtn_DigitalClock = $("#dropdownbtn_DigitalClock")
    .dxDropDownButton({
        dataSource: [
            {
                disabled: !isUserLogedIn,
                icon: "runner",
                text: "Đăng xuất",
                onClick: function (e) {
                    CallToServer("GET", "/Account/Logout", false, undefined, undefined, () => window.location.reload());
                },
            },
        ],
        dropDownOptions: {
            dragEnabled: true,
            dragOutsideBoundary: true,
            enableBodyScroll: true,
            height: "auto",
            position: { my: "right top", at: "right bottom", of: "#dropdownbtn_DigitalClock" },
            resizeEnabled: true,
            width: "auto",
        },
        hint: "Đăng xuất phần mềm",
        icon: "clock",
        noDataText: "Vô lý ???",
        stylingMode: "contained",
        type: "normal",
        useItemTextAsTitle: false,
    })
    .dxDropDownButton("instance");

/** Hiển thị thời gian thực */
setInterval(
    () =>
        dropdownbtn_DigitalClock.option(
            "text",
            CapitalizeString(moment()?.format("dddd, DD/MM/YYYY HH:mm:ss A")),
        ),
    0,
);
