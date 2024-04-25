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
CREATE DATABASE IF NOT EXISTS `webappmanager` /*!40100 DEFAULT CHARACTER SET utf32 COLLATE utf32_vietnamese_ci */;
USE `webappmanager`;

-- Dumping structure for table webappmanager.dm_diagioihanhchinh
CREATE TABLE IF NOT EXISTS `dm_diagioihanhchinh` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `IdCapTren` bigint(20) unsigned DEFAULT NULL,
  `IdNhomCap` bigint(20) unsigned NOT NULL,
  `TenCap` tinytext DEFAULT NULL,
  `TenDiaGioi` tinytext NOT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT 1,
  `UpdatedAt` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`ID`),
  UNIQUE KEY `TenDiaGioi` (`TenDiaGioi`) USING HASH,
  KEY `FK_DMDiaGioiHanhChinh_DMDiaGioiHanhChinh` (`IdCapTren`),
  KEY `FK_DMDiaGioiHanhChinh_DMTagNhom` (`IdNhomCap`),
  CONSTRAINT `FK_DMDiaGioiHanhChinh_DMDiaGioiHanhChinh` FOREIGN KEY (`IdCapTren`) REFERENCES `dm_diagioihanhchinh` (`ID`),
  CONSTRAINT `FK_DMDiaGioiHanhChinh_DMTagNhom` FOREIGN KEY (`IdNhomCap`) REFERENCES `dm_tagnhom` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- Data exporting was unselected.

-- Dumping structure for table webappmanager.dm_giaphong
CREATE TABLE IF NOT EXISTS `dm_giaphong` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `TenGiaPhong` tinytext NOT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT 1,
  `UpdatedAt` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`ID`),
  UNIQUE KEY `TenGiaPhong` (`TenGiaPhong`) USING HASH
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- Data exporting was unselected.

-- Dumping structure for table webappmanager.dm_khoanchi
CREATE TABLE IF NOT EXISTS `dm_khoanchi` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `TenKhoanChi` tinytext NOT NULL,
  `DonViTinh` tinytext NOT NULL,
  `DiaChi` text DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT 1,
  `UpdatedAt` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`ID`),
  UNIQUE KEY `TenKhoanChi` (`TenKhoanChi`) USING HASH
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- Data exporting was unselected.

-- Dumping structure for table webappmanager.dm_pass
CREATE TABLE IF NOT EXISTS `dm_pass` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `TenPass` tinytext NOT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT 1,
  `UpdatedAt` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`ID`),
  UNIQUE KEY `TenPass` (`TenPass`) USING HASH
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- Data exporting was unselected.

-- Dumping structure for table webappmanager.dm_tagnhom
CREATE TABLE IF NOT EXISTS `dm_tagnhom` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `TenTag` text NOT NULL,
  `GhiChu` text DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT 1,
  `UpdatedAt` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`ID`),
  UNIQUE KEY `TenTag` (`TenTag`) USING HASH
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- Data exporting was unselected.

-- Dumping structure for table webappmanager.ds_chitieu
CREATE TABLE IF NOT EXISTS `ds_chitieu` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `IdKhoanChi` bigint(20) unsigned NOT NULL,
  `NgayThang` date NOT NULL,
  `DonGia` bigint(20) unsigned NOT NULL DEFAULT 0,
  `SoLuong` tinyint(3) unsigned NOT NULL DEFAULT 1,
  `LaKhoanNhan` tinyint(1) NOT NULL DEFAULT 0,
  `GhiChu` text DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT 1,
  `UpdatedAt` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`ID`),
  KEY `FK_DSChiTieu_DMKhoanChi` (`IdKhoanChi`),
  CONSTRAINT `FK_DSChiTieu_DMKhoanChi` FOREIGN KEY (`IdKhoanChi`) REFERENCES `dm_khoanchi` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- Data exporting was unselected.

-- Dumping structure for table webappmanager.ds_diachi
CREATE TABLE IF NOT EXISTS `ds_diachi` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `IdDiaGioi` bigint(20) unsigned NOT NULL,
  `DiaChiChiTiet` text DEFAULT NULL,
  `GhiChu` text DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT 1,
  `UpdatedAt` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`ID`),
  KEY `FK_DSDiaChi_DMDiaGioiHanhChinh` (`IdDiaGioi`),
  CONSTRAINT `FK_DSDiaChi_DMDiaGioiHanhChinh` FOREIGN KEY (`IdDiaGioi`) REFERENCES `dm_diagioihanhchinh` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- Data exporting was unselected.

-- Dumping structure for table webappmanager.ds_nguontien
CREATE TABLE IF NOT EXISTS `ds_nguontien` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `TenNguonTien` tinytext NOT NULL,
  `IdNhomTien` bigint(20) unsigned NOT NULL,
  `DonGia` bigint(20) unsigned NOT NULL DEFAULT 0,
  `SoLuong` bigint(20) unsigned NOT NULL DEFAULT 1,
  `IsActive` tinyint(1) NOT NULL DEFAULT 1,
  `UpdatedAt` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`ID`),
  UNIQUE KEY `TenNguonTien` (`TenNguonTien`) USING HASH,
  KEY `FK_DSNguonTien_DMTagNhom` (`IdNhomTien`),
  CONSTRAINT `FK_DSNguonTien_DMTagNhom` FOREIGN KEY (`IdNhomTien`) REFERENCES `dm_tagnhom` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- Data exporting was unselected.

-- Dumping structure for table webappmanager.ds_taikhoan
CREATE TABLE IF NOT EXISTS `ds_taikhoan` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `TenDangNhap` tinytext NOT NULL,
  `MatKhau` text NOT NULL,
  `HashSalt` tinytext NOT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT 1,
  `UpdatedAt` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`ID`),
  UNIQUE KEY `TenDangNhap` (`TenDangNhap`) USING HASH
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- Data exporting was unselected.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
