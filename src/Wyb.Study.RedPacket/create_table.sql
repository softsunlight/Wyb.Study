DROP DATABASE IF EXISTS `test001`;
CREATE DATABASE `test001`;

DROP TABLE IF EXISTS `red_packet`;
CREATE TABLE `red_packet` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `amount` decimal(18,2) DEFAULT 0.00 COMMENT '红包总金额',
  `number` int DEFAULT 0 COMMENT '红包个数',
  `remain_amount` decimal(18,2) DEFAULT 0.00 COMMENT '剩余金额',
  `remain_number` int DEFAULT 0 COMMENT '剩余红包个数',
  `create_time` datetime DEFAULT now() COMMENT '创建时间',
  `modify_time` datetime DEFAULT now() COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci ROW_FORMAT=DYNAMIC;

DROP TABLE IF EXISTS `red_wars_log`;
CREATE TABLE `red_wars_log` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `red_packet_id` bigint(20) COMMENT 'red_packet_id',
  `amount` decimal(18,2) DEFAULT 0.00 COMMENT '红包总金额',
  `user_id` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '用户id',
  `create_time` datetime DEFAULT now() COMMENT '创建时间',
  `modify_time` datetime DEFAULT now() COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci ROW_FORMAT=DYNAMIC;