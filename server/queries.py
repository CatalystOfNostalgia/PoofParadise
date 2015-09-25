import models

def create_account( name, email, username, password ):
	new_user = models.user.User(name = name, email = email, username = username, password = password)
	models.session.add(new_user)
	models.session.commit()

def log_in( username, password ):
	user = models.session.query(models.User).filter(models.User.username == username, models.User.password == password).first()
	return user

def get_user_resource_buildings( user_id ):
	user_buildings = models.session.query(models.user_building.UserResourceBuilding).filter(models.user.UserResourceBuilding.user_id == user_id)

	buildings = []
	for building in user_buildings:
		buildings.insert(models.session.query(models.building_info.ResourceBuilding).filter(models.building_info.ResourceBUilding.building_id == building['building_id']))
	return buildings
	
def get_user_decorative_buildings( user_id ):
	buildings = models.session.query(models.user_building.UserDecorativeBuilding).filter(models.user.UserDecorativeBuilding.user_id == user_id)
	return buildings
