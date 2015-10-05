from sqlalchemy import PrimaryKeyConstraint
from sqlalchemy import Column
from sqlalchemy import Integer
from sqlalchemy import String
from sqlalchemy import Date
from sqlalchemy.ext.declarative import declarative_base

Base = declarative_base()
class Friends(Base):
	__tablename__ = 'friends'
	__table_args__ = (
		PrimaryKeyConstraint('friend1_id', 'friend2_id'),
	)

	friend1_id = Column(Integer, nullable = False)
	friend2_id = Column(Integer, nullable = False)
