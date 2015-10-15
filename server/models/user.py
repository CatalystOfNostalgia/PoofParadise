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
    headquarters_level = Column(Integer, default = 1)
    experience = Column(Integer, default = 0)
    fire = Column(Integer, default = 0)
    air = Column(Integer, default = 0)
    water = Column(Integer, default = 0)
    earth = Column(Integer, default = 0)
    maxFire = Column(Integer, default = 10) 
    maxAir = Column(Integer, default = 10) 
    maxWater = Column(Integer, default = 10) 
    maxEarth = Column(Integer, default = 10) 
    fireEle = Column(Integer, default = 1)
    airEle = Column(Integer, default = 1)
    waterEle = Column(Integer, default = 1)
    earthEle = Column(Integer, default = 1)
