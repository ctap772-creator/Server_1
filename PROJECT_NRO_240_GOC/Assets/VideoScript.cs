using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour
{
	public static VideoScript instance { get; private set; }

	public VideoPlayer video;

	public Texture2D videoFrames;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		if (Rms.loadRMSInt("IntroOff") == 1)
		{
			gameObject.SetActive(false);
			return;
		}
		if (video == null)
		{
			video = Camera.main?.GetComponent<VideoPlayer>();
		}
		if (video != null)
		{
			video.Prepare();
			video.loopPointReached += OnVideoEnd;
			video.Play();
		}
		else
		{
			Debug.LogError("Không tìm thấy VideoPlayer trên Main Camera!");
		}
	}

	public void paint()
	{
		if (video != null && video.texture != null)
		{
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), video.texture);
		}
	}

	private void OnVideoEnd(VideoPlayer vp)
	{
		vp.loopPointReached -= OnVideoEnd;
		vp.Stop();
		vp.targetTexture = null;
		gameObject.SetActive(false);
	}

	private void OnGUI()
	{
		if (video != null)
		{
			paint();
		}
	}
}
