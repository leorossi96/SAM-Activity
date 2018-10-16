using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class YoutubeVideoPlayerForMagiKRoom : MonoBehaviour {

    public static YoutubeVideoPlayerForMagiKRoom instance;
    public GameObject[] canvases;
    public SimplePlayback playersimpleFront;
    public SimplePlayback playersimpleFloor;
    HighQualityPlayback playerhighquality;
    public VideoPlayer player;
    public Camera[] cameras;
    string path;
	// Use this for initialization
	void Start () {
        instance = this;
        //playersimpleFront = GameObject.FindObjectOfType<SimplePlayback>();
        //playerhighquality = GameObject.FindObjectOfType<HighQualityPlayback>();
    }

    /// <summary>
    /// Play a video on the screen
    /// </summary>
    /// <param name="url">the url of the video on youtube or the file path</param>
    /// <param name="frontscreen">true if the video should be played on the front screen (element 0 in camras array), false to show it on the floor screen (element 1 in camras array)</param>
    public void PlayFromInput(string url, bool frontscreen, float starttime = 0, float endtime = 0)
    {
        if (frontscreen)
        {
            player.targetCamera = cameras[0];
            if (url != null && url != "")
            {
                path = url;
            }
            else
            {
                return;
            }
            //search for the low quality if not find search for highquality
            if (playersimpleFront != null)
            {
                player.targetCameraAlpha = 1;
                playersimpleFront.starttime = starttime;
                playersimpleFront.endtime = endtime;
                playersimpleFront.PlayYoutubeVideo(path);
                playersimpleFront.unityVideoPlayer.loopPointReached += OnVideoFinished;
            }
            else if (playerhighquality != null)
            {
                player.targetCameraAlpha = 1;
                playerhighquality.PlayYoutubeVideo(path);
                playerhighquality.unityVideoPlayer.loopPointReached += OnVideoFinished;
            }
        }
        else {
            player.targetCamera = cameras[1];
            if (url != null && url != "")
            {
                path = url;
            }
            else
            {
                return;
            }
            //search for the low quality if not find search for highquality
            if (playersimpleFloor != null)
            {
                player.targetCameraAlpha = 1;
                playersimpleFloor.starttime = starttime;
                playersimpleFloor.endtime = endtime;
                playersimpleFloor.PlayYoutubeVideo(path);
                playersimpleFloor.unityVideoPlayer.loopPointReached += OnVideoFinished;
            }
            else if (playerhighquality != null)
            {
                player.targetCameraAlpha = 1;
                playerhighquality.PlayYoutubeVideo(path);
                playerhighquality.unityVideoPlayer.loopPointReached += OnVideoFinished;
            }
        }
        foreach(GameObject g in canvases)
        {
            g.SetActive(false);
        }
        
    }

    private void OnVideoFinished(VideoPlayer vPlayer)
    {
        if (playersimpleFront != null)
        {
            player.targetCameraAlpha = 0;

            playersimpleFront.unityVideoPlayer.loopPointReached -= OnVideoFinished;
        }
        else if (playerhighquality != null)
        {
            player.targetCameraAlpha = 0;
            playerhighquality.unityVideoPlayer.loopPointReached -= OnVideoFinished;
        }
        foreach (GameObject g in canvases)
        {
            g.SetActive(true);
        }
    }
}
