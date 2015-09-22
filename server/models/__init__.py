from sqlalchemy import *
from sqlalchemy.orm import sessionmaker

db = create_engine('mysql+mysqldb://root:@localhost:8000/gravehub')
db.echo = False

Session = sessionmaker(bind=db)
session = Session()

# Import all our models here. Will make doing table operations much easier. Please import in alphabetical order.
from user import *
#from user_building import *

