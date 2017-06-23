using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Collections;
using System.Net;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class MqttController : MonoBehaviour
{
	// VR Controller component
    private SteamVR_TrackedObject trackedObj;
    private MqttClient client;

    // variables
    public string brokerIp = "192.168.10.4";
	public string localIp = "192.168.10.3";
    public int brokerPort = 1883;
    public string clientId = "HTC-VIVE";
    public string username = "sender";
    public string password = "ostfalia";
	public string topicVideo = "car/video/";
	public string topicControl = "car/control/";
	public string topicStop = "car/control/stop";
    public string videoUdpPort = "1337";
    public string videoWidth = "1344";
    public string videoHeight = "376";
    public string videoFps = "30";

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void sendControlMsg(string msg)
    {
        if (client != null && client.IsConnected)
        {
			if (msg.StartsWith ("2")) { // stop msg -> qos 1
				client.Publish (topicStop, System.Text.Encoding.UTF8.GetBytes (msg), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
			} else { // normal steering msg -> qos 0
				client.Publish (topicControl, System.Text.Encoding.UTF8.GetBytes (msg), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
			}
        }
    }

    void Start()
    {
        Debug.Log("connecting...");
        trackedObj = GetComponent<SteamVR_TrackedObject>();

        // create client instance 
        client = new MqttClient(IPAddress.Parse(brokerIp), brokerPort, false, null);
        client.Connect(clientId, username, password, true, 10);
        Debug.Log("connected");

		// start video stream when connected to mqtt
        sendVideoRefresh();
    }

    private void sendVideoRefresh()
    {
        if (client == null || !client.IsConnected)
        {
            Debug.Log("Not connected");
            return;
        }
		// send mqtt msg with qos 2
		client.Publish(topicVideo, System.Text.Encoding.UTF8.GetBytes(videoUdpPort + " " + localIp + videoWidth + " " + videoHeight + " " + videoFps), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
    }

    private void OnApplicationQuit()
    {
        if (client != null)
        {
            client.Disconnect();
        }
    }

    void Update()
    {
		// Refresh the video stream when clicking the 'R' button.
		// TODO use a vive controller button instead
		if (Input.GetKeyDown (KeyCode.R)) {
			sendVideoRefresh ();
		}

        if (Controller.GetAxis() != Vector2.zero)
        {
            if (Controller.GetHairTrigger() == true)
            {
                // negative x -> left
                // negative y -> back
                
                float x = Controller.GetAxis ().x;
				float y = Controller.GetAxis ().y;
                
				if (x < 0.3 && x > -0.3 && y > 0.3) {
					Debug.Log ("straight");
                    sendControlMsg("0 2");
                } else if (x < 0.3 && x > -0.3 && y < -0.3) {
					Debug.Log ("back");
                    sendControlMsg("1 2");
                } else if (x < 0.3 && x > -0.3 && y > -0.3 && y < 0.3) {
					Debug.Log ("stop");
                    sendControlMsg("2 2");
                } else if (x >= 0.3 && y >= 0.0) {
					Debug.Log ("straight right");
                    sendControlMsg("0 1");
                } else if (x >= 0.3 && y < 0.0) {
					Debug.Log ("back right");
                    sendControlMsg("1 1");
                } else if (x <= -0.3 && y >= 0.0) {
					Debug.Log ("straight left");
                    sendControlMsg("0 0");
                } else if (x <= -0.3 && y < 0.0) {
					Debug.Log ("back left");
                    sendControlMsg("1 0");
                }
            }
        }

        if (Controller.GetHairTriggerUp())
		{
			Debug.Log ("stop");
            sendControlMsg("2 0");
        }
    }
}
