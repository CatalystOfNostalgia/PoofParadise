import models

def create_account( name, email, username, password ):
	new_user = models.user.User(name=name, email=email, username=username, password=password)
	models.session.add(new_user)
	models.session.commit()

def log_in( username, password ):
	user = models.session.query(models.User).filter(models.User.username==username, models.User.password==password).first()
	if user is None:
		return -1
	else:
		return user.user_id

#def get_user_buildings( user_id ):
	# buildings = models.session.query(models.
