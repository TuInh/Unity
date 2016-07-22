using UnityEngine;
using System.Collections;

public class Spectrum : MonoBehaviour {
	
	// Use this for initialization
	public GameObject prefab;
	public int numberOfObjects = 25;
	public float radius = 5f;
	public GameObject[] cubes;
	//variable for audio part
	public System.IO.DirectoryInfo musicfolder;
	public string myPath;
	public WWW myClip;
	public AudioClip audioClip;
	public  AudioSource myaudio;
	public AudioClip otherClip;
	public int songIndex;
	public string playControl;
	Vector3[] path;
	void Start ()
	{
		path = new Vector3[numberOfObjects+1];
		
		
		
		
		for (int i=0; i<numberOfObjects; i++) {
			float angle = i * Mathf.PI * 2 / numberOfObjects;
			Vector3 pos = new Vector3 (Mathf.Cos (angle), 0, Mathf.Sin (angle)) * radius;
			path [i] = new Vector3 (Mathf.Cos (angle), 0, Mathf.Sin (angle)) * radius;
			Instantiate (prefab, pos, Quaternion.identity);
		}
		path [numberOfObjects] = path [0];
		
		cubes = GameObject.FindGameObjectsWithTag ("cubes");
		for (int i=0; i<numberOfObjects; i++) {
			
			Vector3[] pathClone = new Vector3[numberOfObjects+1];
			for(int j=0;j<numberOfObjects;j++)
			{
				int index= i+j;
				if(index>= numberOfObjects)
				{
					index= index - numberOfObjects;
				}
				pathClone[j]= path[index];
			}
			pathClone[numberOfObjects] = pathClone[0];
			iTween.MoveTo(cubes[i],iTween.Hash(
				"path" , pathClone,
				"speed" , 1f,
				"delay",0.0f,
				"loopType" , iTween.LoopType.loop,
				"easeType" , iTween.EaseType.linear
				
				));
		}
		
		//load and play song
		myaudio = GetComponent<AudioSource>();
		myPath = "/mnt/sdcard/Music";
		musicfolder = new System.IO.DirectoryInfo (myPath);
		//Debug.Log ("tungnt "+"file://" + musicfolder.GetFiles()[0].FullName);
		//	myClip = new WWW ("file://" + musicfolder.GetFiles()[0].FullName);
		//	yield return new WaitForSeconds(1);
		//	otherClip = myClip.GetAudioClip(false,false);
		/*	if (otherClip != null) {
			Debug.Log (" clip != null");
		} else {
			Debug.Log (" clip == null");
		}
		if (myaudio != null) {
			Debug.Log ("audio != null");
		} else {
			Debug.Log ("audio == null");
		}*/
		//	myaudio.clip = otherClip;

		//	Debug.Log ("length"+ otherClip.length);
		//	myaudio.Play ();
	}
	void OnDrawGizmos()
	{
		iTween.DrawPath (path);		
	}

	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("3")) {
			Application.LoadLevel("myscene");
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
		if (Input.GetKeyDown ("4")) {
			ReceiveFFT("song_3_play");
		}
		float[] spectrum = AudioListener.GetSpectrumData (1024,0,FFTWindow.Hamming);
		for (int i=0; i<numberOfObjects; i++) {
			Vector3 previousScale = cubes[i].transform.localScale;
			previousScale.y = Mathf.Lerp(previousScale.y,spectrum[i]*40,Time.deltaTime*30);
			cubes[i].transform.localScale = previousScale;
		}
	}
	void ReceiveFFT(string message)
	{
		Debug.Log ("jumped: ReceiveFFT");
//		if (message != null)
//		{
//			if (message.Contains ("song"))
//			{
//				Debug.Log ("jumped: message.Contains (song)");
//				string[] mang = message.Split ('_');
//				playControl = mang[1];
//				string songName = mang[2];
//
//				if(playControl.Equals("play"))
//				{
					Debug.Log ("jumped: playControl.Equals(play), index: "+songIndex);
					StartCoroutine(LoadnPlay(message));
					Debug.Log ("jumped: after playControl.Equals(play), index: "+songIndex);
					
//				}
//				else if(playControl.Equals("pause"))
//				{
					//if(myaudio.isPlaying)
					//{
//						myaudio.Pause();
					//}
//				}
//				else if(playControl.Equals("continue"))
//				{
					//if(!myaudio.isPlaying)
					//{
//						myaudio.UnPause();
					//}
//				}
//				else if(playControl.Equals("stop"))
//				{
					//if(myaudio.isPlaying)
					//{
//						myaudio.Stop();
					//}
//				}
//			} 
//			else if (message.Contains("comeback"))
//			{
//				Application.LoadLevel("myscene");
//			}
//			else if(message.Contains("alarm_begin"))
//			{
//				RotateClock.isTime= false;
//				Debug.Log("Start jump to alarm scene");
//				Application.LoadLevel("SceneAlarm");
//			}
//		}
	}
	IEnumerator LoadnPlay(string songPath)
	{
		Debug.Log ("LoadnPlay");
//		Debug.Log ("tungnt "+ "file:///mnt/sdcard/Music/"+songName);
//		myClip = new WWW (" file:///mnt/sdcard/Music/"+songName);
		myClip = new WWW (songPath);
		yield return new WaitForSeconds(1);
		otherClip = myClip.GetAudioClip(false,false);
		if (otherClip != null) 
		{
		Debug.Log (" clip != null");
		} else {
			Debug.Log (" clip == null");
		}
		if (myaudio != null) {
			Debug.Log ("audio != null");
		} else {
			Debug.Log ("audio == null");
		}

		myaudio.loop = true;
		myaudio.clip = otherClip;
		//yield return new WaitForSeconds(5);
		Debug.Log ("length"+ otherClip.length);
		myaudio.Play ();
	}
}