using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayVideo : MonoBehaviour {
	private string movPath = "Dancing_1111.mp4";
	
	// Use this for initialization
	void Start () {
		StartCoroutine(PlayStreamingVideo(movPath));
	}
	
	private IEnumerator PlayStreamingVideo(string url)
	{
		Handheld.PlayFullScreenMovie(url, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFill);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		print ("completed");
		Debug.Log("Video playback completed.");
	}
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
	}
	void ReceiveMessageVideo(string message)
	{
		Debug.Log ("jumped: ReceiveMessageVideo");
		if (message.Contains("comeback"))
		{
			Application.LoadLevel("myscene");
		}
	}
}

	


