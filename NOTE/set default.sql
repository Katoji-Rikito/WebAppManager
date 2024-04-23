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

-- Dumping structure for table webappmanager.ds_nguontien
CREATE TABLE IF NOT EXISTS `ds_nguontien` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `TenNguonTien` tinytext NOT NULL,
  `IdNhomTien` bigint(20) unsigned NOT NULL,
  `DonGia` bigint(20) unsigned NOT NULL DEFAULT 0,
  `SoLuong` bigint(20) unsigned NOT NULL DEFAULT 1,
  `IsActive` tinyint(1) NOT NULL DEFAULT 0,
  `UpdatedAt` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`ID`),
  UNIQUE KEY `TenNguonTien` (`TenNguonTien`) USING HASH,
  KEY `FK_DSNguonTien_DMTagNhom` (`IdNhomTien`),
  CONSTRAINT `FK_DSNguonTien_DMTagNhom` FOREIGN KEY (`IdNhomTien`) REFERENCES `dm_tagnhom` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- Data exporting was unselected.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
