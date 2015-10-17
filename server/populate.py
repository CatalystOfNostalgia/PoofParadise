import models
import random

name_pool = ['Ted', 'Margaret', 'Eric', 'Anthony', 'Jeremy', 'Anjana', \
             'Robert', 'Alex', 'Austin', 'Brittany', 'William', 'Bryn', 'Zach',\
             'Alberto', 'Ian', 'Dan', 'Madison', 'Dylan', 'Lisa']

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

    build1 = models.user_building.UserResourceBuilding( \
        user_id = user_id, \
        building_info_id = building_info_id, \
        position_x = random.randrange(25), \
        position_y = random.randrange(25)
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
