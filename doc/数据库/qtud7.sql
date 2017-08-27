/*
Navicat MySQL Data Transfer

Source Server         : qtud
Source Server Version : 50539
Source Host           : localhost:3306
Source Database       : qtud

Target Server Type    : MYSQL
Target Server Version : 50539
File Encoding         : 65001

Date: 2017-08-27 21:18:45
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `tbl_curve_file_link`
-- ----------------------------
DROP TABLE IF EXISTS `tbl_curve_file_link`;
CREATE TABLE `tbl_curve_file_link` (
  `curve_uuid` varchar(64) DEFAULT NULL,
  `file_uuid` varchar(64) DEFAULT NULL,
  `nindex` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tbl_curve_file_link
-- ----------------------------

-- ----------------------------
-- Table structure for `tbl_curve_info`
-- ----------------------------
DROP TABLE IF EXISTS `tbl_curve_info`;
CREATE TABLE `tbl_curve_info` (
  `uuid` varchar(64) NOT NULL DEFAULT '',
  `report_uuid` varchar(64) DEFAULT NULL COMMENT '曲线所属报告UUId',
  `starttime` datetime DEFAULT NULL,
  `endtime` datetime DEFAULT NULL,
  `ranges` varchar(32) DEFAULT NULL,
  `meno` varchar(128) DEFAULT NULL COMMENT '曲线说明',
  `nindex` tinyint(4) DEFAULT NULL,
  `strmode` varchar(16) DEFAULT NULL COMMENT '检查模式',
  PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tbl_curve_info
-- ----------------------------

-- ----------------------------
-- Table structure for `tbl_patient_checknum_file_info`
-- ----------------------------
DROP TABLE IF EXISTS `tbl_patient_checknum_file_info`;
CREATE TABLE `tbl_patient_checknum_file_info` (
  `uuid` varchar(64) NOT NULL DEFAULT '',
  `check_uuid` varchar(64) DEFAULT NULL COMMENT 'tbl_patient_checknum_link表的uuid',
  `path` varchar(128) DEFAULT NULL COMMENT '文件所在全路径',
  `checkmode` varchar(4) DEFAULT NULL COMMENT '检查类型',
  `createtime` datetime DEFAULT NULL,
  PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tbl_patient_checknum_file_info
-- ----------------------------

-- ----------------------------
-- Table structure for `tbl_patient_checknum_link`
-- ----------------------------
DROP TABLE IF EXISTS `tbl_patient_checknum_link`;
CREATE TABLE `tbl_patient_checknum_link` (
  `uuid` varchar(64) NOT NULL DEFAULT '',
  `patient_uuid` varchar(64) DEFAULT NULL COMMENT '病人UUID',
  `checkNum` varchar(20) DEFAULT NULL COMMENT '检查编号',
  `txtPath` varchar(64) DEFAULT NULL COMMENT 'txt文件路径',
  PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tbl_patient_checknum_link
-- ----------------------------

-- ----------------------------
-- Table structure for `tbl_print_info`
-- ----------------------------
DROP TABLE IF EXISTS `tbl_print_info`;
CREATE TABLE `tbl_print_info` (
  `uuid` varchar(64) NOT NULL DEFAULT '',
  `useruuid` varchar(64) DEFAULT NULL COMMENT '用户UUId',
  `ReportUUId` varchar(64) DEFAULT NULL COMMENT '报告UUID',
  `pagecnt` tinyint(4) DEFAULT NULL COMMENT '打印页数',
  `printDate` datetime DEFAULT NULL,
  `printcnt` tinyint(4) DEFAULT NULL COMMENT '打印份数',
  PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tbl_print_info
-- ----------------------------
INSERT INTO `tbl_print_info` VALUES ('0332CFBB31AE4E0DBD02A4642F471E27', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '1', '2017-08-06 15:01:14', '1');
INSERT INTO `tbl_print_info` VALUES ('10B6FC3568894A8FA1ECC1B27DF48A7C', '1', '49AA483E3EFB40159658377035DA2604', '2', '2017-08-22 20:58:17', '1');
INSERT INTO `tbl_print_info` VALUES ('16F54A5068214705AE7C78DCAA555827', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '1', '2017-08-06 15:01:22', '1');
INSERT INTO `tbl_print_info` VALUES ('212506F95A1C4A0C8B6E5FFAA02F91CC', '1', 'E74D8199B0684EF7BAEE7C10F5A08485', '2', '2017-08-19 11:01:42', '1');
INSERT INTO `tbl_print_info` VALUES ('258853B098634A718618D227962346AF', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '2', '2017-08-06 21:28:45', '1');
INSERT INTO `tbl_print_info` VALUES ('2D419C2A0C5A41A0B0875EF68F5A5CC8', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '2', '2017-08-06 21:10:27', '1');
INSERT INTO `tbl_print_info` VALUES ('3219DDB968AC4099BDDCBEB08144CA9F', '1', 'E74D8199B0684EF7BAEE7C10F5A08485', '2', '2017-08-19 11:01:25', '1');
INSERT INTO `tbl_print_info` VALUES ('353639ED35A94F33A0A8421941A237E9', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '2', '2017-08-06 21:28:24', '1');
INSERT INTO `tbl_print_info` VALUES ('6C455CFB29F5434EB395AC7522481A69', '1', 'E74D8199B0684EF7BAEE7C10F5A08485', '3', '2017-08-19 11:48:00', '1');
INSERT INTO `tbl_print_info` VALUES ('6D12BA15417F47EB90D5E93D901E312E', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '2', '2017-08-06 21:10:42', '1');
INSERT INTO `tbl_print_info` VALUES ('7BDF07AA6C9B400E85AE4A4F744F6790', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '2', '2017-08-06 21:10:04', '1');
INSERT INTO `tbl_print_info` VALUES ('878487D7012D47178A7F1B9A11A14CA2', '1', '49AA483E3EFB40159658377035DA2604', '5', '2017-08-22 22:58:39', '1');
INSERT INTO `tbl_print_info` VALUES ('8E32929FAC78484C983ED778C9374BFD', '1', '0C5DEB2405614D0A9A44C02878CB8D2C', '2', '2017-08-22 21:07:51', '1');
INSERT INTO `tbl_print_info` VALUES ('90F6EAD1B2D940B091503A5965733E2F', '1BF9700AC16146EB9F29C235674DD84F', '5A6B21AF30C5466F857535C197D2AB12', '3', '2017-08-27 21:04:20', '1');
INSERT INTO `tbl_print_info` VALUES ('98ADD694E3654DC897B86F66929B1C0C', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '1', '2017-08-05 21:21:26', '1');
INSERT INTO `tbl_print_info` VALUES ('A9E6F2CB15F34A81BDE84F92063E492A', '1', '49AA483E3EFB40159658377035DA2604', '1', '2017-08-22 22:07:15', '1');
INSERT INTO `tbl_print_info` VALUES ('B2D2A8CFA0A34F43910082434060B8DA', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '1', '2017-08-05 21:21:07', '1');
INSERT INTO `tbl_print_info` VALUES ('BA63E4A4213E4E038EB887E3FBC2A851', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '0', '2017-08-05 19:41:14', '1');
INSERT INTO `tbl_print_info` VALUES ('CFA63CC24F2346168820A683E88BA437', '1', '6BFF543CEC184543B6844AD70CD361EF', '2', '2017-08-06 21:37:10', '1');
INSERT INTO `tbl_print_info` VALUES ('D6482333BEB64057AB7DD2A508F74A4D', '1', 'E74D8199B0684EF7BAEE7C10F5A08485', '3', '2017-08-19 13:08:52', '1');
INSERT INTO `tbl_print_info` VALUES ('ED28562F9EEB4D5381E35AB346D39EB6', '1', '3EE61C9447264130B19186AA5C80F962', '4', '2017-08-20 14:38:55', '1');
INSERT INTO `tbl_print_info` VALUES ('F5589A8BC1B44B9E8CFBE1E1E46F4456', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '2', '2017-08-06 21:10:50', '1');
INSERT INTO `tbl_print_info` VALUES ('FFE8AF4268984277A0CCB9B42E5D2879', '1BF9700AC16146EB9F29C235674DD84F', '5A6B21AF30C5466F857535C197D2AB12', '3', '2017-08-27 21:04:40', '1');

-- ----------------------------
-- Table structure for `tb_patient_info`
-- ----------------------------
DROP TABLE IF EXISTS `tb_patient_info`;
CREATE TABLE `tb_patient_info` (
  `uuid` varchar(36) NOT NULL,
  `ID` varchar(20) DEFAULT NULL,
  `cardid` varchar(20) NOT NULL COMMENT '身份证号,加密存储',
  `name` varchar(20) NOT NULL,
  `sex` tinyint(1) unsigned zerofill DEFAULT NULL COMMENT '性别 0女 1男',
  `birth` date DEFAULT NULL,
  `phone` varchar(16) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL,
  `lastchecktime` datetime DEFAULT NULL COMMENT '最后一次检查时间',
  `bs` varchar(128) DEFAULT NULL COMMENT '病史',
  `meno` varchar(64) DEFAULT NULL,
  PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tb_patient_info
-- ----------------------------
INSERT INTO `tb_patient_info` VALUES ('298E7F50C57340E9825F2CD0B60FDC63', '1', '201112198703238895', '杨过韩', '1', null, '13799005588', '2017-07-15 22:42:50', '2017-07-15 22:42:50', '我爱北京天安门嗯嗯', '测试');
INSERT INTO `tb_patient_info` VALUES ('79EAE05B6C644B3FADFAC7FDA337AD0F', '4', '107822197603192561', '田晓雯', '0', null, '13500981352', '2017-08-06 07:16:41', '2017-08-06 07:16:41', '无病也是病', '测试1');

-- ----------------------------
-- Table structure for `tb_report_info`
-- ----------------------------
DROP TABLE IF EXISTS `tb_report_info`;
CREATE TABLE `tb_report_info` (
  `uuid` varchar(64) NOT NULL DEFAULT '',
  `name` varchar(32) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `patient_uuid` varchar(64) DEFAULT NULL COMMENT '病人uuid',
  `ks` varchar(16) DEFAULT NULL COMMENT '科室',
  `ch` varchar(3) DEFAULT NULL COMMENT '床号',
  `nlljcjg` varchar(8) DEFAULT NULL COMMENT '尿流量检查结果-尿流率',
  `pnl` varchar(8) DEFAULT NULL COMMENT '排尿量',
  `pgrlylcd` varchar(8) DEFAULT NULL COMMENT '充盈期膀胱容量-压力测定结果',
  `pgrl_cg` varchar(8) DEFAULT NULL COMMENT '膀胱容量-初感',
  `pgrl_zc` varchar(8) DEFAULT NULL COMMENT '膀胱容量-正常',
  `pgrl_zd` varchar(8) DEFAULT NULL COMMENT '膀胱容量-最大',
  `pgsyx` varchar(8) DEFAULT NULL COMMENT '膀胱顺应性',
  `pgwdx` varchar(8) DEFAULT NULL COMMENT '膀胱稳定性',
  `tsjc` varchar(32) DEFAULT NULL COMMENT '特殊检查',
  `vlpp` varchar(8) DEFAULT NULL,
  `dlpp` varchar(8) DEFAULT NULL,
  `clpp` varchar(8) DEFAULT NULL,
  `pgaqrl` varchar(8) DEFAULT NULL,
  `otherInfo` varchar(256) DEFAULT NULL,
  `testresult` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tb_report_info
-- ----------------------------
INSERT INTO `tb_report_info` VALUES ('5A6B21AF30C5466F857535C197D2AB12', '', '2017-08-27 21:02:25', '298E7F50C57340E9825F2CD0B60FDC63', '', '', '1', '2', '', '3', '4', '5', '高顺应性', '逼尿肌活动过度', '', '6', '7', '8', '9', '11114444rfff', '22222eeee');

-- ----------------------------
-- Table structure for `tb_user`
-- ----------------------------
DROP TABLE IF EXISTS `tb_user`;
CREATE TABLE `tb_user` (
  `user_id` varchar(64) NOT NULL COMMENT 'uuid',
  `user_name` varchar(20) DEFAULT NULL COMMENT '用户名',
  `user_loginName` varchar(16) DEFAULT NULL,
  `user_passwd` varchar(128) DEFAULT NULL COMMENT '用户口令',
  `user_status` smallint(2) DEFAULT NULL COMMENT '状态: 1正常，2冻结',
  `user_class` smallint(2) DEFAULT NULL COMMENT '级别：1系统管理员，2普通用户',
  `user_phone` varchar(16) DEFAULT NULL,
  `user_createtime` datetime DEFAULT NULL COMMENT '创建时间',
  `user_lastlogintime` datetime DEFAULT NULL COMMENT '最后登录时间',
  `user_meno` varchar(128) DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tb_user
-- ----------------------------
INSERT INTO `tb_user` VALUES ('1BF9700AC16146EB9F29C235674DD84F', '管理员', 'admin', 'MDUtMzYtMUMtMDEtNzEtNkItQTgtOTMtMkQtRDktRjQtRjYtMjQtN0YtMDEtOTA=', '1', '1', '13500998769', '2017-08-27 12:30:52', '2017-08-27 21:16:43', '');
