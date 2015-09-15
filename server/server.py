from BaseHTTPServer import BaseHTTPRequestHandler, HTTPServer
import cgi
from urlparse import urlparse, parse_qs
import re
import os
import json

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

		self.send_response(200)
		self.wfile.write('Content-type: gravehub/json\n')
		self.wfile.write('Client: %s\n' % str(self.client_address))
		self.wfile.write('User-agent: %s\n' % str(self.headers['user-agent']))
		self.wfile.write('Path: %s\n' % self.path)
		self.end_headers()
		#only post allowed is to save gamestate

		form = cgi.FieldStorage(fp=self.rfile, headers=self.headers, environ={'REQUEST_METHOD':'POST', 'CONTENT_TYPE':self.headers['CONTENT_TYPE'],})
		self.wfile.write('{\n')
		first_key = True

		for field in form.keys():

			if not first_key:
				self.wfile.write(',\n')
			else:
				self.wfile.write('\n')
				first_key=False
		self.wfile.write('"%s":"%s"' % (field, form[field].value))
		self.wfile.write('\n}')

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

