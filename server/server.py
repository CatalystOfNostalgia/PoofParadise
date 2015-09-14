from BaseHTTPServer import BaseHTTPRequestHandler, HTTPServer
from urlparse import urlparse, parse_qs
import re
import os

class GraveHubHTTPRequestHandler(BaseHTTPRequestHandler):

	def do_GET(self):

		self.send_response(200)

		self.send_header('Content-type', 'text-html')
		self.end_headers()

		parameters = parse_qs(urlparse(self.path).query)
		# logging in
		if re.match('/login.*', self.path):

			if 'user' in parameters and 'pass' in parameters:
				self.wfile.write('username: ' + parameters['user'][0] + '\n')
				self.wfile.write('password: ' + parameters['pass'][0])
			else:
				self.send_response(400)
				self.wfile.write('Need a username and password')

		# looking for friends of a user
		elif re.match('/friends.*', self.path):

			if 'user' in parameters:
				self.wfile.write('friends of ' + queries['user'][0] + ': \n')
			else:
				self.send_response(400)
				self.wfile.write('Need a user ID')

		# looking for a specific user
		elif re.match('.*/users/.*', self.path):

			userName = os.path.basename(urlparse(self.path).path)
			self.wfile.write('user: ' + userName)

		# asking for all users
		elif re.match('.*/users$', self.path):

			self.wfile.write('user homepage')

		# homepage
		elif re.match('/$', self.path):
			self.wfile.write('homepage')

		else:
			self.send_response(404)
		return

print('http server is starting...')

server_address = ('127.0.0.1', 8000)
httpd = HTTPServer(server_address, GraveHubHTTPRequestHandler)
print('http server is running on 127.0.0.1:8000')
httpd.serve_forever()

if __name__ == '__main__':
	run

