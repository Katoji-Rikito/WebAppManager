/** @format */

// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

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

/**
 * Đăng nhập tài khoản
 */
function TryLogin() {
	if (dxForm_Account.validate().isValid)
		CallServer_POST(
			"/Account/Login",
			false,
			{
				UserName: dxForm_Account
					?.getEditor("TenDangNhap")
					?.option("value")
					?.trim()
					?.toUpperCase(),
				UserPass: dxForm_Account?.getEditor("MatKhau")?.option("value")?.trim(),
				LastUrl: new URLSearchParams(window?.location?.search)?.get(
					"ReturnUrl",
				), // Lấy đường dẫn trước khi trỏ về trang đăng nhập
			},
			(data) => {
				window.location.replace(data);
			},
		);
}

// FORM ĐĂNG NHẬP ---------------------------------------------------------------------------------------------------
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
						message: "Tên đăng nhập là bắt buộc",
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
				buttonOptions: {
					icon: "login",
					stylingMode: "contained",
					text: "Đăng nhập",
					type: "success",
					width: "100%",
					onClick: function (e) {
						TryLogin();
					},
				},
			},
		],
		onEditorEnterKey: function (e) {
			TryLogin();
		},
	})
	.dxForm("instance");
