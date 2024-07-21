// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

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
        //}, {
        //    type: "pattern",
        //    pattern: /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$/,
        //    message: "Mật khẩu chứa ít nhất 8 ký tự gồm: chữ hoa, chữ thường, số và ký tự đặc biệt",
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
                    CallServer_POST(URL_Login, true, {
                        userName: dxForm_Account?.getEditor("TenDangNhap")?.option("value")?.trim()?.toUpperCase(),
                        userPass: dxForm_Account?.getEditor("MatKhau")?.option("value")?.trim(),
                    });
            },
        },
    }],
}).dxForm("instance");
