from models import User
from sqlalchemy import insert, select, create_engine
import models

def sample_insert():
    user = models.User(name='ted', email='asdf@1234.com', username = 'abc')
    models.session.add(user)
    models.session.commit()
    ted = User.query.filter_by(username='peter').first()


