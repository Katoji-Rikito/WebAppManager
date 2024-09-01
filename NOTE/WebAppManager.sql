-- --------------------------------------------------------
-- Máy chủ:                      127.0.0.1
-- Server version:               11.5.0-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Phiên bản:           12.6.0.6765
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for webappmanager
CREATE DATABASE IF NOT EXISTS `webappmanager` /*!40100 DEFAULT CHARACTER SET utf32 COLLATE utf32_unicode_ci */;
USE `webappmanager`;

-- Dumping structure for table webappmanager.dm_tagnhom
CREATE TABLE IF NOT EXISTS `dm_tagnhom` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `TenTagNhom` text NOT NULL,
  `GhiChu` text DEFAULT NULL,
  `IsAble` tinyint(1) NOT NULL DEFAULT 1 COMMENT 'Hết hiệu lực: 0, có hiệu lực: 1',
  `UpdatedAt` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT 'Thời gian cập nhật dữ liệu',
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE KEY `TenTagNhom` (`TenTagNhom`) USING HASH
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_unicode_ci COMMENT='Danh mục các tag, nhóm chung cho nhiều bảng';

-- Data exporting was unselected.

-- Dumping structure for table webappmanager.ds_lichsuthaydoi
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

-- Data exporting was unselected.

-- Dumping structure for table webappmanager.ds_nguontien
CREATE TABLE IF NOT EXISTS `ds_nguontien` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `IdNhomTien` bigint(20) unsigned NOT NULL,
  `TenNguonTien` tinytext NOT NULL,
  `DonGia` bigint(20) unsigned NOT NULL DEFAULT 0,
  `SoLuong` bigint(20) unsigned NOT NULL DEFAULT 1,
  `IsAble` tinyint(1) NOT NULL DEFAULT 1 COMMENT 'Hết hiệu lực: 0, có hiệu lực: 1',
  `UpdatedAt` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT 'Thời gian cập nhật dữ liệu',
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE KEY `TenNguonTien` (`TenNguonTien`) USING HASH,
  KEY `FK_DsNguonTien_DmTagNhom` (`IdNhomTien`),
  CONSTRAINT `FK_DsNguonTien_DmTagNhom` FOREIGN KEY (`IdNhomTien`) REFERENCES `dm_tagnhom` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_unicode_ci COMMENT='Danh sách nguồn tiền';

-- Data exporting was unselected.

-- Dumping structure for table webappmanager.ds_taikhoan
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

-- Data exporting was unselected.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
