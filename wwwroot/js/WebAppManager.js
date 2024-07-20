/** @format */

// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

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
 * Hàm gọi dữ liệu từ server
 * @param {string} callURL URL cần gọi
 * @param {boolean} isAsync Thực hiện bất đồng bộ hay không? True là có
 * @param {boolean} showNotify Hiện thông báo không? True là hiện
 * @param {function} actionSuccess Hàm sẽ thực thi nếu thành công
 * @param {function} actionFail Hàm sẽ thực thi nếu thất bại
 */
function CallServer_GET(
    callURL = "",
    isAsync = true,
    showNotify = false,
    actionSuccess = null,
    actionFail = null,
) {
    $.ajax({
        url: callURL,
        type: "GET",
        async: isAsync,
        dataType: "json",
        success(result, status, xhr) {
            //console.log(result);
            //console.log(status);
            //console.log(xhr);
            if (showNotify)
                DevExpress.ui.notify("Thành công", "success", 3000);

            if (IsFunction(actionSuccess))
                actionSuccess(result);
        },
        error(xhr, status, error) {
            console.log("xhr: " + xhr);
            console.log("status: " + status);
            console.log("error: " + error);
            DevExpress.ui.notify("Thất bại: " + xhr, "error", 3000);

            if (IsFunction(actionFail))
                actionFail(xhr);
        },
    });
}

/**
 * Hàm gửi dữ liệu về server
 * @param {string} callURL URL cần gọi
 * @param {boolean} isAsync Thực hiện bất đồng bộ hay không? True là có
 * @param {boolean} showNotify Hiện thông báo không? True là hiện
 * @param {object} args Tham số cần gửi về server
 * @param {function} actionSuccess Hàm sẽ thực thi nếu thành công
 * @param {function} actionFail Hàm sẽ thực thi nếu thất bại
 */
function CallServer_POST(
    callURL = "",
    isAsync = true,
    showNotify = false,
    args = [],
    actionSuccess = null,
    actionFail = null,
) {
    $.ajax({
        url: callURL,
        type: "POST",
        async: isAsync,
        dataType: "json",
        data: args,
        success(result, status, xhr) {
            console.log(result);
            console.log(status);
            console.log(xhr);
            if (showNotify)
                DevExpress.ui.notify("Thành công", "success", 3000);

            if (IsFunction(actionSuccess))
                actionSuccess(result);
        },
        error(xhr, status, error) {
            console.log("xhr: " + xhr);
            console.log("status: " + status);
            console.log("error: " + error);
            DevExpress.ui.notify("Thất bại: " + xhr, "error", 3000);

            if (IsFunction(actionFail))
                actionFail(xhr);
        },
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

// KHAI BÁO CÁC BIẾN FORM ĐĂNG NHẬP ---------------------------------------------------------------------------------------------------
const URL_Login = "/Account/Login";
const URL_Logout = "/Account/Logout";

// CÁC HÀM HỖ TRỢ FORM ĐĂNG NHẬP ---------------------------------------------------------------------------------------------------
/**
 * Thay đổi chế độ text và password
 * @param {string} id Tên trường cần thay đổi
 */
function ChangeTextMode(id) {
    const txtMode = dxForm_Account?.getEditor(id);
    const iconShow = txtMode?.getButton("showText");
    if (txtMode?.option("mode") === "text") {
        txtMode?.option("mode", "password");
        iconShow?.option("icon", "eyeopen");
    } else {
        txtMode?.option("mode", "text");
        iconShow?.option("icon", "eyeclose");
    }
}

// FORM ĐĂNG NHẬP ---------------------------------------------------------------------------------------------------
const dxForm_Account = $("#dxForm_Account")?.dxForm({
    items: [{
        label: { text: "Tên đăng nhập" },
        dataField: "TenDangNhap",
        editorOptions: {
            mode: "password",
            buttons: [{
                name: "showText",
                location: "after",
                options: {
                    stylingMode: "text",
                    icon: "eyeopen",
                    onClick: () => ChangeTextMode("TenDangNhap"),
                },
            }],
        },
        validationRules: [{
            type: "required",
            message: "Tên đăng nhập là bắt buộc",
        }],
    },
    {
        label: { text: "Mật khẩu" },
        dataField: "MatKhau",
        editorOptions: {
            mode: "password",
            buttons: [{
                name: "showText",
                location: "after",
                options: {
                    stylingMode: "text",
                    icon: "eyeopen",
                    onClick: () => ChangeTextMode("MatKhau"),
                },
            }],
        },
        validationRules: [{
            type: "required",
            message: "Tên đăng nhập là bắt buộc",
        }, {
            type: "pattern",
            pattern: /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$/,
            message: "Mật khẩu chứa ít nhất 8 ký tự gồm: chữ hoa, chữ thường, số và ký tự đặc biệt",
        }],
    },
    {
        itemType: "button",
        buttonOptions: {
            icon: "login",
            stylingMode: "contained",
            text: "Đăng nhập",
            type: "success",
            width: "100%",
            onClick: () => {
                if (dxForm_Account.validate().isValid)
                    CallServer_POST(URL_Login, true, false, {
                        userName: dxForm_Account?.getEditor("TenDangNhap")?.option("value"),
                        userPass: dxForm_Account?.getEditor("MatKhau")?.option("value"),
                    });
            },
        },
    }],
}).dxForm("instance");
