from BaseHTTPServer import BaseHTTPRequestHandler, HTTPServer
import cgi
from urlparse import urlparse, parse_qs
import re
import os
import json
import cgi

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

			if re.match('/save', self.path):

				length = int(self.headers['Content-length'])
				self.send_response(200)
				response_json = self.rfile.read(length)
				parsed_json = json.loads(response_json)

				self.wfile.write('Post Successful!')
				print(parsed_json['user'])
				print('POST successful!')

			else:
				self.send_response(404)
				self.wfile.write("Not a url")

	def login(self, parameters):

		self.send_header('Content-type', 'text-html')
		self.end_headers()

		if 'user' in parameters and 'pass' in parameters:
			self.wfile.write('username: ' + parameters['user'][0] + '\n')
			self.wfile.write('password: ' + parameters['pass'][0])
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

