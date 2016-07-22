
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using System;
	public class ActionListioner : MonoBehaviour
		{
			
			GameObject unityChan;
			GameObject weather_back,weather_right,weather_left,weather_front;
			Animator unityChanAnim;
			string currentAnimation;
			bool inAction,inAlarm = false;
			int n0,n1;
			float duration = -1f;
			float currentTime = 0.0f;
			int lastAction;
			int waitState, runFState, runLState, runRState,winState,slideState
				, walkBState,walkFState,walkLState,walkRState;
			List<GameObject> weatherLst;
			List<Vector3> positionLst;
			
			int state;
			int temperature;
			int isDay;
			int isTomorrow;
			GameObject gameObject;
			TextMesh date;
			TextMesh city;
			String name_city;
			
			// Use this for initialization
			void Start ()
			{
				
				unityChan = GameObject.Find ("unitychan");
				unityChanAnim = unityChan.GetComponent<Animator> ();
				lastAction = -1;	
				waitState = Animator.StringToHash ("Base Layer.WAIT00");
				runFState = Animator.StringToHash ("Base Layer.RUN00_F");
				runLState = Animator.StringToHash ("Base Layer.RUN00_L");
				runRState = Animator.StringToHash ("Base Layer.RUN00_R");
				winState = Animator.StringToHash ("Base Layer.WIN00");
				slideState= Animator.StringToHash ("Base Layer.SLIDE00");
				
				walkFState = Animator.StringToHash ("Base Layer.WALK00_F");
				walkLState = Animator.StringToHash ("Base Layer.WALK00_L");
				walkRState = Animator.StringToHash ("Base Layer.WALK00_R");
				positionLst = new List<Vector3> ();
				
				//16 cai dau + 19 (mist)
				positionLst.Add(new Vector3 (0f, 10.5f, 0f));
				positionLst.Add(new Vector3 (0f, 10.5f, 0f));
				positionLst.Add(new Vector3 (0f, 12.5f, 0f));
				positionLst.Add(new Vector3 (0f, 12.5f, 0f));
				
				//nhiet do
				positionLst.Add(new Vector3 (-4.81f, 0f, 1f));//vi tri hang chuc cua nhiet do
				positionLst.Add(new Vector3 (-2.35f, 0f, 1f));//vi tri hang don vi cua nhiet do
				positionLst.Add(new Vector3 (-0.06f, 0f, 1f));// vi tri cua oC
				
				//trang thai 16,17,18: may mat troi
				positionLst.Add(new Vector3 (0f, 0f, 1f));
				positionLst.Add(new Vector3 (0f, 0f, 1f));
				positionLst.Add(new Vector3 (0f, 0f, 1f));
				positionLst.Add(new Vector3 (0f, 0f, 1f));
				
				//clock
				positionLst.Add(new Vector3 (-9.87f, 0f, 1f));
				positionLst.Add(new Vector3 (-5.4f, 0f, 1f));
				positionLst.Add(new Vector3 (-3.8f, 0f, 1f));
				positionLst.Add(new Vector3 (-3.6f, 0f, 1f));
				positionLst.Add(new Vector3 (-1f, 0f, 1f));
				positionLst.Add(new Vector3 (1.7f, 0f, 1f));
				positionLst.Add(new Vector3 (3f, 0f, 1f));
				positionLst.Add(new Vector3 (5.43f, 0f, 1f));
				
				
				
			}
			
			// Update is called once per frame
			void Update ()
			{
				
				//print (time.Hour + ":"+time.Minute+":"+time.Second);
				//print (unityChanAnim.GetCurrentAnimatorStateInfo (0).fullPathHash + " " + waitState);
				if (inAction) {
					if (checkIdle ())
						return;
				} else {
					if (lastAction == 1) {
						RunMode (2);
					}
				}
			if (inAlarm) {
				if(checkIdleAlarm())
					return;
				else
				{
					unityChanAnim.Play("WAIT00");
				}
			}
				
				if (unityChan.activeSelf == true &&  (!unityChanAnim.GetCurrentAnimatorStateInfo (0).fullPathHash.Equals (waitState) && 
				                                      !unityChanAnim.GetCurrentAnimatorStateInfo (0).fullPathHash.Equals (runFState) && 
				                                      !unityChanAnim.GetCurrentAnimatorStateInfo (0).fullPathHash.Equals (runLState) && 
				                                      !unityChanAnim.GetCurrentAnimatorStateInfo (0).fullPathHash.Equals (runRState) && 
				                                      !unityChanAnim.GetCurrentAnimatorStateInfo (0).fullPathHash.Equals (slideState) && 
				                                      
				                                      !unityChanAnim.GetCurrentAnimatorStateInfo (0).fullPathHash.Equals (walkFState) && 
				                                      !unityChanAnim.GetCurrentAnimatorStateInfo (0).fullPathHash.Equals (walkRState) && 
				                                      !unityChanAnim.GetCurrentAnimatorStateInfo (0).fullPathHash.Equals (walkLState) &&
				                                      ! unityChanAnim.GetCurrentAnimatorStateInfo (0).fullPathHash.Equals (winState)) )
					return;
				
				unityChanAnim.SetLayerWeight (1, 0);
				
				if (Input.GetKeyDown ("1")) {
					//RunMode(0);
					print ("Pressed 1");
					ReceiveDuration ("weather_3_1_-20_0_hanoi");
					
				}
				if (Input.GetKeyDown ("2")) {

					print ("Pressed 2");
					ReceiveDuration ("alarm_set");
					
				}
				if (Input.GetKeyDown ("0")) {
					
					print (0);
					ReceiveDuration ("start");
				}
			if (Input.GetKeyDown ("4")) {
				
				print (4);
				ReceiveDuration ("alarm_begin_1");
			}
				if (Input.GetKeyDown ("9")) {
					print (3);
					ReceiveDuration ("comeback");
					
				}
				if (Input.GetKeyDown (KeyCode.Escape)) {
					Application.Quit();
				}
				if (Input.GetKeyDown ("3")) {
					Application.LoadLevel("scene1");
				}

				if (Input.GetKeyDown ("d")) {
					ReceiveDuration ("show_time");
					
				}
				
			}
			
			void ReceiveDuration (string message)
			{
				
				if (message != null) {
					if (message.Contains ("start")) {
						print ("nhan mode 0");
						RunMode (0);
						lastAction = 0;
					} 
					else if (message.Contains ("RUN00_F")) {
						print ("nhan mode 4");
						RunMode (4);
						lastAction = 4;
					}
					else if (message.Contains ("RUN00_L")) {
						RunMode (5);
						lastAction = 5;
					}
					else if (message.Contains ("RUN00_R")) {
						RunMode (6);
						lastAction = 6;
					}
					else if (message.Contains ("WIN00")) {
						RunMode (7);
						lastAction = 7;
					}
					else if (message.Contains ("SLIDE00")) {
						RunMode (8);
						lastAction = 8;
					}
					
					else if (message.Contains ("WALK00_F")) {
						RunMode (9);
						lastAction = 9;
					}
					else if (message.Contains ("WALK00_L")) {
						RunMode (10);
						lastAction = 10;
					}
					else if (message.Contains ("WALK00_R")) {
						RunMode (11);
						lastAction = 11;
					}else if (message.Contains ("weather")) {
						char[] separate = {'_'};
						string[] mang = message.Split (separate);
						state = int.Parse(mang [1]);
						duration = float.Parse (mang [2]);
						temperature = int.Parse (mang [3]);
						isTomorrow = int.Parse (mang[4]);
						name_city = mang[5];
						name_city = name_city.ToUpper();
						currentTime = Time.time + duration;
						RunMode (1);
						lastAction = 1;
					} 
				else if(message.Contains ("alarm_remove") || message.Contains("alarm_set")) {
				Debug.Log("Set alarm or remove alarm");	
				currentTime = Time.time + 4.0f;
					lastAction= 12;
					RunMode(12);
				}
				else if (message.Contains ("comeback")) {
						RunMode (3);
						lastAction = 2;
					} else if(message.Contains("playsong"))
					{
						Application.LoadLevel("scene1");
					}
					else if(message.Contains("playvideo"))
					{
						Application.LoadLevel("SceneVideo");
					}
					else if(message.Contains("alarm_begin"))
					{
					if(message.Contains("0"))
						RotateClock.isTime = true;
					else if(message.Contains("1"))
					RotateClock.isTime = false;
						Debug.Log("Start jump to alarm scene");
						Application.LoadLevel("SceneAlarm");
					}

				}
			}
			void Awake()
			{
				//DontDestroyOnLoad (this);
			}
				void RunMode (int mode)
			{
				
				switch (mode) {
					// Say Hello
				case 0:
					print("thuc hien mode 0");
					if (!unityChan.activeSelf) {
						unityChan.SetActive (true);
						ClearList();
					}
					unityChanAnim.Play ("WAIT03");
					unityChanAnim.SetBool ("isWait", true);
					break;
					// Say Set alarm or remove alarm
			case 12:
				print("thuc hien mode 12");
				if (!unityChan.activeSelf) {
					unityChan.SetActive (true);
					ClearList();
				}

				unityChanAnim.SetLayerWeight (1, 1);
				inAlarm = true;
			inAction= true;
				break;
					//Say Temperature
				case 1:
					
					
					if (!unityChan.activeSelf) {
						unityChan.SetActive (true);
						ClearList();
						
						//hamsetActive(weather_index,false);
					}
					unityChanAnim.SetLayerWeight (1, 1);
					inAction = true;
					break;
					
				case 4:
					print("thuc hien mode 4");
					if (!unityChan.activeSelf) {
						unityChan.SetActive (true);
						ClearList();
					}
					unityChanAnim.Play ("RUN00_F");
					
					break;
				case 5:
					if (!unityChan.activeSelf) {
						unityChan.SetActive (true);
						ClearList();
					}
					unityChanAnim.Play ("RUN00_L");
					
					break;
					
				case 6:
					if (!unityChan.activeSelf) {
						unityChan.SetActive (true);
						ClearList();
					}
					unityChanAnim.Play ("RUN00_R");
					
					break;
					
				case 7:
					if (!unityChan.activeSelf) {
						unityChan.SetActive (true);
						ClearList();
					}
					unityChanAnim.Play ("WIN00");
					
					break;
					
				case 8:
					if (!unityChan.activeSelf) {
						unityChan.SetActive (true);
						ClearList();
					}
					unityChanAnim.Play ("SLIDE00");
					
					break;
					
					
				case 9:
					if (!unityChan.activeSelf) {
						unityChan.SetActive (true);
						ClearList();
					}
					unityChanAnim.Play ("WALK00_F");
					
					break;
				case 10:
					if (!unityChan.activeSelf) {
						unityChan.SetActive (true);
						ClearList();
					}
					unityChanAnim.Play ("WALK00_L");
					
					break;
					
				case 11:
					if (!unityChan.activeSelf) {
						unityChan.SetActive (true);
						ClearList();
					}
					unityChanAnim.Play ("WALK00_R");
					
					break;
					
					
					//Display Temperature After Say Temperature
				case 2:
					inAction = false;
					unityChan.SetActive (false);
					if(temperature>=0)
					{
						n0=temperature/10;
						n1= temperature %10;
					}
					else
					{
						n0 = -temperature/10;
						n1 = -temperature%10;
					}
					RunModeWeather();
					
					//hamsetActive(6,true);
					break;
				case 3:
					if(unityChan.activeSelf == false)
					{
						unityChan.SetActive (true);
						ClearList();
						print ("run mode3");
					}
					else
					{
						unityChanAnim.Play ("WAIT00");
					}
					
					//inAction = false;
					break;
				default:
					break;
				}
				
				//lastAction = mode;
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
		bool checkIdleAlarm ()
		{
			if (Time.time > currentTime) {

				inAlarm = false;
				currentTime = Time.time + duration;
				return false;
			}
			return true;
		}
			void RunModeWeather()
			{
				if(weatherLst == null)
				{
					weatherLst = new List<GameObject>();
					
					
					switch( state)
					{
					case 0: //clear sky day
					{
						LoadSingleWeather(0,false);
						break;
					}
					case 1: // clear sky night
					{
						LoadSingleWeather(6,false);
						break;
					}
						
					case 2: //few cloud co mat troi
					{
						LoadSingleWeather(16,true);
						
						break;
					}
					case 3: //few cloud co mat trang
					{
						LoadSingleWeather(1,false);
						break;
					}
						
					case 4: // scarated cloud
					{
						LoadSingleWeather(2,false);
						break;
					}
					case 5:// scratted cloud
					{
						LoadSingleWeather(2,false);
						break;
					}
					case 6: //broken cloud
					{
						LoadSingleWeather(2,false);
						//	LoadSingleWeather(16,true);
						break;
					}
					case 7: // broken cloud
					{
						LoadSingleWeather(2,false);
						break;
					}
						
					case 8: //Shower rain
					{
						LoadSingleWeather(3,false);
						
						break;
					}
					case 9: //Shower rain
					{
						LoadSingleWeather(3,false);
						//	LoadSingleWeather(16,true);
						break;
					}
						
					case 10: //  rain
					{
						LoadSingleWeather(17,true);
						//	LoadSingleWeather(16,true);
						break;
					}
					case 11://  rain
					{
						LoadSingleWeather(18,true);
						break;
					}
						
					case 12: //thunderstorm
					{
						LoadSingleWeather(13,false);
						break;
					}
					case 13: // thunderstorm
					{
						LoadSingleWeather(13,false);
						break;
					}
						
					case 14: //snow
					{
						LoadSingleWeather(5,false);
						
						break;
					}
					case 15: //snow
					{
						LoadSingleWeather(5,false);
						break;
					}
						
					case 16: // mist
					{
						LoadSingleWeather(19,false);
						break;
					}
					case 17:// mist
					{
						LoadSingleWeather(19,false);
						break;
					}
						
					}
					
					LoadTemp();
				}
				
			}
			void ClearList()
			{
				if (weatherLst != null) {
					for (int i =weatherLst.Count-1; i>=0; i--) {
						Destroy(weatherLst[i]);
					}
					weatherLst.Clear ();
					weatherLst = null;
				}
				
				
				
			}
			void LoadSingleWeather(int index,bool isSecond)
			{
				
				if (isSecond == false) {
					for (int j=0; j<4; j++) {
						
						
						gameObject = Instantiate (Resources.Load (index + "_" + j, typeof(GameObject)))as GameObject;
						gameObject.transform.position = positionLst [j];
						weatherLst.Add (gameObject);
						
					}
				} else {
					for (int j=0; j<4; j++) {
						
						
						gameObject = Instantiate (Resources.Load (index + "_" + j, typeof(GameObject)))as GameObject;
						gameObject.transform.position = positionLst [j+7];
						weatherLst.Add (gameObject);
						
					}
				}
				
			}
			void LoadTemp()
			{
				if (temperature < 0) {
					gameObject = Instantiate (Resources.Load ( "negative2", typeof(GameObject)))as GameObject;
					gameObject.transform.position = new Vector3 (-6f,0f,1f);
					gameObject.transform.localScale = new Vector3 (1f,1f,0.5f);
					gameObject.transform.Rotate (0f, 180f, 0f);
					weatherLst.Add(gameObject);
				}
				gameObject = Instantiate (Resources.Load ( "n" + n0, typeof(GameObject)))as GameObject;
				gameObject.transform.position = positionLst[4];
				gameObject.transform.localScale = new Vector3 (1f,1f,2f);
				gameObject.transform.Rotate (0f, 180f, 0f);
				weatherLst.Add(gameObject);
				
				gameObject = Instantiate (Resources.Load ( "n" + n1, typeof(GameObject)))as GameObject;
				gameObject.transform.position = positionLst[5];
				gameObject.transform.localScale = new Vector3 (1f,1f,2f);
				gameObject.transform.Rotate (0f, 180f, 0f);
				weatherLst.Add(gameObject);
				
				gameObject = Instantiate (Resources.Load ( "n10" , typeof(GameObject)))as GameObject;
				gameObject.transform.position = positionLst[6];
				gameObject.transform.localScale = new Vector3 (1f,1f,2f);
				gameObject.transform.Rotate (0f, 180f, 0f);
				weatherLst.Add(gameObject);
				
				gameObject = Instantiate (Resources.Load ( "date", typeof(GameObject)))as GameObject;
				date = (TextMesh)gameObject.transform.GetComponent(typeof(TextMesh));
				if(isTomorrow==0)
					date.text = "Now";
				else
					date.text = "Tomorrow";
				weatherLst.Add(gameObject);
				
				gameObject = Instantiate (Resources.Load ( "city", typeof(GameObject)))as GameObject;
				city = (TextMesh)gameObject.transform.GetComponent(typeof(TextMesh));
				if (name_city != null)
					city.text = name_city;
				weatherLst.Add(gameObject);
			}
			
			
		}
