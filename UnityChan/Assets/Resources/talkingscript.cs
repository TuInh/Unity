using UnityEngine;
using System.Collections;

public class talkingscript : MonoBehaviour
{

	public static Animator anim;
	bool flag = false;
	float duration = -1f;
	float currentTime = 0.0f;
	int mode = 0;
	GameObject myGameObject;
	private static bool check = false;
	public static bool isIdle = true;
	public static bool isUnityChanexist = true;
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
		//currentTime = Time.time + duration;
		//myGameObject = (GameObject)Resources.Load ("UnityChan/Models/BoxUnityChan.fbx",typeof(GameObject));
		//ReceiveDuration ("cloud 10");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown ("1")) {
			ReceiveDuration ("cloud 10");
		}
		if (Input.GetKeyDown ("0")) {
			ReceiveDuration ("Start");
		}
		if (mode == 1) {
			mode = 0;
			anim.Play ("WAIT03");
			anim.SetBool ("isWait00", true);
			print ("mode =0");

		} else if (mode == 2) {

			if (!flag) {
					
				checkIdle ();
				anim.SetLayerWeight (1, 1);
				print ("checkidle");

			} else {
				print ("noi xong");
				anim.SetLayerWeight (1, 0);
				mode = 3;
			}
		}

	}

	public int getMode ()
	{
		return mode;
	}

	public void setMode (int mo)
	{
		mode = mo;
	}

	public void checkIdle ()
	{
		if (Time.time > currentTime) {
			print ("flag= true");
			flag = true;
			currentTime = Time.time + duration;
		}

	}

	void ReceiveDuration (string message)
	{
		print ("received");
		if (message != null) {

			if (isUnityChanexist == false || (isUnityChanexist == true && isIdle == true)) {
				if (message.Contains ("Start") == true) {
					isIdle = false;
					mode = 1;
				} else {
					mode = 2;
					isIdle = false;
					flag= false;
					char[] separate = {' '};
					string[] mang = message.Split (separate);
					string state = mang [0];
					duration = float.Parse (mang [1]);
					print ("enter mode........." + mode);
					
					currentTime = Time.time + duration;
					print ("duration la" + duration);
				}
				
			} else {
		
			}
		}

	}
}
