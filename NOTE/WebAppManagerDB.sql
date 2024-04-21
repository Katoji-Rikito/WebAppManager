-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th4 21, 2024 lúc 06:56 PM
-- Phiên bản máy phục vụ: 10.4.32-MariaDB
-- Phiên bản PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+07:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `webappmanager`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `dm_diagioihanhchinh`
--

CREATE TABLE `dm_diagioihanhchinh` (
  `ID` bigint(20) UNSIGNED NOT NULL,
  `IdCapTren` bigint(20) UNSIGNED DEFAULT NULL,
  `IdNhomCap` bigint(20) UNSIGNED NOT NULL,
  `TenCap` tinytext DEFAULT NULL,
  `TenDiaGioi` tinytext NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `UpdatedAt` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `dm_giaphong`
--

CREATE TABLE `dm_giaphong` (
  `ID` bigint(20) UNSIGNED NOT NULL,
  `TenGiaPhong` tinytext NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `UpdatedAt` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `dm_khoanchi`
--

CREATE TABLE `dm_khoanchi` (
  `ID` bigint(20) UNSIGNED NOT NULL,
  `TenKhoanChi` tinytext NOT NULL,
  `DonViTinh` tinytext NOT NULL,
  `DiaChi` text DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `UpdatedAt` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `dm_pass`
--

CREATE TABLE `dm_pass` (
  `ID` bigint(20) UNSIGNED NOT NULL,
  `TenPass` tinytext NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `UpdatedAt` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `dm_tagnhom`
--

CREATE TABLE `dm_tagnhom` (
  `ID` bigint(20) UNSIGNED NOT NULL,
  `TenTag` text NOT NULL,
  `GhiChu` text DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `UpdatedAt` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `ds_chitieu`
--

CREATE TABLE `ds_chitieu` (
  `ID` bigint(20) UNSIGNED NOT NULL,
  `IdKhoanChi` bigint(20) UNSIGNED NOT NULL,
  `NgayThang` date NOT NULL,
  `DonGia` bigint(20) UNSIGNED NOT NULL,
  `SoLuong` tinyint(3) UNSIGNED NOT NULL,
  `LaKhoanNhan` tinyint(1) NOT NULL,
  `GhiChu` text DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `UpdatedAt` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `ds_diachi`
--

CREATE TABLE `ds_diachi` (
  `ID` bigint(20) UNSIGNED NOT NULL,
  `IdDiaGioi` bigint(20) UNSIGNED NOT NULL,
  `DiaChiChiTiet` text NOT NULL,
  `GhiChu` text DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `UpdatedAt` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `ds_nguontien`
--

CREATE TABLE `ds_nguontien` (
  `ID` bigint(20) UNSIGNED NOT NULL,
  `TenNguonTien` tinytext NOT NULL,
  `IdNhomTien` bigint(20) UNSIGNED NOT NULL,
  `DonGia` bigint(20) UNSIGNED NOT NULL,
  `SoLuong` bigint(20) UNSIGNED NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `UpdatedAt` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `ds_taikhoan`
--

CREATE TABLE `ds_taikhoan` (
  `ID` bigint(20) UNSIGNED NOT NULL,
  `TenDangNhap` tinytext NOT NULL,
  `MatKhau` text NOT NULL,
  `HashSalt` tinytext NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `UpdatedAt` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_vietnamese_ci;

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `dm_diagioihanhchinh`
--
ALTER TABLE `dm_diagioihanhchinh`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `ID` (`ID`),
  ADD UNIQUE KEY `TenDiaGioi` (`TenDiaGioi`) USING HASH,
  ADD KEY `FK_DMDiaGioiHanhChinh_DMDiaGioiHanhChinh` (`IdCapTren`),
  ADD KEY `FK_DMDiaGioiHanhChinh_DMTagNhom` (`IdNhomCap`);

--
-- Chỉ mục cho bảng `dm_giaphong`
--
ALTER TABLE `dm_giaphong`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `ID` (`ID`),
  ADD UNIQUE KEY `TenGiaPhong` (`TenGiaPhong`) USING HASH;

--
-- Chỉ mục cho bảng `dm_khoanchi`
--
ALTER TABLE `dm_khoanchi`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `ID` (`ID`),
  ADD UNIQUE KEY `TenKhoanChi` (`TenKhoanChi`) USING HASH;

--
-- Chỉ mục cho bảng `dm_pass`
--
ALTER TABLE `dm_pass`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `ID` (`ID`),
  ADD UNIQUE KEY `TenPass` (`TenPass`) USING HASH;

--
-- Chỉ mục cho bảng `dm_tagnhom`
--
ALTER TABLE `dm_tagnhom`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `ID` (`ID`),
  ADD UNIQUE KEY `TenTag` (`TenTag`) USING HASH;

--
-- Chỉ mục cho bảng `ds_chitieu`
--
ALTER TABLE `ds_chitieu`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `ID` (`ID`),
  ADD KEY `FK_DSChiTieu_DMKhoanChi` (`IdKhoanChi`);

--
-- Chỉ mục cho bảng `ds_diachi`
--
ALTER TABLE `ds_diachi`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `ID` (`ID`),
  ADD KEY `FK_DSDiaChi_DMDiaGioiHanhChinh` (`IdDiaGioi`);

--
-- Chỉ mục cho bảng `ds_nguontien`
--
ALTER TABLE `ds_nguontien`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `ID` (`ID`),
  ADD UNIQUE KEY `TenNguonTien` (`TenNguonTien`) USING HASH,
  ADD KEY `FK_DSNguonTien_DMTagNhom` (`IdNhomTien`);

--
-- Chỉ mục cho bảng `ds_taikhoan`
--
ALTER TABLE `ds_taikhoan`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `ID` (`ID`),
  ADD UNIQUE KEY `TenDangNhap` (`TenDangNhap`) USING HASH;

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `dm_diagioihanhchinh`
--
ALTER TABLE `dm_diagioihanhchinh`
  ADD CONSTRAINT `FK_DMDiaGioiHanhChinh_DMDiaGioiHanhChinh` FOREIGN KEY (`IDCapTren`) REFERENCES `dm_diagioihanhchinh` (`ID`),
  ADD CONSTRAINT `FK_DMDiaGioiHanhChinh_DMTagNhom` FOREIGN KEY (`IDNhomCap`) REFERENCES `dm_tagnhom` (`ID`);

--
-- Các ràng buộc cho bảng `ds_chitieu`
--
ALTER TABLE `ds_chitieu`
  ADD CONSTRAINT `FK_DSChiTieu_DMKhoanChi` FOREIGN KEY (`IDKhoanChi`) REFERENCES `dm_khoanchi` (`ID`);

--
-- Các ràng buộc cho bảng `ds_diachi`
--
ALTER TABLE `ds_diachi`
  ADD CONSTRAINT `FK_DSDiaChi_DMDiaGioiHanhChinh` FOREIGN KEY (`IDDiaGioi`) REFERENCES `dm_diagioihanhchinh` (`ID`);

--
-- Các ràng buộc cho bảng `ds_nguontien`
--
ALTER TABLE `ds_nguontien`
  ADD CONSTRAINT `FK_DSNguonTien_DMTagNhom` FOREIGN KEY (`IDNhomTien`) REFERENCES `dm_tagnhom` (`ID`);

--
-- AUTO_INCREMENT cho các bảng đã đổ
--

ALTER TABLE `dm_diagioihanhchinh`
  MODIFY `ID` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

ALTER TABLE `dm_giaphong`
  MODIFY `ID` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

ALTER TABLE `dm_khoanchi`
  MODIFY `ID` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

ALTER TABLE `dm_pass`
  MODIFY `ID` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

ALTER TABLE `dm_tagnhom`
  MODIFY `ID` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

ALTER TABLE `ds_chitieu`
  MODIFY `ID` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

ALTER TABLE `ds_diachi`
  MODIFY `ID` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

ALTER TABLE `ds_nguontien`
  MODIFY `ID` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

ALTER TABLE `ds_taikhoan`
  MODIFY `ID` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
