-- MySQL dump 10.13  Distrib 5.5.39, for Win64 (x86)
--
-- Host: localhost    Database: qtud
-- ------------------------------------------------------
-- Server version	5.5.39

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
-- Table structure for table `tb_patient_info`
--

DROP TABLE IF EXISTS `tb_patient_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_patient_info` (
  `uuid` varchar(36) NOT NULL,
  `cardid` varchar(128) NOT NULL COMMENT '身份证号,加密存储',
  `name` varchar(32) NOT NULL,
  `sex` tinyint(1) unsigned zerofill DEFAULT NULL COMMENT '性别 0女 1男',
  `phone` varchar(16) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL,
  `lastchecktime` datetime DEFAULT NULL COMMENT '最后一次检查时间',
  `meno` varchar(64) DEFAULT NULL,
  PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_patient_info`
--

LOCK TABLES `tb_patient_info` WRITE;
/*!40000 ALTER TABLE `tb_patient_info` DISABLE KEYS */;
INSERT INTO `tb_patient_info` VALUES ('298E7F50C57340E9825F2CD0B60FDC63','REUtQTktQjctMkItRDAtOTYtMUQtQ0QtMEUtM0YtMEQtNkItNDgtMzktNjEtRDMtOUItQUEtNEEtQUMtMzQtMjUtQkMtNEM=','杨过韩',1,'13799005588','2017-07-15 22:42:50','2017-07-15 22:42:50','测试'),('648E9BD766184E4C9BFD1BD2BA0B6E08','Q0EtQkEtNzktOTctNUUtRjYtOEUtNjQtOTAtRDctODctRUUtQjItMTYtNUYtMkMtRTUtM0MtQTMtRUEtNTQtRTMtQzItMzk=','张三',1,'13300001111','2017-07-15 15:23:29','2017-07-15 15:23:29','测试22'),('6F089539E2D4460EB3B609E97047EF4E','RDgtRDYtNkUtNUItQkYtMzEtRjEtMzItMzMtNDctQTMtOUEtN0YtRTctQjgtRkEtNTctRjUtODktRjYtNDktNUMtMjEtMkU=','欧阳岩轩',1,'13899996664','2017-07-15 15:34:19','2017-07-15 15:34:19','测试'),('C2BD4BA1BE4B4FE9B17D7657AD1227DC','NDYtMEYtRDktODYtNjUtMzQtQzEtNzYtQTAtOTEtNTAtQTctNEItQjEtNDMtMUUtRjctMDUtQ0ItNzAtOEQtRUQtMkQtN0Y=','张唯亭',0,'13588889992','2017-07-15 15:29:48','2017-07-15 15:29:48','测试4177');
/*!40000 ALTER TABLE `tb_patient_info` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_user`
--

DROP TABLE IF EXISTS `tb_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_user` (
  `user_id` varchar(64) NOT NULL COMMENT 'Id',
  `user_name` varchar(32) DEFAULT NULL COMMENT '用户名',
  `user_passwd` varchar(128) DEFAULT NULL COMMENT '用户口令',
  `user_status` smallint(2) DEFAULT NULL COMMENT '状态: 1正常，2冻结',
  `user_class` smallint(2) DEFAULT NULL COMMENT '级别：1系统管理员，2普通用户',
  `user_phone` varchar(16) DEFAULT NULL,
  `user_createtime` datetime DEFAULT NULL COMMENT '创建时间',
  `user_lastlogintime` datetime DEFAULT NULL COMMENT '最后登录时间',
  `user_meno` varchar(128) DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_user`
--

LOCK TABLES `tb_user` WRITE;
/*!40000 ALTER TABLE `tb_user` DISABLE KEYS */;
INSERT INTO `tb_user` VALUES ('1','admin','1',1,1,NULL,NULL,'2017-07-23 22:24:20',NULL);
/*!40000 ALTER TABLE `tb_user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-07-24  6:20:05
