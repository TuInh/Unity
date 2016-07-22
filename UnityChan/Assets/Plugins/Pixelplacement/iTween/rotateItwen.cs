using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class rotateItwen : MonoBehaviour {

	GameObject hour,minute,clock_temp,clock_full;
	Vector3 targetH,targetM;
	ActionListioner actionListenerScript;
	float hourIndex,minuteIndex,angleH,angleM;
	DateTime time;
	bool inAction = false,inRunanim= false;
	float duration = 7.0f;
	float currentTime= 0.0f;
	void Start () {
		
		hour = GameObject.Find ("hour");
		minute = GameObject.Find ("minute");
		clock_temp = GameObject.Find ("clock-temp");
		clock_full = GameObject.Find ("Clock-full_finalAnim");
		clock_full.SetActive (false);
		currentTime = Time.time + duration;
		
		ReceiveTime("alarm_begin");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey ("a")) {
			Application.LoadLevel("myscene");
		}
		

	}

	void ReceiveTime (string message)
	{
		
		if (message != null) {
			if (message.Contains ("alarm_begin")) {
				inAction = true;
				Debug.Log("start display alarm");
				hourIndex= DateTime.Now.Hour;
				minuteIndex= DateTime.Now.Minute;
				angleH= 5 -360+ ( hourIndex/12.0f * 360+ 30 * minuteIndex/60.0f);
				angleM = 60-360+ minuteIndex/60.0f *360;
				
				targetH = new Vector3 (0, 180, angleH);
				targetM = new Vector3 (0, 180, angleM);
				iTween.RotateTo (hour,targetH,0.1f);
				iTween.RotateTo (minute,targetM,0.1f);
				
			} 
			else if(message.Contains("alarm_cancel"))
			{
				Debug.Log("cancel alarm, return myscene");
				Application.LoadLevel("myscene");
			}
		}
	}
}
