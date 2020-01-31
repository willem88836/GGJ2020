using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.ScriptableObjects.Variables;
using System.Net;
using System.Net.Sockets;


public class MobileController : MonoBehaviour
{
	[SerializeField] private SharedString serverIP;

	private TcpListener server;
	private TcpClient client;


	private void Start()
	{
		RunClient();
	}


	private void RunClient()
	{
		client = new TcpClient(serverIP.Value, NetConfiguration.PORT);
		Debug.Log(client);
	}

	public void SendMobileInput(MobileInput mobileInput)
	{
		string json = JsonUtility.ToJson(mobileInput);
		byte[] data = System.Text.Encoding.ASCII.GetBytes(json);

		NetworkStream stream = client.GetStream();
		stream.Write(data, 0, data.Length);

		Debug.LogFormat("Sent message ({0}) to server", json);
	}


	public void SendProxyInput()
	{
		MobileInput input = new MobileInput("valeyteyp", 98f);
		SendMobileInput(input);
	}
}
