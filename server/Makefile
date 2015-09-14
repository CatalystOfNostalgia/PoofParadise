# The build script for the server

TABLES = sql/create_tables.sql

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
	@echo 'Installing pip...'
	@sudo easy_install -q pip
	@echo 'Installing pymysql'
	@echo pip install pymysql
	@echo 'Installing some other sql python stuff'
	@echo pip install mysql-python
	@echo 'Installing SQLAlchemy...'
	@sudo easy_install -q SQLAlchemy
	@echo 'Installing mysql...'
	@sudo apt-get -qq install mysql-server

.PHONY: build_gravehub
build_gravehub:
	@echo 'Building database (gravehub)...'
	@echo '\. $(TABLES)' | sudo mysql



