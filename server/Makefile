# The build script for the server

TABLES = sql/create_tables.sql
INSERTS = sql/insert_building_info.sql
# Runs the server
gravehub:
	@echo 'Initializing server...'
	@python server.py

# Handles all server testing
.PHONY: test
test:

# Handles all server warmup
.PHONY: install
install:
	@echo 'Installing Python...'
	@sudo apt-get -qq install python
	@echo 'Installing Python-dev...'
	@sudo apt-get -qq install python-dev
	@echo 'Installing mysql...'
	@sudo apt-get -qq install mysql-server
	@echo 'Installing libmysqlclient-dev'
	@sudo apt-get -qq install libmysqlclient-dev
	@echo 'Installing pip via apt-get...'
	@sudo apt-get -qq install python-pip
	@echo 'Installing pymysql'
	@sudo pip install pymysql
	@echo 'Installing some other sql python stuff'
	@sudo pip install mysql-python
	@echo 'Installing SQLAlchemy...'
	@sudo easy_install -q SQLAlchemy

# builds and populates the database
.PHONY: build_poof
build_poof:
	@echo 'Building database (gravehub)...'
	@echo '\. $(TABLES)' | sudo mysql
	@echo 'Tables built...'
	@echo '\. $(INSERTS)' | sudo mysql
	@echo 'Static data inserted...'
	@python populate.py
	@echo 'Database populated with sample data'
