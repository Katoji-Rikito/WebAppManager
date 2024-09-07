CREATE TABLE IF NOT EXISTS `ds_lichsuthaydoi` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `LoaiThayDoi` tinytext NOT NULL,
  `TenBang` text NOT NULL,
  `IdDong` bigint(20) unsigned DEFAULT NULL,
  `TenCot` text NOT NULL,
  `GiaTriCu` text DEFAULT NULL,
  `GiaTriMoi` text DEFAULT NULL,
  `IsAble` tinyint(1) NOT NULL DEFAULT 1 COMMENT 'Hết hiệu lực: 0, có hiệu lực: 1',
  `UpdatedAt` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT 'Thời gian cập nhật dữ liệu',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_unicode_ci COMMENT='Danh sách lịch sử thay đổi cơ sở dữ liệu';

CREATE TABLE IF NOT EXISTS `ds_taikhoan` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `TenDangNhap` tinytext NOT NULL,
  `MatKhau` text NOT NULL,
  `HashSalt` tinytext NOT NULL,
  `IsAble` tinyint(1) NOT NULL DEFAULT 1 COMMENT 'Hết hiệu lực: 0, có hiệu lực: 1',
  `UpdatedAt` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT 'Thời gian cập nhật dữ liệu',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `TenDangNhap` (`TenDangNhap`) USING HASH
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_unicode_ci COMMENT='Danh sách tài khoản dùng để đăng nhập ứng dụng';


CREATE TABLE IF NOT EXISTS `DM_DiaChi` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `IdCha` bigint(20) unsigned NULL,
  `TenCap` tinytext NOT NULL,
  `TenDiaChi` tinytext NOT NULL,
  `GhiChu` text NULL,
  `IsAble` tinyint(1) NOT NULL DEFAULT 1 COMMENT 'Hết hiệu lực: 0, có hiệu lực: 1',
  `UpdatedAt` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT 'Thời gian cập nhật dữ liệu',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_unicode_ci COMMENT='Danh mục địa chỉ';
