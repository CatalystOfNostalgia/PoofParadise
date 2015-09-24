import models

def createAccount( name, email, username, password ):
	new_user = models.user.User(name=name, email=email, username=username, password=password)
	models.session.add(new_user)
	models.session.commit()

def logIn( username, password ):
	user = models.session.query(models.User).filter(models.User.username==username, models.User.password==password).first()
	if user is None:
		return -1
	else:
		return user.user_id


