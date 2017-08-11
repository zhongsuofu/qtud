/*
Navicat MySQL Data Transfer

Source Server         : local
Source Server Version : 50520
Source Host           : localhost:3306
Source Database       : qtud

Target Server Type    : MYSQL
Target Server Version : 50520
File Encoding         : 65001

Date: 2017-08-11 17:31:17
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `tbl_curve_file_link`
-- ----------------------------
DROP TABLE IF EXISTS `tbl_curve_file_link`;
CREATE TABLE `tbl_curve_file_link` (
  `curve_uuid` varchar(64) DEFAULT NULL,
  `file_uuid` varchar(64) DEFAULT NULL
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
  `meno` varchar(128) DEFAULT NULL COMMENT '曲线说明',
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
  `checkmode` tinyint(4) DEFAULT NULL COMMENT '检查类型',
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
  `checkNum` varchar(15) DEFAULT NULL COMMENT '检查编号',
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
INSERT INTO `tbl_print_info` VALUES ('16F54A5068214705AE7C78DCAA555827', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '1', '2017-08-06 15:01:22', '1');
INSERT INTO `tbl_print_info` VALUES ('258853B098634A718618D227962346AF', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '2', '2017-08-06 21:28:45', '1');
INSERT INTO `tbl_print_info` VALUES ('2D419C2A0C5A41A0B0875EF68F5A5CC8', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '2', '2017-08-06 21:10:27', '1');
INSERT INTO `tbl_print_info` VALUES ('353639ED35A94F33A0A8421941A237E9', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '2', '2017-08-06 21:28:24', '1');
INSERT INTO `tbl_print_info` VALUES ('6D12BA15417F47EB90D5E93D901E312E', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '2', '2017-08-06 21:10:42', '1');
INSERT INTO `tbl_print_info` VALUES ('7BDF07AA6C9B400E85AE4A4F744F6790', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '2', '2017-08-06 21:10:04', '1');
INSERT INTO `tbl_print_info` VALUES ('98ADD694E3654DC897B86F66929B1C0C', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '1', '2017-08-05 21:21:26', '1');
INSERT INTO `tbl_print_info` VALUES ('B2D2A8CFA0A34F43910082434060B8DA', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '1', '2017-08-05 21:21:07', '1');
INSERT INTO `tbl_print_info` VALUES ('BA63E4A4213E4E038EB887E3FBC2A851', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '0', '2017-08-05 19:41:14', '1');
INSERT INTO `tbl_print_info` VALUES ('CFA63CC24F2346168820A683E88BA437', '1', '6BFF543CEC184543B6844AD70CD361EF', '2', '2017-08-06 21:37:10', '1');
INSERT INTO `tbl_print_info` VALUES ('F5589A8BC1B44B9E8CFBE1E1E46F4456', '1', '8D28DEBFC94B480EB79BD39DF35B1B8D', '2', '2017-08-06 21:10:50', '1');

-- ----------------------------
-- Table structure for `tb_patient_info`
-- ----------------------------
DROP TABLE IF EXISTS `tb_patient_info`;
CREATE TABLE `tb_patient_info` (
  `ID` varchar(20) DEFAULT NULL,
  `uuid` varchar(36) NOT NULL,
  `cardid` varchar(128) NOT NULL COMMENT '身份证号,加密存储',
  `name` varchar(32) NOT NULL,
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
INSERT INTO `tb_patient_info` VALUES (null, '298E7F50C57340E9825F2CD0B60FDC63', '201112198703238895', '杨过韩', '1', null, '13799005588', '2017-07-15 22:42:50', '2017-07-15 22:42:50', '我爱北京天安门嗯嗯', '测试');
INSERT INTO `tb_patient_info` VALUES (null, '648E9BD766184E4C9BFD1BD2BA0B6E08', '101112198703238235', '张三', '1', null, '13300001111', '2017-07-15 15:23:29', '2017-07-15 15:23:29', ' ', '测试22');
INSERT INTO `tb_patient_info` VALUES (null, '6F089539E2D4460EB3B609E97047EF4E', '115112198703233781', '欧阳岩轩', '1', null, '13899996664', '2017-07-15 15:34:19', '2017-07-15 15:34:19', ' ', '测试');
INSERT INTO `tb_patient_info` VALUES (null, '79EAE05B6C644B3FADFAC7FDA337AD0F', '107822197603192561', '田晓雯', '0', null, '13500981352', '2017-08-06 07:16:41', '2017-08-06 07:16:41', '无病也是病', '测试1');
INSERT INTO `tb_patient_info` VALUES (null, 'AE5D937AEF4048C282AB6F7BA511E9A5', '123456198003028876', '关犇熙', '1', null, '', '2017-08-06 11:41:28', '2017-08-06 11:41:28', '多肉', '344');
INSERT INTO `tb_patient_info` VALUES (null, 'C2BD4BA1BE4B4FE9B17D7657AD1227DC', '113112198703239023', '张唯亭', '0', null, '13588889992', '2017-07-15 15:29:48', '2017-07-15 15:29:48', ' ', '测试4177');

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
  `nlljcjg` float DEFAULT NULL COMMENT '尿流量检查结果-尿流率',
  `pnl` float DEFAULT NULL COMMENT '排尿量',
  `pgrlylcd` float DEFAULT NULL COMMENT '充盈期膀胱容量-压力测定结果',
  `pgrl_cg` float DEFAULT NULL COMMENT '膀胱容量-初感',
  `pgrl_zc` float DEFAULT NULL COMMENT '膀胱容量-正常',
  `pgrl_zd` float DEFAULT NULL COMMENT '膀胱容量-最大',
  `pgsyx` varchar(8) DEFAULT NULL COMMENT '膀胱顺应性',
  `pgwdx` varchar(8) DEFAULT NULL COMMENT '膀胱稳定性',
  `tsjc` varchar(32) DEFAULT NULL COMMENT '特殊检查',
  `vlpp` float DEFAULT NULL,
  `dlpp` float DEFAULT NULL,
  `clpp` float DEFAULT NULL,
  `pgaqrl` float DEFAULT NULL,
  `otherInfo` varchar(256) DEFAULT NULL,
  `testresult` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tb_report_info
-- ----------------------------
INSERT INTO `tb_report_info` VALUES ('028A1261FD8C4B80B8FE0236F26D9BFE', '110501198110238802', '2017-08-05 18:33:38', '298E7F50C57340E9825F2CD0B60FDC63', '', '', '0', '0', '0', '0', '0', '0', '正常', '正常', '', '0', '0', '0', '0', '', '');
INSERT INTO `tb_report_info` VALUES ('49C884F762CB49108E89A4DF25C18C73', '110501198110238802', '2017-08-05 18:12:31', '298E7F50C57340E9825F2CD0B60FDC63', '泌尿科', '21', '1', '2', '3', '4', '5', '6', '高顺应性', '逼尿肌活动过度', 'dfg', '7', '8', '9', '10', '我爱北京天安门我爱北京天安门', '我爱北京天安门`我爱北京天安门1我爱北京天安门2我爱北京天安门3我爱北京天安门4');
INSERT INTO `tb_report_info` VALUES ('6BFF543CEC184543B6844AD70CD361EF', '201112198703238895', '2017-08-06 21:35:44', '298E7F50C57340E9825F2CD0B60FDC63', '', '', '0', '0', '0', '0', '0', '0', '正常', '正常', '', '0', '0', '0', '0', '', '');
INSERT INTO `tb_report_info` VALUES ('8D28DEBFC94B480EB79BD39DF35B1B8D', '110501198110238802', '2017-08-05 17:31:25', '298E7F50C57340E9825F2CD0B60FDC63', '泌尿科', '3', '12.3', '1', '123.443', '12.1', '12.2', '12.3', '高顺应性', '逼尿肌活动过度', '22.1', '22.2', '22.3', '22.4', '22.5', '天安门上太阳升，1我爱北京天安门，天安门上太阳升。4我爱北京天安门，天安门上太阳升。6我爱北京天安门，天安门上太阳升\r\n我爱北京天安门，天安门上太阳升9', '我爱北京天安门，天安门上太阳升，欧耶。1我爱北京天安门，天安门上太阳升。2我爱北京天安门，天安门上太阳升\r\n大灰狼与小白兔。');

-- ----------------------------
-- Table structure for `tb_user`
-- ----------------------------
DROP TABLE IF EXISTS `tb_user`;
CREATE TABLE `tb_user` (
  `user_id` varchar(64) NOT NULL COMMENT 'uuid',
  `user_name` varchar(20) DEFAULT NULL COMMENT '用户名',
  `user_passwd` varchar(128) DEFAULT NULL COMMENT '用户口令',
  `user_status` smallint(2) DEFAULT NULL COMMENT '状态: 1正常，2冻结',
  `user_class` smallint(2) DEFAULT NULL COMMENT '级别：1系统管理员，2普通用户',
  `user_phone` varchar(16) DEFAULT NULL,
  `user_createtime` datetime DEFAULT NULL COMMENT '创建时间',
  `user_lastlogintime` datetime DEFAULT NULL COMMENT '最后登录时间',
  `user_meno` varchar(128) DEFAULT NULL,
  `user_loginName` varchar(16) DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tb_user
-- ----------------------------
INSERT INTO `tb_user` VALUES ('1', 'admin', '1', '1', '1', null, null, '2017-08-11 12:53:56', null, null);
