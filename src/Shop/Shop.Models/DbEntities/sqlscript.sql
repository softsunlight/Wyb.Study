CREATE TABLE `shop` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '店铺名称',
  `logo` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci COMMENT '店铺LOGO',
  `desc` varchar(1024) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '店铺描述',
  `create_time` datetime DEFAULT now() COMMENT '创建时间',
  `update_time` datetime DEFAULT now() COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci ROW_FORMAT=DYNAMIC;