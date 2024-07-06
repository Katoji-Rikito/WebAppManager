/** @format */

// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// CÁC THIẾT LẬP MẶC ĐỊNH ---------------------------------------------------------------------------------------------------
// THÊM PADDING TOP CHO PHẦN HIỂN THỊ NỘI DUNG
document.addEventListener("DOMContentLoaded", function () {
	// Điều chỉnh padding-top của nội dung chính
	const adjustMainPaddingTop = () =>
		$("#mainContent")?.css("padding-top", $("#navbarApp").height() + "px");

	// Điều chỉnh padding-top khi tải trang
	adjustMainPaddingTop();

	// Điều chỉnh padding-top khi cửa sổ thay đổi kích thước
	window.addEventListener("resize", adjustMainPaddingTop);
});

// THỜI GIAN
moment.locale("vi");

// KHAI BÁO CÁC BIẾN ---------------------------------------------------------------------------------------------------
const urlLogin = "/Account/Login";
const urlLogout = "/Account/Logout";

// CÁC HÀM HỖ TRỢ ---------------------------------------------------------------------------------------------------
/**
 * Kiểm tra giá trị có trống hay rỗng không
 * @param {any} value Giá trị cần kiểm tra
 * @returns True nếu trống
 */
async function IsThisNullOrEmpty(value) {
	return value === undefined || value === null || value === "";
}

/**
 * Viết hoa chữ cái đầu tiên của chuỗi string
 * @param {string} value Chuỗi cần viết hoa
 * @returns Chuỗi đã viết hoa chữ cái đầu tiên
 */
async function CapitalizeString(value) {
	if (await IsThisNullOrEmpty(value)) return value;
	return value?.charAt(0)?.toUpperCase() + value?.slice(1);
}

// HIỂN THỊ THỜI GIAN THỰC
setInterval(
	async () =>
		$("#digitalClock")?.text(
			await CapitalizeString(moment().format("dddd, DD/MM/YYYY HH:mm:ss A")),
		),
	500,
);

// CÁC HÀM HỖ TRỢ FORM ---------------------------------------------------------------------------------------------------
/**
 * Thay đổi chế độ text và password
 * @param {string} id Tên trường cần thay đổi
 */
function ChangeTextMode(id) {
	const txtPass = dxForm_Account?.getEditor(id);
	const iconPass = txtPass?.getButton("showText");
	if (txtPass?.option("mode") === "text") {
		txtPass?.option("mode", "password");
		iconPass?.option("icon", "eyeopen");
	} else {
		txtPass?.option("mode", "text");
		iconPass?.option("icon", "eyeclose");
	}
}

/**
 * Lấy giá trị từ form
 * @param {any} form Form cần lấy giá trị
 * @param {string} id ID cần lấy giá trị
 * @returns Giá trị lấy được
 */
function GetEditorValue(form = null, id = "") {
	return form?.getEditor(id)?.option("value");
}

// MÀN HÌNH LOADING ---------------------------------------------------------------------------------------------------
const loadPanel = $("#dxLoadingPanel")
	?.dxLoadPanel({
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

$(document)
	.ajaxStart(async () => {
		$("#notifyText")?.text("(Đang tải dữ liệu . . .)");
		loadPanel?.show();
	})
	.ajaxStop(async () => {
		$("#notifyText")?.text("");
		loadPanel?.hide();
	});

// CÁC HÀM HỖ TRỢ GỌI SERVER ---------------------------------------------------------------------------------------------------
/**
 * Hàm lấy dữ liệu từ server
 * @param {string} callURL URL cần gọi
 * @param {boolean} showNotify Hiện thông báo không? True là hiện
 * @param {function} actionForSuccess Hàm sẽ thực thi nếu thành công
 */
function GetData(
	callURL = "",
	showNotify = false,
	actionForSuccess = null,
) {
	$.ajax({
		url: callURL,
		type: "GET",
		async: true,
		dataType: "json",
		success(result, status, xhr) {
			console.log(result);
			console.log(status);
			console.log(xhr);
			if (!IsThisNullOrEmpty(actionForSuccess)) actionForSuccess();
		},
		error(xhr, status, error) {
			console.log(error);
			console.log(status);
			console.log(xhr);
			if (!IsThisNullOrEmpty(actionForError)) actionForError();
		},
	});
}

/**
 * Hàm gửi dữ liệu về server
 * @param {string} callURL URL cần gọi
 * @param {boolean} showNotify Hiện thông báo không? True là hiện
 * @param {object} args Tham số cần gửi về server
 * @param {function} actionForSuccess Hàm sẽ thực thi nếu thành công
 */
function PostData(
	callURL = "",
	showNotify = false,
	args = [],
	actionForSuccess = null,
) {
	$.ajax({
		url: callURL,
		type: "POST",
		async: true,
		dataType: "json",
		data: args,
		success(result, status, xhr) {
			console.log(result);
			console.log(status);
			console.log(xhr);
			if (!IsThisNullOrEmpty(actionForSuccess)) actionForSuccess();
		},
		error(xhr, status, error) {
			console.log(error);
			console.log(status);
			console.log(xhr);
			if (!IsThisNullOrEmpty(actionForError)) actionForError();
		},
	});
}

// FORM ĐĂNG NHẬP ---------------------------------------------------------------------------------------------------
const dxForm_Account = $("#dxForm_Account")
	?.dxForm({
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
					onClick: () => {
						if (dxForm_Account.validate().isValid)
							PostData(urlLogin, false, {
								userName: GetEditorValue(dxForm_Account, "TenDangNhap"),
								userPass: GetEditorValue(dxForm_Account, "MatKhau"),
							});
					},
				},
			},
		],
	})
	.dxForm("instance");
