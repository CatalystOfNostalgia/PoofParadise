import models

def create_account( name, email, username, password ):
	new_user = models.user.User( \
		name = name, \
		email = email, \
		username = username, \
		password = password)

	models.session.add(new_user)
	models.session.commit()

def log_in( username, password ):
	user = models.session.query(models.User).filter( \
													models.User.username == username, \
													models.User.password == password \
												   ).one()
	return user

def save_user_info( user ):

	updated_user = find_user_from_id(user['user_id'])

	updated_user.name = user['name']
	updated_user.level = user['level']
	updated_user.email = user['email']
	updated_user.username = user['username']
	updated_user.password = user['password']
	updated_user.experience = user['experience']
	updated_user.headquarters_level = user['hq_level']

	models.session.commit()

def find_user_from_username( username ):
	user = models.session.query(models.User).filter(models.User.username == username).one()

	return user

def find_user_from_id( user_id ):
	user = models.session.query(models.User).filter(models.User.user_id == user_id).one()

	return user

def get_friends( user_id ):
	
	friendships = models.session.query(models.Friends).filter(models.Friends.friend1_id == user_id)
	other_friendships = models.session.query(models.Friends).filter(models.Friends.friend2_id == user_id)

	user_friends = []
	for friendship in friendships:
		new_friend = {}
		friend_id = friendship.friend2_id
		friend_username = find_user_from_id(friend_id).username
		new_friend[str(friend_username)] = friend_id
		user_friends.append(new_friend)

	for friendship in other_friendships:
		new_friends = {}
		friend_id = friendship.friend1_id
		friend_username = find_user_from_id(friend_id).username
		new_friends[friend_username] = friend_id
		user_friends.append(new_friend)

	print(user_friends)

	return user_friends

def add_friend( user_id, friend_id ):

	friends = models.friends.Friends( \
		friend1_id = user_id, \
		friend2_id = friend_id )

	models.session.add(friends)
	models.session.commit()

def get_user_resource_buildings( user_id ):
	user_buildings = models.session.query(models.UserResourceBuilding).filter(models.UserResourceBuilding.user_id == user_id).all()

	buildings = dict_buildings(user_buildings)

	if buildings:
			for building in buildings:
					building_info = get_resource_building_info(building['building_info_id'])
					building.update(building_info)

	return buildings
	

def get_user_decorative_buildings( user_id ):
	user_buildings = models.session.query(models.UserDecorativeBuilding).filter(models.UserDecorativeBuilding.user_id == user_id).all()

	buildings = dict_buildings(user_buildings)
	
	if buildings:
			for building in buildings:
					building_info = get_decorative_building_info(building['building_info_id'])
					building.update(building_info)

	return buildings

def get_resource_building_info( building_info_id ):

	building_info = models.session.query(models.ResourceBuildingInfo).filter(models.ResourceBuildingInfo.building_info_id == building_info_id).one()

	building = {}
	building['size'] = building_info.size
	building['name'] = building_info.name
	building['level'] = building_info.level
	building['production_type'] = building_info.production_type
	building['production_rate'] = building_info.production_rate
	
	return building

def get_decorative_building_info( building_info_id ):

	building_info = models.session.query(models.DecorativeBuildingInfo).filter(models.DecorativeBuildingInfo.building_info_id == building_info_id).one()

	building = {}
	building['size'] = building_info.size
	building['name'] = building_info.name
	building['poofs_generated'] = building_info.poofs_generated

	return building


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

def dict_friends( friendships, userdd ):
	new_friendships = []
	for friendship in friendships:
		add_friends = {}
		add_friends['friend1_id'] = friend1_id

def rollback():
	models.session.rollback()
	
