using System.Collections.Concurrent;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Framework.ScriptableObjects.Variables;
using System.Collections.Generic;

public class ControllerServer : MonoBehaviour
{
	[SerializeField] private SharedString ServerIP;
	// HACK: This should only accept IControllable, but I don't feel like writing an inspector script.
	[SerializeField] private List<MonoBehaviour> controllables = new List<MonoBehaviour>();

	private Thread listeningThread;

	private TcpListener server;
	private TcpClient client;

	[SerializeField] private SharedBool isConnected;
	private bool isRunning;

	ConcurrentQueue<MobileInput> mobileInputQueue = new ConcurrentQueue<MobileInput>();


	public void Start()
	{
		for(int i = controllables.Count - 1; i >= 0; i--)
		{
			if (controllables[i].GetType() != typeof(IControllable))
			{
				controllables.RemoveAt(i);
			}
		}

		listeningThread = new Thread(new ThreadStart(RunServer));
		listeningThread.Name = "TCPServerThread";
		listeningThread.Start();
	}


	private void RunServer()
	{
		Debug.Log("Starting TCP Listening Thread...");

		// TODO: test this IP
		server = new TcpListener(IPAddress.Parse(ServerIP.Value), NetConfiguration.PORT);
		server.Start();
		isRunning = true;

		while (isRunning)
		{
			Debug.Log("Waiting for connection...");

			// Only accept one client
			client = server.AcceptTcpClient();
			Debug.LogFormat("Connected to client ({0})", client.ToString());

			byte[] data = new byte[NetConfiguration.TCPBUFFERSIZE];
			isConnected.Value = true;


			while (isConnected)
			{
				Debug.Log("Waiting for message...");

				NetworkStream stream = client.GetStream();
				Debug.Log(stream.ToString());
				
				int i;

				// Loop to receive all the data sent by the client.
				while ((i = stream.Read(data, 0, data.Length)) != 0)
				{
					// Translate data bytes to a ASCII string.
					string json = System.Text.Encoding.ASCII.GetString(data, 0, i);
					Debug.Log("received: " + json);
					MobileInput mobileInput = JsonUtility.FromJson<MobileInput>(json);
					mobileInputQueue.Enqueue(mobileInput);
				}
			}

			Debug.LogFormat("Terminating Connection with client ({0})", client.ToString());
		}

		Debug.Log("Terminating Server...");
	}

	//TODO: Add sending functionality? 

	public void TerminateConnection()
	{
		isConnected.Value = false;
	}

	public void TerminateServer()
	{
		TerminateConnection();
		server.Stop();
		isRunning = false;
	}


	private void OnDestroy()
	{
		TerminateServer();
	}

	private void Update()
	{
		while(mobileInputQueue.Count > 0)
		{
			MobileInput mobileInput;
			if (!mobileInputQueue.TryDequeue(out mobileInput))
				break;

			foreach (IControllable controllable in controllables)
			{
				controllable.OnInputAcquired(mobileInput);
			}
		}
	}
}
