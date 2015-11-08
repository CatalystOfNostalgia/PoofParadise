﻿using UnityEngine;
using System.Collections;
using System.Net;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using HTTP;

public class GetHTTP : MonoBehaviour {

	void Update () {
	
	}

	public static Request request;

	/* commented out so that it doesn't do anything crazy
	void Start () {

		/*
		 * Create new account
		 *
		Hashtable data = new Hashtable();
		data.Add( "name", "Eric" );
		data.Add( "email", "something345@case.edu" );
		data.Add ("user", "DacoolchickenXP");
		data.Add ("pass", "notsecurepass");
		data.Add ("level", 9001);

		toCreate (data);

		/*
		 * login attempt
		 *
		string x = "Ted1";
		string y = "password";
		//string y = "wrongPass";
		toLogin (x, y);

		Hashtable data2 = new Hashtable();
		data.Add ("user", "Lisa");

		addFriend (data2);
	}
	*/

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

	//save to server
	public static void toSave(String jsonStuff){

		var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8000/save");
		httpWebRequest.ContentType = "application/json";
		httpWebRequest.Method = "POST";
		
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonStuff);
        //System.Buffer.BlockCopy(jsonStuff.ToCharArray(), 0, jsonBytes, 0, jsonBytes.Length);
        
        Stream writer = httpWebRequest.GetRequestStream();
        writer.Write(jsonBytes, 0, jsonBytes.Length);
        writer.Flush();
        writer.Close();

		HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
        
		Debug.Log(getHttpBody(new StreamReader(response.GetResponseStream()).ReadToEnd()));
        /*
		using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
		{
			var result = streamReader.ReadToEnd();
			Debug.Log (result);
            writer.close();
		}
        */

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
	// Attempts to log into the site, returns the user info in a json string
	public static string login(string inputUser, string inputPass){

		string template = "http://localhost:8000/login?user={0}&pass={1}";
		string username = inputUser;
		string password = inputPass;
		string link = string.Format (template, username, password);
		string url = link;

		HttpWebRequest loginrequest = (HttpWebRequest)WebRequest.Create (url);

		HttpWebResponse response = (HttpWebResponse)loginrequest.GetResponse ();

		return getHttpBody(new StreamReader(response.GetResponseStream()).ReadToEnd());
	
	}


	// this trims the headers from an http response
	private static String getHttpBody(String response) {

		int count = 0;
		String body = "";
		String[] full = response.Split ('\n');

		foreach (String line in full) {
			if (count > 2) {
				body += line;
			}
			else {
				count++;
			}
		}

		return body;

	}

	private IEnumerator WaitForRequest(WWW www)
	{

		Debug.Log ("waiting for request");

		yield return www;

		Debug.Log ("got response");

		//check for errors
		if (www.error == null) {
			Debug.Log("WWW Ok!: " + www.text);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
		
	}  

}
