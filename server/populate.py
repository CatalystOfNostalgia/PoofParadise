import models
import random

name_pool = ['Ted', 'Margaret', 'Eric', 'Anthony', 'Jeremy', 'Anjana', \
             'Robert', 'Alex', 'Austin', 'Brittany', 'William', 'Bryn', 'Zach',\
             'Alberto', 'Ian', 'Dan', 'Madison', 'Dylan', 'Lisa']

used_positions = [[False for x in range(25)] for x in range(25)]

def free_spot(x, y):
    if not used_positions[x][y] and \
       not used_positions[x - 1][y] \
       and not used_positions[x][y - 1] \
       and not used_positions[x - 1][y - 1]:

        return True
    else:
        return False

def sample_insert_group():
    for name in name_pool:
        if not exists(name):
            sample_insert(name)

def sample_select_group():
    for name in name_pool:
        if exists(name):
            sample_select_email(name)

def sample_insert_building_group():
    for name in name_pool:
        if (exists(name)):
            for i in range(5):
                sample_insert_building(name)

def sample_select_buildings_group():
    for name in name_pool:
        if (exists(name)):
            sample_select_buildings(name)

def sample_insert_building(name):
    user_id = models.session.query(models.User).filter(models.User.name==name).one().user_id
    lv = random.randrange(10)
    building_info_id = random.randrange(8)
    building_info_id = building_info_id + 1

    position_x = random.randrange(1, 24, 1)
    position_y = random.randrange(1, 24, 1)
    while (not free_spot(position_x, position_y)):
        print("x: " + str(position_x) + "y: " + str(position_y))
        position_x = random.randrange(1, 24, 1)
        position_y = random.randrange(1, 24, 1)

    used_positions[position_x][position_y] = True
    used_positions[position_x - 1][position_y] = True
    used_positions[position_x][position_y - 1] = True
    used_positions[position_x - 1][position_y - 1] = True

    build1 = models.user_building.UserResourceBuilding( \
        user_id = user_id, \
        building_info_id = building_info_id, \
        position_x = position_x, \
        position_y = position_y
        )

    models.session.add(build1)
    models.session.commit()
    print (name + " added a building")

def sample_insert(name):
    user = models.user.User(name=name, \
                            email=name+'@gravehub.com', \
                            username = name+'1', \
                            password = 'password')

    models.session.add(user)
    models.session.commit()
    print (name + " is added")

def sample_select_email(name):
    selected  = models.session.query(models.User).filter(models.User.name==name).one()
    print (name + "'s email is: " + selected.email)

def sample_select_buildings(name):
    user_id = models.session.query(models.User).filter(models.User.name==name).one().user_id
    selected = models.session.query(models.UserResourceBuilding).filter(models.UserDecorativeBuilding.user_id==user_id).all()
    for building in selected:
        print (name + ' has a building with info_id ' + \
               str(building.building_info_id) + \
               ' and with level: ' + \
               str(building.level))
    
def sample_remove(name):
    selected = models.session.query(models.User).filter(models.User.name==name).one()
    models.session.delete(selected)
    models.session.commit()
    print (name + " is deleted")

def exists(name):
    if models.session.query(models.User).filter(models.User.name == name).count() == 0:
        return False
    else:
        return True

sample_insert_group()
sample_select_group()
sample_insert_building_group()
sample_select_buildings_group()
