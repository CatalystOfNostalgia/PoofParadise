# The build script for the server

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
	@python server.py
