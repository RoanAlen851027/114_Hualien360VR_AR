/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.Video;

public class TestVideoPlay : MonoBehaviour
{
    [SerializeField] string videoFileName;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            videoFileName = "Pet.mp4";
            PlayVideo();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            videoFileName = "Time.mp4";
            PlayVideo();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            videoFileName = "Type.mp4";
            PlayVideo();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            videoFileName = "Visit.mp4";
            PlayVideo();
        }
    }


    public void PlayVisit()
    {
        videoFileName = "Visit.mp4";
        PlayVideo();
    }


    public void PlayType()
    {
        videoFileName = "Type.mp4";
        PlayVideo();
    }

    public void PlayTime()
    {
        videoFileName = "Time.mp4";
        PlayVideo();
    }

    public void PlayPet()
    {
        videoFileName = "Pet.mp4";
        PlayVideo();
    }

    public void PlayVideo()
    {
        VideoPlayer videoPlayer  = GetComponent<VideoPlayer>();

        if (videoPlayer)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            Debug.Log(videoPath);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
        }
    }
}
