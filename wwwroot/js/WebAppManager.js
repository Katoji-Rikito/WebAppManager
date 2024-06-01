// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// KHAI BÁO CÁC BIẾN ---------------------------------------------------------------------------------------------------
const urlLogin = "/Account/Login";
const urlLogout = "/Account/Logout";

// CÁC HÀM HỖ TRỢ ---------------------------------------------------------------------------------------------------
/**
 * Kiểm tra giá trị có trống hay rỗng không
 * @param {any} value Giá trị cần kiểm tra
 * @returns True nếu trống
 */
function IsNullOrEmpty(value) {
    return value === undefined || value === null || value === "";
}

// THÊM PADDING TOP CHO PHẦN HIỂN THỊ NỘI DUNG ---------------------------------------------------------------------------------------------------


document.addEventListener("DOMContentLoaded", function () {
    // Điều chỉnh padding-top của nội dung chính
    const adjustMainPaddingTop = () => $("#mainContent")?.css("padding-top", $("#navbarApp").height() + "px");

    // Điều chỉnh padding-top khi tải trang
    adjustMainPaddingTop();

    // Điều chỉnh padding-top khi cửa sổ thay đổi kích thước
    window.addEventListener("resize", adjustMainPaddingTop);
});

// HIỂN THỊ THỜI GIAN THỰC ---------------------------------------------------------------------------------------------------
const thuTrongTuan = ["Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy"];
setInterval(async () => {
    const now = new Date();
    const day = thuTrongTuan[now.getDay()];
    const date = now.getDate().toString().padStart(2, "0");
    const month = (now.getMonth() + 1).toString().padStart(2, "0");
    const year = now.getFullYear();
    const hour = now.getHours().toString().padStart(2, "0");
    const minute = now.getMinutes().toString().padStart(2, "0");
    const second = now.getSeconds().toString().padStart(2, "0");
    $("#digitalClock")?.text(`${day}, ${date}/${month}/${year} ${hour}:${minute}:${second}`);
}, 500);

// MÀN HÌNH LOADING ---------------------------------------------------------------------------------------------------
const loadPanel = $("#dxLoadingPanel").dxLoadPanel({
    focusStateEnabled: true, // Chỉ định xem thành phần UI có thể được tập trung hay không
    hideOnOutsideClick: true, // Ẩn nếu click vào vùng ngoài
    hideOnParentScroll: true, // Ẩn nếu cuộn phần tử cha
    hint: "Đang tải dữ liệu . . .", // Chỉ định văn bản cho gợi ý xuất hiện khi người dùng tạm dừng trên thành phần UI
    hoverStateEnabled: true, // Chỉ định xem thành phần giao diện người dùng có thay đổi trạng thái hay không khi người dùng tạm dừng trên đó
    indicatorSrc: "/content/images/loading.gif", // Nguồn thay cho hình ảnh mặc định
    message: "Đang tải dữ liệu . . .",
    position: { my: "center", at: "center", of: "window" }, // Vị trí hiển thị
    shading: true, // Tạo bóng nền hay không
    shadingColor: "rgba(0,0,0,0.25)", // Màu bóng nền
    showIndicator: true, // Hiện ảnh loading
    showPane: true, // Hiện bảng
}).dxLoadPanel("instance");

$(document).ajaxStart(async () => {
    $("#notifyText")?.text("(Đang tải dữ liệu . . .)");
    loadPanel?.show();
}).ajaxStop(async () => {
    $("#notifyText")?.text("");
    loadPanel?.hide();
});

// MÀN HÌNH ĐĂNG NHẬP ---------------------------------------------------------------------------------------------------
/**
 * Hàm lấy dữ liệu từ server
 * @param {String} callURL URL cần gọi
 * @param {Boolean} showNotify Hiện thông báo không? True là hiện
 * @param {Function} actionForSuccess Hàm sẽ thực thi nếu thành công
 */
