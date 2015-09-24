from sqlalchemy import Column
from sqlalchemy import Integer
from sqlalchemy import String
from sqlalchemy import Date
from sqlalchemy.ext.declarative import declarative_base

Base = declarative_base()
class UserDecorativeBuilding(Base):
    __tablename__ = 'user_decorative_building'

    id = Column(Integer, primary_key=True, autoincrement=True)
    user_id = Column(Integer, nullable=False)
    building_info_id = Column(Integer, nullable=False)
    level = Column(Integer, nullable=False, default=1)

class UserResourceBuilding(Base):
    __tablename__ = 'user_resource_building'

    id = Column(Integer, primary_key=True, autoincrement=True)
    user_id = Column(Integer, nullable=False)
    building_id = Column(Integer, nullable=False)


