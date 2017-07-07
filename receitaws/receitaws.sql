/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50621
Source Host           : localhost:3306
Source Database       : receitaws

Target Server Type    : MYSQL
Target Server Version : 50621
File Encoding         : 65001

Date: 2017-07-07 14:30:57
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `cliente`
-- ----------------------------
DROP TABLE IF EXISTS `cliente`;
CREATE TABLE `cliente` (
  `id` bigint(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(255) DEFAULT NULL,
  `cpf` varchar(255) DEFAULT NULL,
  `dt_nascimento` datetime DEFAULT NULL,
  `num_cartao` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `indexCpf` (`cpf`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of cliente
-- ----------------------------
INSERT INTO `cliente` VALUES ('1', 'Vagton Alves Ferreira', '23324343', '2017-07-14 00:48:00', '223323232');
INSERT INTO `cliente` VALUES ('2', 'Ana Carla Silva', '23423423', '2017-07-18 00:48:05', '544545454');

-- ----------------------------
-- Table structure for `estabelecimento`
-- ----------------------------
DROP TABLE IF EXISTS `estabelecimento`;
CREATE TABLE `estabelecimento` (
  `id` bigint(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(255) DEFAULT NULL,
  `cnpj` varchar(100) DEFAULT NULL,
  `natureza_juridica` varchar(255) DEFAULT NULL,
  `situacao` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `indexCnpj` (`cnpj`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of estabelecimento
-- ----------------------------
INSERT INTO `estabelecimento` VALUES ('1', 'Casa Bahia', '3423424234', 'Loja', 'Ativo');
INSERT INTO `estabelecimento` VALUES ('4', 'Telemar Norte Leste Sa', '3245234234', 'Ponto de Venda', 'Ativo');

-- ----------------------------
-- Table structure for `pagamento`
-- ----------------------------
DROP TABLE IF EXISTS `pagamento`;
CREATE TABLE `pagamento` (
  `id` bigint(11) NOT NULL AUTO_INCREMENT,
  `valor` float DEFAULT NULL,
  `dt_pagamento` datetime DEFAULT NULL,
  `id_cliente` bigint(11) DEFAULT NULL,
  `id_estabelecimento` bigint(11) DEFAULT NULL,
  `cancelado` int(4) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `indexPagamento` (`id_cliente`,`id_estabelecimento`,`valor`,`dt_pagamento`) USING BTREE,
  KEY `fk_cliente` (`id_cliente`),
  KEY `fk_estabelecimento` (`id_estabelecimento`),
  CONSTRAINT `pagamento_ibfk_1` FOREIGN KEY (`id_cliente`) REFERENCES `cliente` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pagamento_ibfk_2` FOREIGN KEY (`id_estabelecimento`) REFERENCES `estabelecimento` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of pagamento
-- ----------------------------
INSERT INTO `pagamento` VALUES ('1', '100', '2017-07-18 00:49:38', '1', '1', '0');
INSERT INTO `pagamento` VALUES ('2', '23.5', '2017-07-07 02:04:00', '2', '1', '0');
