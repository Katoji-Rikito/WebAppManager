// CÁC HÀM HỖ TRỢ FORM ĐĂNG NHẬP ---------------------------------------------------------------------------------------------------
/**
 * Thay đổi chế độ text và password
 * @param {string} id Tên trường cần thay đổi
 */
function ChangeTextMode(id) {
    const txtMode = dxForm_Account?.getEditor(id);
    const iconShow = txtMode?.getButton("showText");
    if (txtMode?.option("mode") === "text")
    {
        txtMode?.option("mode", "password");
        iconShow?.option("icon", "eyeopen");
    } else
    {
        txtMode?.option("mode", "text");
        iconShow?.option("icon", "eyeclose");
    }
}

/** Kiểm tra xem đây có phải là lần đầu triển khai ứng dụng hay không */
function IsFirstTimeDeploy() {
    CallToServer("GET", "/Account/CheckFirstTimeDeployApp", false, undefined, undefined, undefined, (result) => {
        dxForm_Account.option("onEditorEnterKey", result ? () => CreateAccountForFirstTimeDeploy() : () => TryLogin());
        dxForm_Account.getButton("btn_Login").option("text", result ? "Tạo tài khoản lần đầu" : "Đăng nhập");
        dxForm_Account.getButton("btn_Login").option("onClick", result ? () => CreateAccountForFirstTimeDeploy() : () => TryLogin());
        if (result)
            alert("👋 Ồ xin chào!\n🎉 Chúc mừng bạn đã hoàn thành triển khai ứng dụng nhé!\n👉 Vì đây là lần đầu nên bạn hãy điền thông tin đăng nhập mà bạn muốn để tạo tài khoản mới nhe!");
    });
}

/** Đăng nhập tài khoản */
function TryLogin() {
    if (dxForm_Account.validate().isValid)
        CallToServer("POST", "/Account/Login", false, undefined, undefined, {
            UserName: dxForm_Account?.getEditor("TenDangNhap")?.option("value")?.trim()?.toUpperCase(),
            UserPass: dxForm_Account?.getEditor("MatKhau")?.option("value")?.trim(),
            LastUrl: new URLSearchParams(window?.location?.search)?.get("ReturnUrl"), // Lấy đường dẫn trước khi trỏ về trang đăng nhập
        }, (data) => {
            window.location.replace(data);
        });
}

/** Tạo tài khoản cho lần đầu triển khai ứng dụng */
function CreateAccountForFirstTimeDeploy() {
    if (dxForm_Account.validate().isValid)
        CallToServer("POST", "/Account/CreateAccountFirstTime", false, "Tạo tài khoản thành công! Vui lòng đăng nhập lại!", undefined, {
            UserName: dxForm_Account?.getEditor("TenDangNhap")?.option("value")?.trim()?.toUpperCase(),
            UserPass: dxForm_Account?.getEditor("MatKhau")?.option("value")?.trim(),
        }, (data) => IsFirstTimeDeploy());
}

/** Chạy khi mở trang */
$(() => IsFirstTimeDeploy());

/** Form đăng nhập */
const dxForm_Account = $("#dxForm_Account")
    .dxForm({
        labelMode: "floating",
        optionalMark: "(nếu có)",
        requiredMessage: "{0} là bắt buộc",
        scrollingEnabled: true,
        showColonAfterLabel: true,
        showOptionalMark: true,
        items: [
            {
                label: { text: "Tên đăng nhập" },
                dataField: "TenDangNhap",
                editorOptions: {
                    mode: "password",
                    buttons: [
                        {
                            name: "showText",
                            location: "after",
                            options: {
                                stylingMode: "text",
                                icon: "eyeopen",
                                onClick: () => ChangeTextMode("TenDangNhap"),
                            },
                        },
                    ],
                },
                validationRules: [
                    {
                        type: "required",
                        message: "Tên đăng nhập là bắt buộc",
                    },
                ],
            },
            {
                label: { text: "Mật khẩu" },
                dataField: "MatKhau",
                editorOptions: {
                    mode: "password",
                    buttons: [
                        {
                            name: "showText",
                            location: "after",
                            options: {
                                stylingMode: "text",
                                icon: "eyeopen",
                                onClick: () => ChangeTextMode("MatKhau"),
                            },
                        },
                    ],
                },
                validationRules: [
                    {
                        type: "required",
                        message: "Mật khẩu là bắt buộc",
                    },
                    {
                        type: "pattern",
                        pattern:
                            /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&#\.])[A-Za-z\d@$!%*?&#\.]{8,}$/,
                        message:
                            "Mật khẩu chứa ít nhất 8 ký tự gồm: chữ hoa, chữ thường, số và ký tự đặc biệt",
                    },
                ],
            },
            {
                itemType: "button",
                name: "btn_Login",
                buttonOptions: {
                    icon: "login",
                    stylingMode: "contained",
                    text: "Đăng nhập",
                    type: "success",
                    width: "100%",
                    //onClick: () => TryLogin(),
                },
            },
        ],
        //onEditorEnterKey: () => TryLogin(),
    })
    .dxForm("instance");
