from BaseHTTPServer import BaseHTTPRequestHandler, HTTPServer
from urlparse import urlparse, parse_qs
import re
import os

class GraveHubHTTPRequestHandler(BaseHTTPRequestHandler):

	def do_GET(self):

		self.send_response(200)

		self.send_header('Content-type', 'text-html')
		self.end_headers()

		if re.match('/login.*', self.path):
			loginInfo = parse_qs(urlparse(self.path).query)
			print(loginInfo)
			self.wfile.write('username: ' + loginInfo['user'][0] + '\n')
			self.wfile.write('password: ' + loginInfo['pass'][0])

		# if we are asking for a friend
		elif re.match('/friend.*', self.path):
			friendID = parse_qs(urlparse(self.path).query)
			print(friendID)
			self.wfile.write('friend: ' + friendID['user'][0] + '\n')

		# if we are asking for a user
		elif re.match('.*/users/.*', self.path):
			self.wfile.write('user')

		# asking for a specific user
		elif re.match('.*/users$', self.path):
			self.wfile.write('user homepage')

		# homepage
		elif re.match('/$', self.path):
			self.wfile.write('homepage')
		else:
			self.send_response(404)
			self.wfile.write('not a url')
		return
    def do_POST(self):
        
        self.send_response(200)
        self.wfile.write('Content-type: gravehub/json\n')
        self.wfile.write('Client: %s\n' % str(self.client_address))
        self.wfile.write('User-agent: %s\n' % str(self.headers['user-agent'])
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
                first_key=false
            self.wfile.write('"%s":"%s"' % (field, form[field].value)
        self.wfile.write('\n}')        


       

print('http server is starting...')

server_address = ('127.0.0.1', 8000)
httpd = HTTPServer(server_address, GraveHubHTTPRequestHandler)
print('http server is running on 127.0.0.1:8000')
httpd.serve_forever()

if __name__ == '__main__':
	run

