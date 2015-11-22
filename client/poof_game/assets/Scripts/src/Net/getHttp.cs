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

    //static String server = "http://129.22.150.55:51234";
    static String server = "http://localhost:51234";

	/*
	 * Account creation 
	 * user enters their information as table 
	 * Table is posted onto the server
	 */
	void toCreate(Hashtable table){

		HTTP.Request theRequest = new HTTP.Request( "post", server + "/create", table );
		theRequest.Send( ( request ) => {
			
			Hashtable result = request.response.Object;
			if ( result == null )
			{
				Debug.LogWarning( "Could not parse JSON response!" );
				return;
			}
			
		});
	}

    // create an account
    public static String createAccount(String name, String username, String password, String email) {

        String url = server + "/create";        

        String body = "{ \"name\": \"" + name + "\", ";
        body += "\"username\": \"" + username + "\", ";
        body += "\"password\": \"" + password + "\", ";
        body += "\"email\": \"" + email + "\"}";

        Debug.Log(body);

        byte[] jsonBytes = Encoding.UTF8.GetBytes(body);

        return body;

    }

	//save to server
	public static IEnumerator toSave(String jsonStuff){

		String url = server + "/save";
		byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonStuff);

		Dictionary<String, String> headers = new Dictionary<String, String>();

		headers.Add("Content-Type", "application/json");
		headers.Add ("Content-Length", jsonBytes.Length.ToString());
		WWW request = new WWW(url, jsonBytes, headers);

		yield return request;

		Debug.Log ("got request");
		Debug.Log (request.text);
	}


	void addFriend(Hashtable table){
		HTTP.Request theRequest = new HTTP.Request( "post", server + "/friends", table );
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

		string template = server + "/login?user={0}&pass={1}";
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
