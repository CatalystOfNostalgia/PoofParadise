import models

class UserLogin(Base):
    def get_user(self, user_id):
        # Will throw an error if there is not an existing user.
        return models.session.query(models.User).filter(models.User.user_id == user_id).one()

    def get_user_buildings(self, user_id):
        return None 
