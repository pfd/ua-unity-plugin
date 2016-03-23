﻿/*
 Copyright 2016 Urban Airship and Contributors
*/

using UnityEngine;
using System.Collections;
using UrbanAirship;

public class UrbanAirshipBehaviour : MonoBehaviour
{
	public string addTagOnStart;

	void Awake ()
	{
		UAirship.Shared.UserNotificationsEnabled = true;
	}

	void Start ()
	{

		if (!string.IsNullOrEmpty (addTagOnStart)) {
			UAirship.Shared.AddTag (addTagOnStart);
		}

		UAirship.Shared.OnPushReceived += OnPushReceived;
		UAirship.Shared.OnChannelUpdated += OnChannelUpdated;

		CheckDeepLink ();
	}

	void OnDestroy ()
	{
		UAirship.Shared.OnPushReceived -= OnPushReceived;
		UAirship.Shared.OnChannelUpdated -= OnChannelUpdated;
	}

	void OnApplicationPause (bool pauseStatus)
	{
		if (!pauseStatus) {
			CheckDeepLink ();
		}
	}

	void OnPushReceived(PushMessage message) {
		Debug.Log ("Received push! " + message.Alert);
	}

	void OnChannelUpdated(string channelId) {
		Debug.Log ("Channel updated: " + channelId);
	}

	void CheckDeepLink ()
	{
		Debug.Log ("Checking for deeplink.");

		string deepLink = UAirship.Shared.GetDeepLink (true);
		if (!string.IsNullOrEmpty (deepLink)) {
			Debug.Log ("Launched with deeplink! " + deepLink);
			// Assume everything is a Bonus level for now
			Application.LoadLevel ("Bonus");
		}
	}
}