CREATE SCHEMA IF NOT EXISTS gravehub
USE gravehub;

CREATE TABLE user(
    user_id long,
	name varchar(100),
	email varchar(100),
    username varchar(100),
    last_login date,
	password varchar(100),
    level long,
    PRIMARY KEY (userID)
);

CREATE TABLE user_building(
    user_id long,
    building_type ENUM  ('cave', 'windmill', 'campfire', 'pond', 'townhall'),
	level long,
	points long,
    resource_production_rate long,
    resource_type ENUM ('fire', 'earth', 'air', 'water'),
    capacity long
);

CREATE TABLE `building_info`(
    `id_building_info` long NOT NULL AUTO_INCREMENT,
    `name` VARCHAR(100) NULL,
    `structure_type` INT NOT NULL,
    `resource_cost_fire` long NULL,
    `resource_cost_water` long NULL,
    `resource_cost_earth` long NULL,
    `resource_cost_air` long NULL,
    `resource_gather_rate` long  NULL,
    PRIMARY KEY (`id_building_info`)
);
