-- --------------------------------------------------------
-- 主机:                           47.99.217.95
-- 服务器版本:                        5.5.60-MariaDB - MariaDB Server
-- 服务器操作系统:                      Linux
-- HeidiSQL 版本:                  9.3.0.4984
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 导出 random_food 的数据库结构
CREATE DATABASE IF NOT EXISTS `random_food` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `random_food`;


-- 导出  表 random_food.rf_catering_shop 结构
CREATE TABLE IF NOT EXISTS `rf_catering_shop` (
  `id` bigint(20) unsigned NOT NULL COMMENT 'ID',
  `title` varchar(100) NOT NULL COMMENT '店面名称',
  `category` varchar(50) NOT NULL COMMENT '类别',
  `type` tinyint(4) NOT NULL COMMENT '类型编号',
  `address` varchar(200) NOT NULL COMMENT '详细地址',
  `adcode` int(11) NOT NULL COMMENT '地址编码',
  `province` varchar(30) NOT NULL COMMENT '省份',
  `city` varchar(30) NOT NULL COMMENT '城市',
  `district` varchar(30) NOT NULL COMMENT '区域',
  `latitude` double(16,13) NOT NULL COMMENT '纬度',
  `longitude` double(16,13) NOT NULL COMMENT '经度',
  `status` tinyint(2) NOT NULL COMMENT '0不可用1可用',
  `create_time` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='餐饮店';

-- 数据导出被取消选择。


-- 导出  表 random_food.rf_choose_customize_shop 结构
CREATE TABLE IF NOT EXISTS `rf_choose_customize_shop` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `open_id` varchar(50) NOT NULL COMMENT '微信openid',
  `shop_id` bigint(20) NOT NULL COMMENT '店铺ID',
  `shop_title` varchar(50) NOT NULL COMMENT '店铺名称',
  `category` varchar(50) NOT NULL COMMENT '类型',
  `lable` varchar(50) NOT NULL COMMENT '标签',
  `create_time` int(11) NOT NULL COMMENT '添加时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT COMMENT='随机选择的自定义店铺';

-- 数据导出被取消选择。


-- 导出  表 random_food.rf_choose_food 结构
CREATE TABLE IF NOT EXISTS `rf_choose_food` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `open_id` varchar(50) NOT NULL,
  `food_id` int(11) NOT NULL,
  `food_name` varchar(50) NOT NULL,
  `create_time` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `open_id` (`open_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 random_food.rf_choose_shop 结构
CREATE TABLE IF NOT EXISTS `rf_choose_shop` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `open_id` varchar(50) NOT NULL COMMENT '微信openid',
  `catering_shop_id` bigint(20) unsigned NOT NULL COMMENT '店铺ID',
  `catering_shop_title` varchar(50) NOT NULL COMMENT '店铺名称',
  `category` varchar(50) NOT NULL COMMENT '类型',
  `create_time` int(11) NOT NULL COMMENT '添加时间',
  PRIMARY KEY (`id`),
  KEY `open_id` (`open_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='随机选择的店铺';

-- 数据导出被取消选择。


-- 导出  表 random_food.rf_food 结构
CREATE TABLE IF NOT EXISTS `rf_food` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `food_category_id` tinyint(4) NOT NULL,
  `name` varchar(50) NOT NULL COMMENT '食物名称',
  `img_url` varchar(200) NOT NULL COMMENT '图片',
  `sort` tinyint(4) NOT NULL COMMENT '权重 越大排序越前',
  `status` tinyint(2) NOT NULL COMMENT '0不可用 1可用',
  `description` varchar(255) NOT NULL COMMENT '描述',
  `details` mediumtext NOT NULL COMMENT '详细',
  `modify_time` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `food_category_id` (`food_category_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='食物表';

-- 数据导出被取消选择。


-- 导出  表 random_food.rf_food_category 结构
CREATE TABLE IF NOT EXISTS `rf_food_category` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `p_id` int(11) NOT NULL DEFAULT '0' COMMENT '父级ID 顶级为0',
  `name` varchar(50) NOT NULL COMMENT '名称',
  `sort` tinyint(4) NOT NULL COMMENT '排序',
  `status` tinyint(2) NOT NULL COMMENT '0不可用 1可用',
  `description` varchar(50) NOT NULL COMMENT '描述',
  `modify_time` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `p_id` (`p_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='类别表';

-- 数据导出被取消选择。


-- 导出  表 random_food.rf_member 结构
CREATE TABLE IF NOT EXISTS `rf_member` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `open_id` varchar(50) NOT NULL COMMENT '微信openid',
  `nick_name` varchar(50) NOT NULL COMMENT '用户名称',
  `avatar_url` varchar(200) NOT NULL COMMENT '头像',
  `gender` tinyint(2) NOT NULL COMMENT '性别 1男 0女',
  `country` varchar(50) NOT NULL COMMENT '国家',
  `province` varchar(50) NOT NULL COMMENT '省',
  `city` varchar(50) NOT NULL COMMENT '市',
  `login_count` int(11) NOT NULL COMMENT '登录次数',
  `create_time` int(11) NOT NULL COMMENT '创建时间',
  `modify_time` int(11) NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `open_id` (`open_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='微信用户信息表';

-- 数据导出被取消选择。


-- 导出  表 random_food.rf_member_customize_shop 结构
CREATE TABLE IF NOT EXISTS `rf_member_customize_shop` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `open_id` varchar(50) NOT NULL,
  `category` varchar(50) NOT NULL COMMENT '类别',
  `lable` tinyint(4) NOT NULL COMMENT '标签 枚举 春、夏、秋、冬、住、公、家',
  `title` varchar(100) NOT NULL COMMENT '店铺名称',
  `create_time` int(11) NOT NULL,
  `modify_time` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `open_id` (`open_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户自定义店铺';

-- 数据导出被取消选择。


-- 导出  表 random_food.rf_opinion 结构
CREATE TABLE IF NOT EXISTS `rf_opinion` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `open_id` varchar(50) NOT NULL,
  `opinion` varchar(250) NOT NULL COMMENT '意见',
  `contact_info` varchar(250) NOT NULL COMMENT '联系信息',
  `create_time` int(11) NOT NULL COMMENT '添加时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='意见';

-- 数据导出被取消选择。


-- 导出  表 random_food.rf_phone_device 结构
CREATE TABLE IF NOT EXISTS `rf_phone_device` (
  `open_id` varchar(50) NOT NULL,
  `brand` varchar(50) NOT NULL COMMENT '设备品牌',
  `model` varchar(50) NOT NULL COMMENT '设备型号',
  `pixelRatio` tinyint(4) NOT NULL COMMENT '设备像素比',
  `screenWidth` tinyint(4) NOT NULL COMMENT '屏幕宽度，单位px',
  `screenHeight` tinyint(4) NOT NULL COMMENT '屏幕高度，单位px',
  `language` varchar(50) NOT NULL COMMENT '微信设置的语言',
  `system` varchar(50) NOT NULL COMMENT '操作系统及版本',
  `version` varchar(50) NOT NULL COMMENT '微信版本号',
  `platform` varchar(50) NOT NULL COMMENT '客户端平台',
  `benchmarkLevel` tinyint(4) NOT NULL COMMENT '设备性能等级（仅Android小游戏）。取值为：-2 或 0（该设备无法运行小游戏），-1（性能未知），>=1（设备性能值，该值越高，设备性能越好，目前最高不到50）',
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '修改时间，数据库自动补充',
  PRIMARY KEY (`open_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='手机设备信息';

-- 数据导出被取消选择。
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
