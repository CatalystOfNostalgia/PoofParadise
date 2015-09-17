DROP DATABASE IF EXISTS gravehub;
CREATE SCHEMA IF NOT EXISTS gravehub;
USE gravehub;

CREATE TABLE user(
	user_id int,
	name varchar(100),
	email varchar(100),
	username varchar(100),
	last_login date,
	password varchar(100),
	level int,
	PRIMARY KEY (user_id)
);

CREATE TABLE user_building(
	user_id int,
	building_info_id int,
	building_type ENUM  ('cave', 'windmill', 'campfire', 'pond', 'townhall'),
	level int,
	points int,
	resource_production_rate int,
	resource_type ENUM ('fire', 'earth', 'air', 'water'),
	capacity int
);

CREATE TABLE `building_info`(
	`building_info_id` int NOT NULL AUTO_INCREMENT,
	`name` VARCHAR(100) NULL,
	`structure_type` INT NOT NULL,
	`resource_cost_fire` int NULL,
	`resource_cost_water` int NULL,
	`resource_cost_earth` int NULL,
	`resource_cost_air` int NULL,
	`resource_gather_rate` int  NULL,
	PRIMARY KEY (`building_info_id`)
);

CREATE TABLE friends(
	friend1_id int,
	friend2_id int,
	PRIMARY KEY (friend1_id, friend2_id),
	FOREIGN KEY (friend1_id) REFERENCES user(user_id),
	FOREIGN kEY (friend2_id) REFERENCES user(user_id)
);
