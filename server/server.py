from BaseHTTPServer import BaseHTTPRequestHandler, HTTPServer
import re
import os

class GraveHubHTTPRequestHandler(BaseHTTPRequestHandler):

	def do_GET(self):
		
		self.send_response(200)
		
		self.send_header('Content-type', 'text-html')
		self.end_headers()
		


		#if we are asking for a user
		if re.match('.*/user/.*', self.path):
			self.wfile.write('user')

		elif re.match('.*/user$', self.path):
			self.wfile.write('user homepage')		
		else: 
			self.wfile.write('hello')
		return

print('http server is starting...')

server_address = ('127.0.0.1', 8000)
httpd = HTTPServer(server_address, GraveHubHTTPRequestHandler)
print('http server is running...')
httpd.serve_forever()

if __name__ == '__main__':
	run

