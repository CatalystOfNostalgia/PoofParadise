from sqlalchemy import Column
from sqlalchemy import Integer
from sqlalchemy import String
from sqlalchemy import Date
from sqlalchemy.ext.declarative import declarative_base

Base = declarative_base()
class User(Base):
    __tablename__ = 'user'

    name = Column(String(100), nullable=False)
    email = Column(String(100), nullable=False)
    user_id = Column(Integer, primary_key=True, autoincrement=True)
    username = Column(String(100), nullable=False)
    password = Column(String(100), nullable=False)
    experience = Column(Integer, default = 0)
    headquarters_level = Column(Integer, default = 1)
    level = Column(Integer, default = 1)
    hq_pos_x = Column(Integer, default=7)
    hq_pos_y = Column(Integer, default=8)
    experience = Column(Integer, default = 0)
    fire = Column(Integer, default = 0)
    air = Column(Integer, default = 0)
    water = Column(Integer, default = 0)
    earth = Column(Integer, default = 0)
    max_fire = Column(Integer, default = 10) 
    max_air = Column(Integer, default = 10) 
    max_water = Column(Integer, default = 10) 
    max_earth = Column(Integer, default = 10) 
    fire_ele = Column(Integer, default = 1)
    air_ele = Column(Integer, default = 1)
    water_ele = Column(Integer, default = 1)
    earth_ele = Column(Integer, default = 1)
    poof_count = Column(Integer, default = 5)
