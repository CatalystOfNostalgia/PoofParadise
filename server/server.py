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

        # get static info
        elif re.match('.*/static$', self.path):
            
            self.send_header('Content-type', 'application/json')
            self.end_headers()
            static_info = queries.get_static_info()

        # homepage
        elif re.match('/$', self.path):

            self.send_header('Content-type', 'text-html')
            self.end_headers()
            self.wfile.write('homepage')

        else:
            self.send_response(404)
        return

    def do_POST(self):

        # If the body isn't JSON then reject
        if self.headers['Content-Type'] != 'application/json':
            
            self.send_response(400)
            data = {'message' : 'Need JSON in the body'}
            self.wfile.write(json.dumps(data))
            print ('content type is wrong')
            return

        else:

            length = int(self.headers['Content-Length'])
            response_json = self.rfile.read(length)

            try:
                parsed_json = json.loads(response_json)
            except:
                print response_json
                print('could not parse json')
                data = {'error' : 'Could not parse JSON'}
                return 

            # creating an account
            if re.match('/create', self.path):
                self.createAccount(parsed_json)

            # saving
            elif re.match('/save', self.path):

                required_items = ['name', 'level', 'email', 'user_id', \
                                  'username', 'password', 'experience', \
                                  'hq_level', 'resource_buildings', \
                                  'decorative_buildings'\
                                 ]

                # update the data and send a success response
                if all (item in parsed_json for item in (required_items)):

                    self.save(parsed_json)

                # if the required elements are not present send an error message
                else:
                    
                    self.send_response(400)
                    data = {'error' : 'Missing json items'}
                    self.wfile.write(json.dumps(data))
                    print response_json
                    print('failed saving: missing json items')

            # adding a friend connection
            elif re.match('/friends', self.path):
                user_id = parsed_json['user_id']
                friend_name = parsed_json['friend']

                friend = queries.find_user_from_username(friend_name)
                user =  queries.find_user_from_id(user_id)
            
                try:
                    queries.add_friend(user_id, friend.user_id)

                    print(user.username + ' and ' + \
                          friend.username + ' are now friends!')

                    self.send_response(200)

                except:
                    queries.rollback()
                    self.send_response(400)
                    data = {'error': 'already friends'} 
                    self.wfile.write(json.dumps(data))

            else:
                self.send_response(404)
                self.wfile.write("Not a url")

    # creates an account in the database
    def createAccount(self, body):

        self.send_header('Content-Type', 'application/json')
        self.end_headers()

        required_parameters = ['name', 'email', 'username', 'password'] 

        if all (parameter in body for parameter in required_parameters):
            name = body['name']
            email = body['email']
            username = body['username']
            password = body['password']

            data = {}

            try:
                queries.create_account(name = name, \
                                       username = username, \
                                       email = email, \
                                       password = password \
                                      )

                user = queries.log_in(username = username, \
                                         password = password \
                                        )
                
                data['user_id'] = user.user_id
                data['message'] = "Account Created"

                print("account created")
                print("name: " + name + \
                      "\nusername: " + username + \
                      "\nemail: " + email + '\n' \
                      )
                self.send_response(200)

            except:
                queries.rollback()
                self.send_response(400)
                data['error'] = 'duplicate entry'
                print('Duplicate account entry attempted for\nname: ' + name + \
                      '\nusername' + username + 
                      '\nemail: ' + email + '\n' \
                     )
            print(data)
            self.wfile.write(json.dumps(data))

            
        else:
            self.send_response(400)
            data = {}
            data['error'] = 'missing parameter'
            self.wfile.write(json.dumps(data))

    # logs into the database and returns user info
    def login(self, parameters):

        data = {}
        self.send_header('Content-type', 'application/json')
        self.end_headers()

        if 'user' in parameters and 'pass' in parameters:
            username = parameters['user'][0]
            password = parameters['pass'][0]
            
            user = queries.log_in(username = username, password = password)

            if user is None: 
                self.send_response(400)
                data['error'] = 'No username password combination exists'
                print("user: " + username + " failed to log in")

            else:
                self.send_response(200)

                resource_buildings = queries.get_user_resource_buildings(user.user_id)
                decorative_buildings = queries.get_user_decorative_buildings(user.user_id)
                data['name'] = user.name
                data['username'] = user.username
                data['email'] = user.email
                data['password'] = user.password
                data['user_id'] = user.user_id
                data['experience'] = user.experience
                data['headquarters_level'] = user.headquarters_level
                data['level'] = user.level
                data['resource_buildings'] = resource_buildings
                data['decorative_buildings'] = decorative_buildings
                data['fire'] = user.fire
                data['water'] = user.water
                data['air'] = user.air
                data['earth'] = user.earth
                data['max_fire'] = user.max_fire
                data['max_water'] = user.max_water
                data['max_earth'] = user.max_earth
                data['max_air'] = user.max_air
                data['fire_ele'] = user.fire_ele
                data['water_ele'] = user.water_ele
                data['earth_ele'] = user.earth_ele
                data['air_ele'] = user.air_ele
                data['poof_count'] = user.poof_count
                data['hq_pos_x'] = user.hq_pos_x
                data['hq_pos_y'] = user.hq_pos_y
                print("user: " + username + \
                      " is logging in with user id: " + \
                      str(user.user_id) + "\n")

            self.wfile.write(json.dumps(data))

        else:
            self.send_response(400)
            data['error'] = 'Need a username and password'
            print("no username and password given")
            self.wfile.write(json.dumps(data))

    # returns the friends of a user in JSON
    def getFriends(self, parameters):

        self.send_header('Content-type', 'application/json')
        self.end_headers()

        data = {}

        if 'user' in parameters:
            friends = queries.get_friends(parameters['user'][0])
            data['friends'] = friends
            self.wfile.write(json.dumps(data))
        else:
            self.send_response(400)
            data['error'] = 'need a user ID'
            self.wfile.write(json.dumps(data))

    # saves the users info and building data
    def save(self, parsed_json):

        self.wfile.write('jello')

        resource_buildings = parsed_json['resource_buildings']
        decorative_buildings = parsed_json['decorative_buildings']
        user_id = parsed_json['user_id']

        queries.save_user_info(parsed_json)
        building_ids = queries.save_building_info(resource_buildings, \
                                           decorative_buildings, \
                                           user_id\
                                          )

        data = {}

        if building_ids != None :
            data['message'] = 'Save successful'
            data['building_ids'] = building_ids
            self.send_response(200)
            self.wfile.write(json.dumps(data))
            print(json.dumps(data));
            print(parsed_json['username'] + ' saved\n')
        else:
            data['error'] = 'cannot find building'
            self.send_response(400)
            self.wfile.write(json.dumps(data))
            print('cannot find building')


# starting the server
print('http server is starting...')
port_number = 51234
#server_address = ('129.22.150.55', port_number)
server_address = ('localhost', port_number)
httpd = HTTPServer(server_address, GraveHubHTTPRequestHandler)
print('http server is running on 127.0.0.1:{value}'.format(value=port_number))
httpd.serve_forever()

if __name__ == '__main__':
    run
