-- MySQL dump 10.13  Distrib 8.0.18, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: test
-- ------------------------------------------------------
-- Server version	5.5.62

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `oferty`
--

DROP TABLE IF EXISTS `oferty`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `oferty` (
  `ID_oferty` int(10) unsigned NOT NULL DEFAULT '0',
  `Miasto` char(60) NOT NULL DEFAULT '',
  `Tytul` char(130) NOT NULL DEFAULT '',
  `Ilosc_pokoi` int(11) NOT NULL DEFAULT '0',
  `Powierzchnia` decimal(8,2) NOT NULL DEFAULT '0.00',
  `Cena` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID_oferty`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `oferty`
--

LOCK TABLES `oferty` WRITE;
/*!40000 ALTER TABLE `oferty` DISABLE KEYS */;
INSERT INTO `oferty` VALUES (1,'Krakow','Oferta student',3,10.50,1800),(2,'Krakow','Okazja para',2,8.35,1500),(3,'Krakow','Bieda az piszczy',1,2.50,650),(4,'Tarnow','Piwo za free',3,9.25,1200),(5,'Krakow','Biedronka w poblizu',2,7.30,1400),(6,'Krakow','Nie ma sklepu',1,4.00,1100),(7,'Krakow','Co za widoki',6,170.50,5500);
/*!40000 ALTER TABLE `oferty` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-11-29  2:23:01
