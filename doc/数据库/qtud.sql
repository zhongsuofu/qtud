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
INSERT INTO `tb_patient_info` VALUES ('298E7F50C57340E9825F2CD0B60FDC63','REUtQTktQjctMkItRDAtOTYtMUQtQ0QtMEUtM0YtMEQtNkItNDgtMzktNjEtRDMtOUItQUEtNEEtQUMtMzQtMjUtQkMtNEM=','杨过韩',1,'13799005588','2017-07-15 22:42:50','2017-07-15 22:42:50','测试'),('648E9BD766184E4C9BFD1BD2BA0B6E08','Q0EtQkEtNzktOTctNUUtRjYtOEUtNjQtOTAtRDctODctRUUtQjItMTYtNUYtMkMtRTUtM0MtQTMtRUEtNTQtRTMtQzItMzk=','张三',1,'13300001111','2017-07-15 15:23:29','2017-07-15 15:23:29','测试22'),('6F089539E2D4460EB3B609E97047EF4E','RDgtRDYtNkUtNUItQkYtMzEtRjEtMzItMzMtNDctQTMtOUEtN0YtRTctQjgtRkEtNTctRjUtODktRjYtNDktNUMtMjEtMkU=','欧阳岩轩',1,'13899996664','2017-07-15 15:34:19','2017-07-15 15:34:19','测试'),('99B79D0375CB480BB24BF9E38EB09C2F','MTQtNkQtMzItRjktRkMtM0ItQzItQTQtMEYtNzctNkYtOTItQjMtMzUtQkItMDMtNTgtQkQtMUEtMDctMEQtNEItNzAtREU=','gggg',1,'2222','2017-08-01 21:54:03','2017-08-01 21:54:03','fff'),('C2BD4BA1BE4B4FE9B17D7657AD1227DC','NDYtMEYtRDktODYtNjUtMzQtQzEtNzYtQTAtOTEtNTAtQTctNEItQjEtNDMtMUUtRjctMDUtQ0ItNzAtOEQtRUQtMkQtN0Y=','张唯亭',0,'13588889992','2017-07-15 15:29:48','2017-07-15 15:29:48','测试4177');
/*!40000 ALTER TABLE `tb_patient_info` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_report_info`
--

DROP TABLE IF EXISTS `tb_report_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_report_info` (
  `uuid` varchar(64) NOT NULL DEFAULT '',
  `name` varchar(32) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `patient_uuid` varchar(64) DEFAULT NULL COMMENT '病人uuid',
  `ks` varchar(16) DEFAULT NULL COMMENT '科室',
  `ch` varchar(3) DEFAULT NULL COMMENT '床号',
  `nlljcjg` float DEFAULT NULL COMMENT '尿流量检查结果',
  `pgrlylcd` float DEFAULT NULL COMMENT '充盈期膀胱容量-压力测定结果',
  `pgrl_cg` float DEFAULT NULL COMMENT '膀胱容量-初感',
  `pgrl_zc` float DEFAULT NULL COMMENT '膀胱容量-正常',
  `pgrl_zd` float DEFAULT NULL COMMENT '膀胱容量-最大',
  `pgsyx` varchar(8) DEFAULT NULL COMMENT '膀胱顺应性',
  `pgwdx` varchar(8) DEFAULT NULL COMMENT '膀胱稳定性',
  `tsjc` varchar(16) DEFAULT NULL COMMENT '特殊检查',
  `vlpp` float DEFAULT NULL,
  `dlpp` float DEFAULT NULL,
  `clpp` float DEFAULT NULL,
  `pgaqrl` float DEFAULT NULL,
  `otherInfo` varchar(256) DEFAULT NULL,
  `testresult` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_report_info`
--

LOCK TABLES `tb_report_info` WRITE;
/*!40000 ALTER TABLE `tb_report_info` DISABLE KEYS */;
INSERT INTO `tb_report_info` VALUES ('23C276E2866341A8ADE02E5CCE49420B','990002198808052314','2017-08-03 05:24:01','99B79D0375CB480BB24BF9E38EB09C2F','ks','2',0,2,3,4,6,'正常','正常','',7,8,9,0,'fff','eeee'),('6670E35E995941FFB3AC35A434748553','110201198801274177','2017-08-03 05:27:45','C2BD4BA1BE4B4FE9B17D7657AD1227DC','泌尿科','3',1,2.3,122,333,444,'高顺应性','逼尿肌活动过度','ee',0,0,0,0,'fes','ffe'),('CAFB297942E844B5A988776D5EEDA0F6','110201198801274177','2017-08-03 05:27:44','C2BD4BA1BE4B4FE9B17D7657AD1227DC','泌尿科','3',1,2.3,122,333,444,'高顺应性','逼尿肌活动过度','ee',0,0,0,0,'fes','ffe');
/*!40000 ALTER TABLE `tb_report_info` ENABLE KEYS */;
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
INSERT INTO `tb_user` VALUES ('1','admin','1',1,1,NULL,NULL,'2017-08-03 05:38:05',NULL);
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

-- Dump completed on 2017-08-03  5:43:12
