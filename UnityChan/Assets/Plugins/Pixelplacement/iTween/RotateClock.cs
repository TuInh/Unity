using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RotateClock : MonoBehaviour {
	GameObject hour,minute,clock_temp,clock_full;
	Vector3 targetH,targetM;
	ActionListioner actionListenerScript;
	float hourIndex,minuteIndex,angleH,angleM;
	DateTime time;
	AudioSource myAudio;
	bool inAction = false,inRunanim= false;
	float duration = 7.0f;
	float currentTime= 0.0f;
	public static bool isTime;
	AudioClip myclip;
	void Start () {
		 myAudio = GetComponent<AudioSource>();
		myclip= Resources.Load("bababanana_ZJs7u4Sp") as AudioClip;
		hour = GameObject.Find ("hour");
		minute = GameObject.Find ("minute");
		clock_temp = GameObject.Find ("clock-temp");
		clock_full = GameObject.Find ("Clock-full_finalAnim");
		clock_full.SetActive (false);
		currentTime = Time.time + duration;
		Debug.Log ("istime la " + isTime);
		ReceiveAlarm ("alarm_begin");
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey ("a")) {
			Application.LoadLevel("myscene");
		}
		if (isTime == false) {
			if (inAction== true) {
				if (checkIdle ())
					return;
			} else {
				if(inRunanim== false)
					RunAnim();
			}
		}

	}
	void RunAnim ()
	{

		myAudio.clip = myclip;
		myAudio.Play ();
		inRunanim = true;
		clock_temp.SetActive (false);
		hour.SetActive (false);
		minute.SetActive (false);
		clock_full.SetActive (true);
		Vector3 punch = new Vector3 (0, -2, 0);
		iTween.PunchPosition (clock_full, punch, 1.0f);
		Vector3 punch1 = new Vector3 (0, 0, 10);
		//iTween.PunchRotation (clock_full, punch, 1.0f);
		Vector3 punch2 = new Vector3 (0, 0, -2);

		RotateLeft ();


	}
	void RotateLeft()
	{
		Debug.Log ("left");
		iTween.RotateTo(clock_full,iTween.Hash("x",0,"y",180,"z",10,"oncomplete","RotateRight"
		                                      , "time",0.05f
		                                       ,"oncompletetarget",this.gameObject
		                                       ));
	}
	void RotateRight()
	{
		Debug.Log ("right");
		iTween.RotateTo(clock_full,iTween.Hash("x",0,"y",180,"z",-10,"oncomplete","RotateLeft"
		                                       , "time",0.05f
		                                       ,"oncompletetarget",this.gameObject
		                                       ));
	}
	bool checkIdle ()
	{

		if (Time.time > currentTime) {
			inAction = false;		
			currentTime = Time.time + duration;

			return false;
		}
		return true;
	}
	void ReceiveAlarm (string message)
	{
		
		if (message != null) {
			if (message.Contains ("alarm_begin")) {
				if(isTime == true)
				{
					;
				}
				else
				{
					inAction = true;
				}

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
			else if(message.Contains("alarm_cancel")|| message.Contains("comeback"))
			{
				Debug.Log("cancel alarm, return myscene");
				Application.LoadLevel("myscene");
			}
			else if(message.Contains("alarm_begin_1"))
			{
				isTime = false;
				Debug.Log("Start jump to alarm scene");
				Application.LoadLevel("SceneAlarm");
			}
		}
	}
}
