# The build script for the server

gravehub:
	@echo 'Installing Python...'
	@sudo apt-get -qq install python
	@echo 'Installing pip...'
	@sudo easy_install -q pip
	@echo 'Installing SQLAlchemy...'
	@sudo easy_install -q SQLAlchemy
	@echo 'Initializing server...'
	@python server.py
