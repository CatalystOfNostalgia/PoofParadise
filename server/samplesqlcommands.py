import models
import random

name_pool = ['Ted', 'Margaret', 'Eric', 'Anthony', 'Jeremy', 'Anjana', 'Robert', 'Alex', 'Austin', 'Brittany', 'William', 'Bryn', 'Zach', 'Alberto', 'Ian', 'Dan', 'Madison', 'Dylan', 'Lisa']

def sample_insert_users():
    for name in name_pool:
        user = models.user.User(name=name, email=name+'@gravehub.com', username = name+'1', password = 'password')
        models.session.add(user)
        models.session.commit()
        print name + " is added"
    if not exists('ted'):
        user = models.user.User(name='ted', email='ted@case.edu', username = 'ted1')
        models.session.add(user)
        models.session.commit()
        print "ted is added"
    if not exists ('eric'):
        user = models.user.User(name='eric', email='eric@case.edu', username = 'eric1')
        models.session.add(user)
        models.session.commit()
        print  "eric is added"

def sample_insert_building(user_id):
    level = random.randrange(10)
    build1 = models.user_building.UserDecorativeBuilding(user_id=user_id, building_info_id=0, level=1)
    models.session.add(build1)
    models.session.commit()

def sample_select():
    ted = models.session.query(models.User).filter(models.User.username=='ted1').one()
    print ted.email
    eric = models.session.query(models.User).filter(models.User.username=='eric1').one()
    print eric.email
def sample_remove():
    ted = models.session.query(models.User).filter(models.User.name=='ted').one()
    models.session.delete(ted)
    models.session.commit()
    print "ted is deleted"
def exists(name):
    if models.session.query(models.User).filter(models.User.name == name).count() == 0:
        return False
    else:
        return True
sample_remove()
sample_insert_users()
sample_select()
