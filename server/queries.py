import models
from sqlalchemy.dialects import mysql
from sqlalchemy import func

# creates a new entry in the user table of the database
def create_account( name, email, username, password ):
    new_user = models.user.User( \
        name = name, \
        email = email, \
        username = username, \
        password = password)

    models.session.add(new_user)
    models.session.commit()


    # create an HQ for the user

# returns the user given a username and password
def log_in( username, password ):
    try:
        user = models.session.query(models.User).filter( \
            models.User.username == username, \
            models.User.password == password \
            ).one()

        return user

    except:
        return None


# updates a user info given a user dictionary
def save_user_info( user ):

    updated_user = find_user_from_id(user['user_id'])

    updated_user.name = user['name']
    updated_user.level = user['level']
    updated_user.email = user['email']
    updated_user.username = user['username']
    updated_user.password = user['password']
    updated_user.experience = user['experience']
    updated_user.headquarters_level = user['hq_level']
    updated_user.hq_pos_x = user['hq_pos_x']
    updated_user.hq_pos_y = user['hq_pos_y']
    updated_user.fire = user['fire']
    updated_user.water = user['water']
    updated_user.earth = user['earth']
    updated_user.air = user['air']
    models.session.commit()

# returns the user given a username
def find_user_from_username( username ):
    user = models.session.query(models.User).filter(models.User.username == \
                                                                username).one()

    return user

# returns a user given a user id
def find_user_from_id( user_id ):
    user = models.session.query(models.User).filter(models.User.user_id == \
                                                                user_id).one()

    return user

# returns all the friends of a user given that user's id
def get_friends( user_id ):
    
    friendships = models.session.query(models.Friends).filter( \
                  models.Friends.friend1_id == user_id)
    other_friendships = models.session.query(models.Friends).filter( \
                        models.Friends.friend2_id == user_id)

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

    return user_friends

# adds a friend connection between users
def add_friend( user_id, friend_id ):

    friends = models.friends.Friends( \
        friend1_id = user_id, \
        friend2_id = friend_id )

    models.session.add(friends)
    models.session.commit()

# gets the resource buildings of a user
def get_user_resource_buildings( user_id ):

    user_buildings = models.session.query(models.UserResourceBuilding).filter( \
                     models.UserResourceBuilding.user_id == user_id).all()

    buildings = dict_buildings(user_buildings)

    # add on the building info data
    if buildings:
            for building in buildings:
                    building_info = get_resource_building_info( \
                                        building['building_info_id'])
                    building.update(building_info)

    return buildings
    
# gets the decorative buildings of a user
def get_user_decorative_buildings( user_id ):
    user_buildings = models.session.query(models.UserDecorativeBuilding).filter( \
                     models.UserDecorativeBuilding.user_id == user_id).all()

    buildings = dict_buildings(user_buildings)
    
    # add on the building info data 
    if buildings:
            for building in buildings:
                    building_info = get_decorative_building_info( \
                                        building['building_info_id'])
                    building.update(building_info)

    return buildings

# get the user's residence buildings 
def get_user_residence_buildings(user_id):
    user_buildings = models.session.query(models.UserResidenceBuilding).filter(
        models.UserResidenceBuilding.user_id == user_id
    ).all()

    buildings = dict_buildings(user_buildings)

    if buildings:
        for building in buildings:
            building_info = get_residence_building_info(building['building_info_id'])
            building.update(building_info)


    return buildings

# get the building info of a residence_building
def get_residence_building_info(building_info_id):
    building_info = models.session.query(models.ResidenceUpdate).filter(
        models.ResidenceUpgrade.level == building_info_id    
    ).one()
    
    building = {
        'level': building_info.level,
        'poof_cap': building_info.poof_cap
    }

    return building

