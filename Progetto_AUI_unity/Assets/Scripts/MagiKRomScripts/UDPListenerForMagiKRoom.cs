using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UDPListenerForMagiKRoom : MonoBehaviour {
    /// <summary>
    /// singlton of the script
    /// </summary>
    public static UDPListenerForMagiKRoom instance;
    /// <summary>
    /// address of the listener
    /// </summary>
    public string address;
    /// <summary>
    /// port of the listener
    /// </summary>
    public int port;
    /// <summary>
    /// true if the the application need to stop reading the udp stream, false otherwise
    /// </summary>
    private  bool _stop;

    /// <summary>
    /// the client
    /// </summary>
    static UdpClient client;
    

    /// <summary>
    /// the last package received from the UPD
    /// </summary>
    public string lastReceivedUDPPacket = "";
    /// <summary>
    /// true if the message has been received
    /// </summary>
    private static bool messageReceived;

    void Awake ()
    {
        instance = this;
        _stop = true;
    }

    /// <summary>
    /// start the receiver
    /// </summary>
    public void StartReceiver()
    {
        _stop = false;
        client = new UdpClient(port);
        messageReceived = false;
        UdpStates udpstate = new UdpStates();
        udpstate.e = new IPEndPoint(IPAddress.Parse(address), port); ;
        udpstate.u = client;
        client.BeginReceive(new AsyncCallback(ReceiveCallback), udpstate);
    }
    
    private void Update()
    {
        if (!_stop) {
            if (messageReceived)
            {
                string temp = lastReceivedUDPPacket;
                if (temp != null && temp != "")
                {
                    try
                    {
                        MagicRoomSmartToyManager.instance.updateFromUDPEvent(temp);
                        //lastReceivedUDPPacket = null;
                        messageReceived = false;
                    }
                    catch
                    {
                        print("message unreadable");
                    }
                    finally
                    {
                        UdpStates udpstate = new UdpStates();
                        udpstate.e = new IPEndPoint(IPAddress.Parse(address), port); ;
                        udpstate.u = client;
                        client.BeginReceive(new AsyncCallback(ReceiveCallback), udpstate);
                    }
                }
            }
        }
    }
    /// <summary>
    /// Function calledwhen the client has received
    /// </summary>
    /// <param name="ar"></param>
    public void ReceiveCallback(IAsyncResult ar)
    {
        UdpClient u = (UdpClient)((UdpStates)(ar.AsyncState)).u;
        IPEndPoint e = (IPEndPoint)((UdpStates)(ar.AsyncState)).e;
        Byte[] receiveBytes = u.EndReceive(ar, ref e);
        //if (receiveBytes.Length > 0)
        //{
            string receiveString = Encoding.ASCII.GetString(receiveBytes);

            Debug.Log("Received: " + receiveString);
            lastReceivedUDPPacket = receiveString;
            messageReceived = true;
        //}
    }

    /// <summary>
    /// stop the receiver
    /// </summary>
    public void StopReceiver()
    {
        _stop = true;
        if (client != null)
        {
            client.Close();
        }
    }

    /// <summary>
    /// what the system has to do when the gameobject is destroyed
    /// </summary>
    private void OnDestroy()
    {
        StopReceiver();
    }
}

[Serializable]
public class tcpPackage
{
    public string id;
    public UDPEvent[] events;
}

[Serializable]
public class udpPackage {
    public string id;
    public sensorstreamposition[] gyroscope;
    public sensorstreamposition[] accelerometer;
    public float[] position;
    public UDPEvent[] state;
}

[Serializable]
public class sensorstreamposition
{
    public string sensorId;
    public float x;
    public float y;
    public float z;
}

[Serializable]
public class UDPEvent {
    public string typ;
    public string id;
    public string val;
    public int dur;
}

class UdpStates
{
    public IPEndPoint e;
    public UdpClient u;
}