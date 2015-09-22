import models

def createAccount( name, email, username ):
	new_user = models.user.User(name=name, email=email, username=username)
	models.session.add(new_user)
	models.session.commit()

def testCreate( name ):
	print("selecting with name " + name)
	user = models.session.query(models.User).filter(models.User.name == "Anthony").first()
	print("The user was inputted with name " + user.name)

createAccount("Anthony", "abc@abc", "coolcat") 