async function GetData(callURL = "", showNotify = false, actionForSuccess = null) {
    await $.ajax({
        url: callURL,
        type: "GET",
        async: true,
        dataType: "json",
        success(result, status, xhr) {
            console.log(result);
            console.log(status);
            console.log(xhr);
            actionForSuccess();
        },
        error(xhr, status, error) {
            console.log(error);
            console.log(status);
            console.log(xhr);
        }
    });
}
/**
 * Hàm gửi dữ liệu về server
 * @param {String} callURL URL cần gọi
 * @param {Boolean} showNotify Hiện thông báo không? True là hiện
 * @param {Object} args Tham số cần gửi về server
 * @param {Function} actionForSuccess Hàm sẽ thực thi nếu thành công
 */
async function PostData(callURL = "", showNotify = false, args = [], actionForSuccess = null) {
    await $.ajax({
        url: callURL,
        type: "POST",
        async: true,
        dataType: "json",
        data: args,
        success(result, status, xhr) {
            console.log(result);
            console.log(status);
            console.log(xhr);
            actionForSuccess();
        },
        error(xhr, status, error) {
            console.log(error);
            console.log(status);
            console.log(xhr);
        }
    });
}

// MÀN HÌNH ĐĂNG NHẬP ---------------------------------------------------------------------------------------------------
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
                    onClick: () => changeTextMode("TenDangNhap"),
                },
            }],
        },
        validationRules: [{
            type: "required",
            message: "Tên đăng nhập là bắt buộc",
        }],
    }, {
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
                    onClick: () => changeTextMode("MatKhau"),
                },
            }],
        },
        validationRules: [{
            type: "required",
            message: "Mật khẩu là bắt buộc",
        }],
    }, {
        itemType: "button",
        buttonOptions: {
            icon: "login",
            stylingMode: "contained",
            text: "Đăng nhập",
            type: "success",
            width: "100%",
            onClick: () => {
                if (dxForm_Account.validate().isValid)
                    PostData(urlLogin, false, { userName: GetEditorValue(dxForm_Account, "TenDangNhap"), userPass: GetEditorValue(dxForm_Account, "MatKhau") });
            },
        },
    }],
}).dxForm("instance");

/**
 * Thay đổi chế độ text và password
 * @param {string} id Tên trường cần thay đổi
 */
function changeTextMode(id) {
    const txtPass = dxForm_Account?.getEditor(id);
    const iconPass = txtPass?.getButton("showText");
    if (txtPass?.option("mode") === "text") {
        txtPass?.option("mode", "password");
        iconPass?.option("icon", "eyeopen");
    }
    else {
        txtPass?.option("mode", "text");
        iconPass?.option("icon", "eyeclose");
    }
}

/**
 * Lấy giá trị từ form
 * @param {any} form Form cần lấy giá trị
 * @param {String} id ID cần lấy giá trị
 * @returns Giá trị lấy được
 */
function GetEditorValue(form = null, id ="") {
    return form?.getEditor(id)?.option("value");
}

// MÃ HOÁ ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function encrypt(data) {
    // convert the JSON object to a string
    const jsonDataString = JSON.stringify(data);

    // Create iv and secret_key
    let iv = crypto.randomBytes(16);
    let secret_key = crypto.randomBytes(32);

    iv = CryptoJS.lib.WordArray.create(iv);
    secret_key = CryptoJS.lib.WordArray.create(secret_key);

    // Encrypt the plaintext using AES/CBC/PKCS5Padding
    const ciphertext = CryptoJS.AES.encrypt(jsonDataString, secret_key, {
        iv: iv,
        padding: CryptoJS.pad.Pkcs7,
        mode: CryptoJS.mode.CBC,
    });

    console.log("iv", iv);
    console.log("secret_key", secret_key);
    console.log("cipherText", ciphertext);

    // Print the ciphertext
    console.log(ciphertext.toString());
}
