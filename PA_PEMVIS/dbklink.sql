-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 17, 2024 at 11:14 AM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dbklink`
--

-- --------------------------------------------------------

--
-- Table structure for table `tbakun`
--

CREATE TABLE `tbakun` (
  `id` int(3) NOT NULL,
  `nama` text NOT NULL,
  `tglLahir` date NOT NULL,
  `jenisKelamin` text NOT NULL,
  `username` text NOT NULL,
  `password` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbakun`
--

INSERT INTO `tbakun` (`id`, `nama`, `tglLahir`, `jenisKelamin`, `username`, `password`) VALUES
(8, 'Han Jisung', '2000-09-14', 'Laki-Laki', 'hanpoem', '123'),
(9, 'Bang Chan', '2024-05-10', 'Laki-Laki', 'ngabchan', '111'),
(10, 'Lee Felix', '2000-09-15', 'Laki-Laki', 'yonglix', '1212'),
(18, 'Vedra Dian', '2005-01-17', 'Perempuan', 'rara', '123');

-- --------------------------------------------------------

--
-- Table structure for table `tbjadwaldokter`
--

CREATE TABLE `tbjadwaldokter` (
  `idDokter` varchar(5) NOT NULL,
  `namaDokter` text NOT NULL,
  `jam` text NOT NULL,
  `tanggal` date NOT NULL,
  `spesialis` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbjadwaldokter`
--

INSERT INTO `tbjadwaldokter` (`idDokter`, `namaDokter`, `jam`, `tanggal`, `spesialis`) VALUES
('JW0', 'Teresia Putri Sp.Kj', '18.00-18.45, 15.00-15.45, 14.00-14.45', '2024-05-20', 'Jiwa'),
('MT0', 'Dony Mulya Sp.M', '14.00-14.45, 11.00-12.00', '2024-05-21', 'Mata'),
('PD0', 'Dio Dharmaesa Sp.PD', '15.00-15.45, 10.00-10.45', '2024-05-22', 'Penyakit Dalam'),
('U0', 'dr. Yohanes Don Bosco', '19.00-19.45, 15.00-15.45', '2024-05-21', 'Umum');

-- --------------------------------------------------------

--
-- Table structure for table `tbjanjikonsul`
--

CREATE TABLE `tbjanjikonsul` (
  `idJanji` int(11) NOT NULL,
  `idAkun` int(3) DEFAULT NULL,
  `idDokter` varchar(5) DEFAULT NULL,
  `tanggal` date DEFAULT NULL,
  `jam` text DEFAULT NULL,
  `catatan` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbjanjikonsul`
--

INSERT INTO `tbjanjikonsul` (`idJanji`, `idAkun`, `idDokter`, `tanggal`, `jam`, `catatan`) VALUES
(32, 18, 'U0', '2024-05-21', '19.00-19.45', 'Minum Obat di jam 9 pagi sehabis sarapan'),
(33, 18, 'PD0', '2024-05-22', '15.00-15.45', NULL),
(34, 8, 'U0', '2024-05-21', '15.00-15.45', NULL),
(35, 8, 'PD0', '2024-05-22', '15.00-15.45', NULL);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tbakun`
--
ALTER TABLE `tbakun`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `tbjadwaldokter`
--
ALTER TABLE `tbjadwaldokter`
  ADD PRIMARY KEY (`idDokter`);

--
-- Indexes for table `tbjanjikonsul`
--
ALTER TABLE `tbjanjikonsul`
  ADD PRIMARY KEY (`idJanji`),
  ADD KEY `idAkun` (`idAkun`),
  ADD KEY `fk_tbjanjikonsul_idDokter` (`idDokter`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tbakun`
--
ALTER TABLE `tbakun`
  MODIFY `id` int(3) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `tbjanjikonsul`
--
ALTER TABLE `tbjanjikonsul`
  MODIFY `idJanji` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `tbjanjikonsul`
--
ALTER TABLE `tbjanjikonsul`
  ADD CONSTRAINT `fk_tbjanjikonsul_idDokter` FOREIGN KEY (`idDokter`) REFERENCES `tbjadwaldokter` (`idDokter`),
  ADD CONSTRAINT `tbjanjikonsul_ibfk_1` FOREIGN KEY (`idAkun`) REFERENCES `tbakun` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
