-- Create statements for the database tables

DROP DATABASE IF EXISTS gravehub;
CREATE SCHEMA IF NOT EXISTS gravehub;
USE gravehub;

CREATE TABLE user(
    user_id int NOT NULL AUTO_INCREMENT,
    name varchar(100) NOT NULL,
    email varchar(100) NOT NULL,
    username varchar(100) NOT NULL UNIQUE,
    password varchar(100),
    fire int DEFAULT 0,
    air int DEFAULT 0,
    water int DEFAULT 0,
    earth int DEFAULT 0,
    max_fire int DEFAULT 10,
    max_air int DEFAULT 10,
    max_water int DEFAULT 10,
    max_earth int DEFAULT 10,
    fire_ele int DEFAULT 1,
    air_ele int DEFAULT 1,
    water_ele int DEFAULT 1,
    earth_ele int DEFAULT 1,
    poof_count int DEFAULT 5,
    experience int NOT NULL,
    headquarters_level int not NULL,
    hq_pos_x int not NULL,
    hq_pos_y int not NULL,
    level int,
    PRIMARY KEY (user_id)
);

CREATE TABLE headquarters_upgrade(
    level int,
    resource_cost_fire int NULL,
    resource_cost_water int NULL,
    resource_cost_earth int NULL,
    resource_cost_air int NULL,
    poof_cap int NOT NULL,
    experience_gain int NULL,
    PRIMARY KEY (level)
);

CREATE TABLE residence_upgrade(
    level int,
    name varchar(100) NOT NULL,
    resource_cost_fire int NULL,
    resource_cost_water int NULL,
    resource_cost_earth int NULL,
    resource_cost_air int NULL,
    poof_cap int NOT NULL,
    experience_gain int NULL,
    PRIMARY KEY (level)
);

CREATE TABLE user_residence_building( 
    id int NOT NULL AUTO_INCREMENT,
    user_id int NOT NULL,
    building_info_id int NOT NULL,
    level int NOT NULL,
    position_x int NOT NULL,
    position_y int NOT NULL,
    PRIMARY KEY (id)
);


CREATE TABLE user_decorative_building(
    id int NOT NULL AUTO_INCREMENT,
    user_id int NOT NULL,
    building_info_id int NOT NULL,
    level int NOT NULL,
    position_x int NOT NULL,
    position_y int NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE decorative_building_info(
    building_info_id int NOT NULL AUTO_INCREMENT,
	level int NOT NULL,
    name VARCHAR(100) NULL,
    size int NULL,
    next_building_id int NULL,
    resource_cost_fire int NULL,
    resource_cost_water int NULL,
    resource_cost_earth int NULL,
    resource_cost_air int NULL,
    poofs_generated int NULL,
    experience_gain int NULL,
    PRIMARY KEY (building_info_id)
);

CREATE TABLE user_resource_building(
    id int NOT NULL AUTO_INCREMENT,
    level int NOT NULL,
    user_id int NOT NULL,
    building_info_id int NOT NULL,
    position_x int NOT NULL,
    position_y int NOT NULL,
    PRIMARY KEY(id)
);

CREATE TABLE resource_building(
    building_info_id int NOT NULL AUTO_INCREMENT,
    level int NOT NULL,
    next_building_id int NULL,
    name VARCHAR(100) NULL,
    size int NULL,
    production_rate int NOT NULL,
    production_type varchar(100) NOT NULL,
    resource_cost_fire int NULL,
    resource_cost_water int NULL,
    resource_cost_air int NULL,
    resource_cost_earth int NULL,
    experience_gain int NULL,
    PRIMARY KEY (building_info_id)
);

CREATE TABLE friends(
    friend1_id int,
    friend2_id int,
    PRIMARY KEY (friend1_id, friend2_id),
    FOREIGN KEY (friend1_id) REFERENCES user(user_id),
    FOREIGN kEY (friend2_id) REFERENCES user(user_id)
);
