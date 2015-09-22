import models

def sample_insert():
    user = models.user.User(name='ted', email='asdf@1234.com', username = 'abc')
    models.session.add(user)
    models.session.commit()

def sample_select():
    ted = models.session.query(models.User).filter(models.User.username=='abc').first()
    print ted.email

sample_insert()
sample_select()
