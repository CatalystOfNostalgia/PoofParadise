using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
	public static IEnumerator toSave(String jsonStuff){


		String url = "http://localhost:8000/save";
		byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonStuff);

		Dictionary<String, String> headers = new Dictionary<String, String>();

		headers.Add("Content-Type", "application/json");
		headers.Add ("Content-Length", jsonBytes.Length.ToString());
		WWW request = new WWW(url, jsonBytes, headers);

		yield return request;

		Debug.Log ("got request");
		Debug.Log (request.text);

		/*
        // get a byte array
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonStuff);

        // creating the web request
		HttpWebRequest request = 
            (HttpWebRequest)WebRequest.Create("http://localhost:8000/save");

		request.ContentType = "application/json";
		request.Method = "POST";
        request.ContentLength = jsonBytes.Length;
        
        // write the request body
        using (Stream writer = request.GetRequestStream()) {
            writer.Write(jsonBytes, 0, jsonBytes.Length);
        }

        // get the response
        WebResponse response = request.GetResponse();
        Stream data = response.GetResponseStream();

        //read the response

        // close everything
        response.Close();
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
