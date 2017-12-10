-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: databaseproject
-- ------------------------------------------------------
-- Server version	5.7.20-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `god`
--

DROP TABLE IF EXISTS `god`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `god` (
  `id` int(11) NOT NULL,
  `name` varchar(45) NOT NULL,
  `partner` varchar(45) NOT NULL,
  `cycle` varchar(45) NOT NULL,
  `festival` varchar(45) NOT NULL,
  `book` varchar(45) NOT NULL,
  `affiliation` varchar(45) NOT NULL,
  `weapon` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `god`
--

LOCK TABLES `god` WRITE;
/*!40000 ALTER TABLE `god` DISABLE KEYS */;
INSERT INTO `god` VALUES (1,'rama','sita','treta yuga','ramanavami','ramayana','seventh avatar','bow and arrow'),(2,'krishna','radha','dvapara yuga','krishna janmasthami','bhagavadgita','supreme soul','sudarshan chakar'),(3,'hanuman','rama and sita','treta yuga','hanuman jayanthi','ramayana','devotee','gada'),(4,'ganesha','shiva','satya yuga','ganesha chaturthi','shiva purana','deva','axe'),(5,'shiva','parvathi','satya yuga','mahashivaratri','shiva purana','supreme being','trishula'),(6,'narasimha','prahlada','satya yuga','narasimha jayanthi','puranas','forth avatar','chakra'),(7,'venkateswara','padmavathi','satya yuga','brahmotsavam','puranas','ninth avatar','shanka and chakra');
/*!40000 ALTER TABLE `god` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-12-03 19:41:44
