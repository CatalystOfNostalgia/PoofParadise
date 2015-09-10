CREATE SCHEMA IF NOT EXISTS gravehub
USE gravehub;

CREATE TABLE Users (userID int, 
					name varchar(20),
					email varchar(20),
                    username varchar(20),
                    lastLogin date,
					password varchar(20),
                    level int, 
                    PRIMARY KEY (userID));
                    
CREATE TABLE UserBuilding  (userID int,
							buildingType ENUM  ('cave', 'windmill', 
                                                'campfire', 'pond', 'townhall'),
							level int,
							points int,
                            resourceProductionRate int,
                            resourceType ENUM ('fire', 'earth', 'air', 'water'),
                            capacity int);

CREATE TABLE IF NOT EXISTS `BuildingInfo` (
  `idBuildingInfo` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NULL,
  `structureType` INT NOT NULL,
  `buildTime` DATE NULL,
  `resourceCostFire` INT NULL,
  `resourceCostWater` INT NULL,
  `resourceCostEarth` INT NULL,
  `resourceCostAir` INT NULL,
  `resourceGatherRate` INT NULL,
  PRIMARY KEY (`idBuildingInfo`));
  
select * from Users;
select * from UserBuilding;