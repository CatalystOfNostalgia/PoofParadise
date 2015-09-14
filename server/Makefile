# The build script for the server

TABLES = sql/create_tables.sql

gravehub:
	@echo 'Installing Python...'
	@sudo apt-get -qq install python
	@echo 'Installing pip...'
	@sudo easy_install -q pip
	@echo 'Installing SQLAlchemy...'
	@sudo easy_install -q SQLAlchemy
	@echo 'Installing mysql...'
	@sudo apt-get -qq install mysql-server
	@echo 'Initializing server...'
# Insert line for building tables here (mysql)
	@echo 'Building database (gravehub)...'
	@echo '\. $(TABLES)' | sudo mysql
	@python server.py
