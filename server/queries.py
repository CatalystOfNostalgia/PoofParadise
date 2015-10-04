import models

def create_account( name, email, username, password ):
	new_user = models.user.User(name = name, email = email, username = username, password = password)
	models.session.add(new_user)
	models.session.commit()

def log_in( username, password ):
	user = models.session.query(models.User).filter(models.User.username == username, models.User.password == password).first()
	return user

def get_user_resource_buildings( user_id ):
	user_buildings = models.session.query(models.UserResourceBuilding).filter(models.UserResourceBuilding.user_id == user_id).all()

	buildings = dict_buildings(user_buildings)

	return buildings
	
def get_user_decorative_buildings( user_id ):
	user_buildings = models.session.query(models.UserDecorativeBuilding).filter(models.UserDecorativeBuilding.user_id == user_id).all()

	buildings = dict_buildings(user_buildings)

	print(buildings)
	return buildings


def dict_buildings( buildings ):
	new_buildings = []
	for building in buildings:
			add_building = {}
			add_building['id'] = building.id
			add_building['building_info_id'] = building.building_info_id
			add_building['position_x'] = building.position_x
			add_building['position_y'] = building.position_y
			new_buildings.append(add_building)
	return new_buildings
		
