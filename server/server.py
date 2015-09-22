from BaseHTTPServer import BaseHTTPRequestHandler, HTTPServer
from urlparse import urlparse, parse_qs
import re
import os
import json
import queries
from sqlalchemy import select
#from models import User

class GraveHubHTTPRequestHandler(BaseHTTPRequestHandler):

	def do_GET(self):

		self.send_response(200)

		parameters = parse_qs(urlparse(self.path).query)

		# logging in
		if re.match('/login.*', self.path):
			self.login(parameters)

		# looking for friends of a user
		elif re.match('/friends.*', self.path):
			self.getFriends(parameters)

		# looking for a specific user

		elif re.match('.*/users/.*', self.path):

			self.send_header('Content-type', 'text-html')
			self.end_headers()

			if re.match('.*/users/.*/building', self.path):
				userName = os.path.dirname(urlparse(self.path).path).split('/')[2]
				self.wfile.write('userid: ' + userName)
				u = User.query.filter_by(user_id=userName).first()
				self.wfile('email: ' + u.email)

			else:
				userName = os.path.basename(urlparse(self.path).path)
				self.wfile.write('user: ' + userName)

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

		self.send_header('Content-type', 'text-html')
		self.end_headers()
		
		if self.headers['Content-Type'] != 'application/json':
			
			self.send_response(400)
			self.wfile.write('Need JSON in the body')

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

				self.wfile.write('Post Successful!')
				print(parsed_json['user'])
				print('POST successful!')

			else:
				self.send_response(404)
				self.wfile.write("Not a url")

	def createAccount(self, json):
		self.send_header('Content-Type', 'application/json')
		self.end_headers()

		if all (parameter in json for parameter in ('name', 'email', 'username')):
			name = json['name']
			email = json['email']
			username = json['username']
			queries.createAccount(name = name, username = username, email = email)
			print("account created")
			print("name: " + name + "\nusername: " + username + "\nemail: " + email)
			
		else:
			self.send_response(400)

	def login(self, parameters):

		self.send_header('Content-type', 'application/json')
		self.end_headers()

		if 'user' in parameters and 'pass' in parameters:
			user_id = queries.logIn(parameters['user'][0])
			data = {}
			data['user_id'] = user_id
			self.wfile.write(json.dumps(data))

			print("user: " + str(user_id) + " is logging in")
		else:
			self.send_response(400)
			self.wfile.write('Need a username and password')

	# returns the friends of a user in JSON
	def getFriends(self, parameters):

		self.send_header('Content-type', 'application/json')
		self.end_headers()

		if 'user' in parameters:
			#self.wfile.write('friends of ' + queries['user'][0] + ': \n')
			self.wfile.write(json.dumps({'user': parameters['user'][0], 'friends': [{'name': 'Eric'}]}, sort_keys=False))
		else:
			self.send_response(400)
			#self.wfile.write('Need a user ID')


print('http server is starting...')

server_address = ('127.0.0.1', 8000)
httpd = HTTPServer(server_address, GraveHubHTTPRequestHandler)
print('http server is running on 127.0.0.1:8000')
httpd.serve_forever()

if __name__ == '__main__':
	run

