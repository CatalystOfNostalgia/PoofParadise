from BaseHTTPServer import BaseHTTPRequestHandler, HTTPServer
from urlparse import urlparse, parse_qs
from sqlalchemy import select
import re
import os
import json
import sys

sys.path.insert(0, '/logic')
import queries

class GraveHubHTTPRequestHandler(BaseHTTPRequestHandler):

	def do_GET(self):

		self.send_response(200)

		parameters = parse_qs(urlparse(self.path).query)

		# logging in
		if re.match('/login', self.path):
			self.login(parameters)

		# looking for friends of a user
		elif re.match('/friends', self.path):
			self.getFriends(parameters)

		# looking for a specific user

		elif re.match('/users/.*', self.path):

			self.send_header('Content-type', 'text-html')
			self.end_headers()

			user_id = os.path.basename(urlparse(self.path).path)
			decorative_buildings = queries.get_user_decorative_buildings(user_id)
			resource_buildings = queries.get_user_resource_buildings(user_id)
				
			self.wfile.write('user id: ' + user_id)

		# asking for all users
		elif re.match('.*/users$', self.path):

			self.send_header('Content-type', 'text-html')
			self.end_headers()
			self.wfile.write('user homepage')


		# homepage
		elif re.match('/$', self.path):

			self.send_header('Content-type', 'text-html')
			self.end_headers()
			self.wfile.write('homepage')

		else:
			self.send_response(404)
		return

	def do_POST(self):

		self.send_header('Content-type', 'application/json')
		self.end_headers()
		
		# If the body isn't JSON then reject
		if self.headers['Content-Type'] != 'application/json':
			
			self.send_response(400)
			data = {'message' : 'Need JSON in the body'}
			self.wfile.write(json.dumps(data))

		else:

			length = int(self.headers['Content-Length'])
			response_json = self.rfile.read(length)
			parsed_json = json.loads(response_json)

			# creating an account
			if re.match('/create', self.path):
				self.send_response(200)
				self.createAccount(parsed_json)

			# saving
			elif re.match('/save', self.path):
				self.send_response(200)

				data = {'message' : 'Can\'t actually save yet...'}
				self.wfile.write(json.dumps(data))
				print(parsed_json['user'])
				print('Can\'t save yet...')

			elif re.match('/friends', self.path):
				user_id = parsed_json['user_id']
				friend_name = parsed_json['friend']

				friend = queries.find_user_from_username(friend_name)
				user =  queries.find_user_from_id(user_id)
			
				try:
					queries.add_friend(user_id, friend.user_id)

					print(user.username + ' and ' + friend.username + ' are now friends!')
					self.send_response(200)

				except:
					queries.rollback()
					self.send_response(400)
					data = {'error': 'already friends'}	
					self.wfile.write(json.dumps(data))

			else:
				self.send_response(404)
				self.wfile.write("Not a url")

	def createAccount(self, body):

		self.send_header('Content-Type', 'application/json')
		self.end_headers()

		if all (parameter in body for parameter in ('name', 'email', 'username', 'password')):
			name = body['name']
			email = body['email']
			username = body['username']
			password = body['password']

			data = {}

			try:
				queries.create_account(name = name, username = username, email = email, password = password)
				user_id = queries.log_in(username = username, password = password)
				data['user_id'] = user_id
				print("account created")
				print("name: " + name + "\nusername: " + username + "\nemail: " + email + '\n')

			except:
				queries.rollback()
				self.send_response(400)
				data['error'] = 'duplicate entry'
				print('Duplicate account entry attempted for\nname: ' + name + '\nusername' + username + '\nemail: ' + email + '\n')

			self.wfile.write(json.dumps(data))

			
		else:
			self.send_response(400)
			data = {}
			data['error'] = 'missing parameter'
			self.wfile.write(json.dumps(data))

	def login(self, parameters):

		self.send_header('Content-type', 'application/json')
		self.end_headers()

		if 'user' in parameters and 'pass' in parameters:
			username = parameters['user'][0]
			password = parameters['pass'][0]
			
			user = queries.log_in(username = username, password = password)

			data = {}

			if user is None: 
				self.send_response(400)
				data['error'] = 'no username password combination exists'
				print("user: " + username + " failed to log in")

			else:
				self.send_response(200)

				resource_buildings = queries.get_user_resource_buildings(user.user_id)
				decorative_buildings = queries.get_user_decorative_buildings(user.user_id)
				data['user_id'] = user.user_id
				data['experience'] = user.experience
				data['headquarters_level'] = user.headquarters_level
				data['level'] = user.level
				data['resource_buildings'] = resource_buildings
				data['decorative_buildings'] = decorative_buildings

				print("user: " + username + " is logging in with user id: " + str(user.user_id) + "\n")

			self.wfile.write(json.dumps(data))


		else:
			self.send_response(400)
			self.wfile.write('Need a username and password')

	# returns the friends of a user in JSON
	def getFriends(self, parameters):

		self.send_header('Content-type', 'application/json')
		self.end_headers()

		data = {}

		if 'user' in parameters:
			data['friends'] = 'no friends'
			self.wfile.write(json.dumps(data))
		else:
			self.send_response(400)
			data['error'] = 'need a user ID'
			self.wfile.write(json.dumps(data))

	#turns a building into json
	def convertToJSON(self, building):

		data = {}

print('http server is starting...')

server_address = ('127.0.0.1', 8000)
httpd = HTTPServer(server_address, GraveHubHTTPRequestHandler)
print('http server is running on 127.0.0.1:8000')
httpd.serve_forever()

if __name__ == '__main__':
	run
