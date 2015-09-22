import models

def createAccount( name, email, username ):
	new_user = models.user.User(name=name, email=email, username=username)
	models.session.add(new_user)
	models.session.commit()

def logIn( username ):
	user = models.session.query(models.User).filter(models.User.username==username).first()
	return user.user_id

