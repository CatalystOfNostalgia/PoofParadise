from sqlalchemy import *

db = create_engine('mysql+mysqldb://root@gravehub')

db.echo = False

import User
