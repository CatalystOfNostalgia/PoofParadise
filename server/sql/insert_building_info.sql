-- inserts the static info for buildings into the database
-- resource_type key:
-- 0 : fire
-- 1 : water
-- 2 : air
-- 3 : earth

USE gravehub;

INSERT INTO residence_upgrade(
    level, resource_cost_fire, resource_cost_water, resource_cost_earth, 
    resource_cost_air, poof_cap, experience_gain 
) VALUES (
    1, 20, 20, 20, 20, 10, 0
);

INSERT INTO residence_upgrade(
    level, resource_cost_fire, resource_cost_water, resource_cost_earth, 
    resource_cost_air, poof_cap, experience_gain 
) VALUES (
    2, 30, 30, 30, 30, 20, 0
);

-- level 1 fire tree
INSERT INTO resource_building(
    level, next_building_id, name, size, production_rate, production_type, 
    resource_cost_fire, resource_cost_water, resource_cost_air, 
    resource_cost_earth, experience_gain
) VALUES (
    1, 2, 'fire_tree', 2, 5, 0, 50, 0, 0, 0, 10
);

-- level 2 fire tree
INSERT INTO resource_building(
    level, next_building_id, name, size, production_rate, production_type, 
    resource_cost_fire, resource_cost_water, resource_cost_air, 
    resource_cost_earth, experience_gain
) VALUES (
    2, null, 'fire_tree', 2, 10, 0, 50, 0, 0, 0, 20
);

-- level 1 pond
INSERT INTO resource_building(
    level, next_building_id, name, size, production_rate, production_type, 
    resource_cost_fire, resource_cost_water, resource_cost_air, 
    resource_cost_earth, experience_gain
) VALUES (
    1, 4, 'pond', 2, 5, 1, 0, 50, 0, 0, 10
);

-- level 2 pond
INSERT INTO resource_building(
    level, next_building_id, name, size, production_rate, production_type, 
    resource_cost_fire, resource_cost_water, resource_cost_air, 
    resource_cost_earth, experience_gain
) VALUES (
    2, null, 'pond', 2, 10, 1, 0, 50, 0, 0, 20
);

-- level 1 windmill
INSERT INTO resource_building(
    level, next_building_id, name, size, production_rate, production_type, 
    resource_cost_fire, resource_cost_water, resource_cost_air, 
    resource_cost_earth, experience_gain
) VALUES (
    1, 6, 'windmill', 2, 5, 2, 0, 0, 50, 0, 10
);

-- level 2 windmill
INSERT INTO resource_building(
    level, next_building_id, name, size, production_rate, production_type, 
    resource_cost_fire, resource_cost_water, resource_cost_air, 
    resource_cost_earth, experience_gain
) VALUES (
    2, null, 'windmill', 2, 10, 2, 0, 0, 50, 0, 20
);

-- level 1 cave
INSERT INTO resource_building(
    level, next_building_id, name, size, production_rate, production_type, 
    resource_cost_fire, resource_cost_water, resource_cost_air, 
    resource_cost_earth, experience_gain
) VALUES (
    1, 8, 'cave', 2, 5, 3, 0, 0, 0, 50, 10
);

-- level 2 cave
INSERT INTO resource_building(
    level, next_building_id, name, size, production_rate, production_type, 
    resource_cost_fire, resource_cost_water, resource_cost_air, 
    resource_cost_earth, experience_gain
) VALUES (
    2, null, 'cave', 2, 10, 1, 0, 0, 0, 50, 20
);
