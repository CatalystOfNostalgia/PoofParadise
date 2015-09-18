from models import user
import models

def sample_insert():
    user = models.User(name='ted', email='asdf@1234.com', username = 'abc')
    models.session.add(user)
    models.session.commit()

def sample_select():
    ted = models.models.User.query.filter_by(username='abc').first()
    print ted.email


sample_select()
