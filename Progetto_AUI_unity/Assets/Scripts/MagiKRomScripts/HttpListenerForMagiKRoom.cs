using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class HttpListenerForMagiKRoom : MonoBehaviour
{
    /// <summary>
    /// Singleton of the system
    /// </summary>
    public static HttpListenerForMagiKRoom instance;
    /// <summary>
    /// address of the listener
    /// </summary>
    public string address;
    /// <summary>
    /// port of the listener
    /// </summary>
    public int port;

    public delegate void RequestHandler(Match match, HttpListenerResponse response, string content);

    private Dictionary<Regex, RequestHandler> _requestHandlers = new Dictionary<Regex, RequestHandler>();

    HttpListener _listener;

    void Awake()
    {
        instance = this;
        _requestHandlers[new Regex(@"^/kinectposition$")] = HandleKinectPosition;
        _requestHandlers[new Regex(@"^/smarttoyevent$")] = HandleSmartToyEventPosition;
        _requestHandlers[new Regex(@"^/kinectaudio$")] = HandleKinectAudio;
        _requestHandlers[new Regex(@"^/speachtotext$")] = HandleSpeachToText;
        _requestHandlers[new Regex(@"^/dashboardStory$")] = HandleDashboardStory;
        _requestHandlers[new Regex(@"^/ExperienceManager$")] = HandleExperienceManager;
        _requestHandlers[new Regex(@"^/speachtotextOffline$")] = HandleSpeachToTextOffline;
        _requestHandlers[new Regex(@"^.*$")] = HandleDefault;

        
    }

    void Start()
    {

        _listener = new HttpListener();
        _listener.Prefixes.Add(address + ":" + port.ToString() + "/");

        _listener.Start();

        _listener.BeginGetContext(new AsyncCallback(ListenerCallback), _listener);
        signalReadyToExperienceManager();
    }

    void Destroy()
    {
        if (_listener != null)
        {
            _listener.Close();
        }
    }
    /// <summary>
    /// function called whenever thelistener get something
    /// </summary>
    /// <param name="result"></param>
    private void ListenerCallback(IAsyncResult result)
    {
        HttpListener listener = (HttpListener)result.AsyncState;
        // Call EndGetContext to complete the asynchronous operation.
        HttpListenerContext context = listener.EndGetContext(result);
        HttpListenerRequest request = context.Request;
        string contentread = new StreamReader(request.InputStream).ReadToEnd();
        // Obtain a response object.
        HttpListenerResponse response = context.Response;

        foreach (Regex r in _requestHandlers.Keys)
        {
            Match m = r.Match(request.Url.AbsolutePath);
            if (m.Success)
            {
                (_requestHandlers[r])(m, response, contentread);
                _listener.BeginGetContext(new AsyncCallback(ListenerCallback), _listener);
                return;
            }
        }

        response.StatusCode = 404;
        response.Close();
    }

    void Update()
    {
        if (httpcontent != null && httpcontent != "")
        {
            MagicRoomSmartToyManager.instance.updateFromTCPEvent(httpcontent);
            httpcontent = "";
        }
    }
    /// <summary>
    /// handler of the events when the message is directed to the kinect position 
    /// </summary>
    /// <param name="match"></param>
    /// <param name="response"></param>
    /// <param name="content"></param>
    private static void HandleKinectPosition(Match match, HttpListenerResponse response, string content)
    {
        MagicRoomKinectV2Manager.instance.setSkeletons(content);

        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Success");
        // Get a response stream and write the response to it.
        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        // You must close the output stream.
        output.Close();
    }
    private static  string httpcontent;
    /// <summary>
    /// handler of the events when the message is directed to the smart toy event  
    /// </summary>
    /// <param name="match"></param>
    /// <param name="response"></param>
    /// <param name="content"></param>
    private static void HandleSmartToyEventPosition(Match match, HttpListenerResponse response, string content)
    {
        Debug.Log(content);
        httpcontent = content;
        //MagicRoomSmartToyManager.instance.updateFromEvent(content);

        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Success");
        // Get a response stream and write the response to it.
        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        // You must close the output stream.
        output.Close();
    }
    /// <summary>
    /// handler of the events when the message is directed to the kinect audiocommand 
    /// </summary>
    /// <param name="match"></param>
    /// <param name="response"></param>
    /// <param name="content"></param>
    private static void HandleKinectAudio(Match match, HttpListenerResponse response, string content)
    {
        //MagicRoomKinectV2Manager.instance.setSkeletons(content);

        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Success");
        // Get a response stream and write the response to it.
        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        // You must close the output stream.
        output.Close();
    }

    /// <summary>
    /// handler of the events when the message is directed to the audiospeacker 
    /// </summary>
    /// <param name="match"></param>
    /// <param name="response"></param>
    /// <param name="content"></param>
    private static void HandleSpeachToText(Match match, HttpListenerResponse response, string content)
    {
        Debug.Log(content);
        MagicRoomTextToSpeechManagerOnline.instance.ReadyFile(content);
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Success");
        // Get a response stream and write the response to it.
        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        // You must close the output stream.
        output.Close();
    }

    /// <summary>
    /// handler of the events when the message is directed to the audiospeacker 
    /// </summary>
    /// <param name="match"></param>
    /// <param name="response"></param>
    /// <param name="content"></param>
    private static void HandleSpeachToTextOffline(Match match, HttpListenerResponse response, string content)
    {
        Debug.Log(content);
        MagicRoomTextToSpeachManagerOffline.instance.isCompleted();
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Success");
        // Get a response stream and write the response to it.
        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        // You must close the output stream.
        output.Close();
    }

    private static void HandleDashboardStory(Match match, HttpListenerResponse response, string content)
    {
        /*StoryStructure story = JsonUtility.FromJson<StoryStructure>(content);
        GameSettings.instance.storytobeplayed = story;*/
        receivedconfig = true;

        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Success");
        // Get a response stream and write the response to it.
        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        // You must close the output stream.
        output.Close();
    }

    public static bool receivedconfig = false;
    public static bool receivedforcedCommand = false;
    public static ForcedActions forcedcommand;
    private static void HandleExperienceManager(Match match, HttpListenerResponse response, string content)
    {
        string messageback="c";
        Debug.Log(content);
        /*try
        {
            GameConfiguration g = JsonUtility.FromJson<GameConfiguration>(content);
            if (g.storyname == null || g.storyname == "")
            {
                MessageFromExpManager mess = JsonUtility.FromJson<MessageFromExpManager>(content);
                if (mess.type != null && mess.type != "") {
                    //received event
                    if (mess.type == "key") {
                        forcedcommand = (ForcedActions) Enum.Parse(typeof(ForcedActions), mess.payload);
                        receivedforcedCommand = true;
                    }
                    messageback = "Successfully received command";
                }
                else
                {
                    GameSettings.instance.storytobeplayed = JsonUtility.FromJson<StoryStructure>(content);
                    GameSettings.instance.gameconfiguration.storyname = GameSettings.instance.storytobeplayed.title;
                    messageback = "Successfully received story";
                }
            }
            else
            {
                GameSettings.instance.gameconfiguration = g;
                messageback = "Successfully received configuration";
            }
            receivedconfig = true;
        }
        catch (Exception e)
        {
            messageback = e.Message;
        }
        */

        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(messageback);
        // Get a response stream and write the response to it.
        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        // You must close the output stream.
        output.Close();
    }

    /// <summary>
    /// handler of the events when the message is directed to unknown addresses
    /// </summary>
    /// <param name="match"></param>
    /// <param name="response"></param>
    /// <param name="content"></param>
    private static void HandleDefault(Match match, HttpListenerResponse response, string content)
    {
        response.StatusCode = 404;
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Not accetabe response");
        // Get a response stream and write the response to it.
        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        // You must close the output stream.
        output.Close();
    }

    public void signalReadyToExperienceManager()
    {
        ExperienceManagerComunication.instance.SendReadyCommand();
    }

    public void signalConclusionToExperienceManager()
    {
        ExperienceManagerComunication.instance.SendConcludedCommand();
    }
    
    public void signalStartToExperienceManager() {
        ExperienceManagerComunication.instance.SendStartedCommand();
    }
}

[Serializable]
public class MessageFromExpManager {
    public string type;
    public string payload;
}

public enum ForcedActions {
    repeat, forward, back, close
}