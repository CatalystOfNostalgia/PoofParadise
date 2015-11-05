using UnityEngine;
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
		
        byte[] jsonBytes = new byte[jsonStuff.Length * sizeof(char)];
        System.Buffer.BlockCopy(jsonStuff.ToCharArray(), 0, jsonBytes, 0, jsonBytes.Length);

        char[] jsonChars = jsonStuff.ToCharArray();
        Stream writer = httpWebRequest.GetRequestStream();
        
        for(int i = 0; i < jsonBytes.Length; i++) {
            writer.WriteByte(jsonBytes[i]);
        }

        writer.Flush();
        writer.Close();
        
        /*
		using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
			streamWriter.Write(jsonstuff.ToCharArray());
            Debug.Log(streamWriter.ToString());
			streamWriter.Flush();
			streamWriter.Close();
		}
        */

		var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
		{
			var result = streamReader.ReadToEnd();
			Debug.Log (result);
		}

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
