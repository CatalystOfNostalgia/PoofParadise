using UnityEngine;
using System.Collections;
using System.Net;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using HTTP;

public class getHttp : MonoBehaviour {
	
	void Update () {
	
	}

	public static Request request;

	void Start () {

		/*
		 * Create new account
		 */ 
		Hashtable data = new Hashtable();
		data.Add( "name", "Eric" );
		data.Add( "email", "something345@case.edu" );
		data.Add ("user", "DacoolchickenXP");
		data.Add ("pass", "notsecurepass");
		data.Add ("level", 9001);

		toCreate (data);

		/*
		 * login attempt
		 */ 
		string x = "Ted1";
		string y = "password";
		//string y = "wrongPass";
		toLogin (x, y);

		Hashtable data2 = new Hashtable();
		data.Add ("user", "Lisa");

		addFriend (data2);
	}

	/*
	 * Account creation 
	 * user enters their information as table 
	 * Table is posted onto the server
	 */
	void toCreate(Hashtable table){

		//link to SaveState script
		//PushtoServer
		HTTP.Request theRequest = new HTTP.Request( "post", "http://localhost:8000/create", table );
		theRequest.Send( ( request ) => {
			
			Hashtable result = request.response.Object;
			if ( result == null )
			{
				Debug.LogWarning( "Could not parse JSON response!" );
				return;
			}
			
		});
	}


	void addFriend(Hashtable table){
		HTTP.Request theRequest = new HTTP.Request( "post", "http://localhost:8000/friends", table );
		theRequest.Send( ( request ) => {
			
			Hashtable result = request.response.Object;
			if ( result == null )
			{
				Debug.LogWarning( "Could not parse JSON response!" );
				return;
			}
			
		});
	}
	//Login to site (using http get)
	void toLogin(string inputUser, string inputPass){

		string template = "http://localhost:8000/login?user={0}&pass={1}";
		string username = inputUser;
		string password = inputPass;
		string link = string.Format (template, username, password);
		string url = link;
		WWW www = new WWW(url);
		StartCoroutine (WaitForRequest (www));
	}


	 /*public IEnumerator toLogin2() {

		HTTP.Request someRequest = new HTTP.Request( "get", "http://localhost:8000/login?user=DacoolchickenXP&pass=notsecurepass" );
		someRequest.Send();
		
		while( !someRequest.isDone )
		{
			yield return null;
		}
		
		// parse some JSON, for example:
		JSONObject thing = new JSONObject( Request.response.Text );
		Console.Write (thing);
	}*/

	void toSave() {
	}


	/*private IEnumerator DoWWW(WWW www){
		string data = www.text;
		//JSONObject data  = www.;
		JSONObject j = new JSONObject(data);
		accessData (j);
		yield return www;
	}
	
	void accessData(JSONObject obj){
		switch(obj.type){
		case JSONObject.Type.OBJECT:
			for(int i = 0; i < obj.list.Count; i++){
				string key = (string)obj.keys[i];
				JSONObject j = (JSONObject)obj.list[i];
				Debug.Log(key);
				accessData(j);
			}
			break;
		case JSONObject.Type.ARRAY:
			foreach(JSONObject j in obj.list){
				accessData(j);
			}
			break;
		case JSONObject.Type.STRING:
			Debug.Log(obj.str);
			break;
		case JSONObject.Type.NUMBER:
			Debug.Log(obj.n);
			break;
		case JSONObject.Type.BOOL:
			Debug.Log(obj.b);
			break;
		case JSONObject.Type.NULL:
			Debug.Log("NULL");
			break;
			
		}
	}*/
	

	IEnumerator WaitForRequest(WWW www)
	{
		//string data = www.text;
		//return data;
		yield return www;
		
		//check for errors
		if (www.error == null) {
			Debug.Log("WWW Ok!: " + www.text);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
		
	}  

	/*public IEnumerator SomeRoutine() {
		HTTP.Request someRequest = new HTTP.Request( "get", "http://localhost:800/friends" );
		someRequest.Send();
		
		while( !someRequest.isDone )
		{
			yield return null;
		}
		
		// parse some JSON, for example:
		JSONObject thing = new JSONObject( Request.response.Text );
	}*/
	
	// Update is called once per frame
	// http://127.0.0.1:8000void Update () {}

}