# gets the building info of a resource building
def get_resource_building_info( building_info_id ):

    building_info = models.session.query(models.ResourceBuildingInfo).filter( \
                    models.ResourceBuildingInfo.building_info_id == \
                                                building_info_id).one()

    building = {}
    building['size'] = building_info.size
    building['name'] = building_info.name
    building['level'] = building_info.level
    building['production_type'] = building_info.production_type
    building['production_rate'] = building_info.production_rate
    
    return building

# gets the building info of a decorative building
def get_decorative_building_info( building_info_id ):

    building_info = models.session.query(models.DecorativeBuildingInfo).filter( \
                    models.DecorativeBuildingInfo.building_info_id == \
                                                  building_info_id).one()

    building = {}
    building['size'] = building_info.size
    building['name'] = building_info.name
    building['poofs_generated'] = building_info.poofs_generated

    return building

# saves the users buildings returns the building if a building cannot be found
def save_building_info ( resource_buildings, decorative_buildings, user_id ):
    
    ids = {}
    ids['resource_buildings'] = []
    ids['decorative_buildings'] = []

    # resource buildings
    for building in resource_buildings:
        if building['new'] == 'True':
            ids['resource_buildings'] = create_resource_building(building, user_id, ids['resource_buildings'])
            success = True
        else:
            success = update_resource_building(building, user_id)
        if not success:
            return None

    # decorative buildings  
    for building in decorative_buildings:
        if building['new'] == 'True':
            ids['decorative_buildings'] = create_decorative_building(building, user_id, ids['decorative_buildings'])
            success = True
        else:
            success = update_decorative_building(building)
        if not success:
            return None

    return ids;

# updates an existing building in the database if the building cannot be found
# returns False
def update_resource_building ( building, user_id ):

    try:

        building_id = building['id']

        updated_building = models.session.query(models.UserResourceBuilding).filter( \
                           models.UserResourceBuilding.id == building_id).one()
        updated_building.level = building['level']
        updated_building.x_coordinate = building['position_x']
        updated_building.y_coordinate = building['position_y']

        models.session.commit()
        return True

    except:
        print 'could not find resource building'
        rollback()
        return False

# updates an existing building in the database if the building cannot be found
# returns False
def update_decorative_building ( building ):

    try:

        building_id = building['id']
        print 'id: ' + building_id

        updated_building = models.session.query(models.UserDecorativeBuilding).filter( \
                           models.UserDecorativeBuilding.id == building_id).one()

        updated_building.level = building['level']
        updated_building.x_coordinate = building['position_x']
        updated_building.y_coordinate = building['position_y']

        models.session.commit()
        return True

    except:
        print 'could not find decorative building'
        rollback()
        return False

# creates a decorative building in the database
def create_decorative_building ( building, user_id, ids ):

    new_building = models.UserDecorativeBuilding( \
        user_id = user_id, \
        building_info_id = building['building_info_id'], \
        level = building['level'], \
        position_x = building['position_x'], \
        position_y = building['position_y'])

    models.session.add(new_building)
    models.session.commit() 

    print 'added decorative building'

    new_id = models.session.query(func.max(models.UserDecorativeBuilding.id)).one()[0]

    ids.append(new_id)
    return ids

# creates a resource building in the database
def create_resource_building ( building, user_id, ids ):

    new_building = models.UserResourceBuilding( \
        user_id = user_id, \
        building_info_id = building['building_info_id'], \
        level = building['level'], \
        position_x = building['position_x'], \
        position_y = building['position_y'])

    models.session.add(new_building)
    models.session.commit() 

    new_id = models.session.query(func.max(models.UserResourceBuilding.id)).one()[0]

    ids.append(new_id - 1)
    return ids

# turns a building into a dictionary
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

# turns a friendship into a dictionary
def dict_friends( friendships, userdd ):
    new_friendships = []
    for friendship in friendships:
        add_friends = {}
        add_friends['friend1_id'] = friend1_id

# rollsback the sqlalchemy session. Use if there is an exception
def rollback():
    models.session.rollback()
    
