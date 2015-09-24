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
    level = Column(Integer, default = 1)
    headquarters_level = Column(Integer, default = 1)
    experience = Column(Integer, default = 0)

