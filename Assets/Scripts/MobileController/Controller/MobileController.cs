using UnityEngine;
using Framework.ScriptableObjects.Variables;
using System.Net.Sockets;
using UnityEngine.Events;
using System;

public class MobileController : MonoBehaviour
{
	[SerializeField] private SharedString serverIP;
	[SerializeField] private UnityEvent onConnectEvent;
	[SerializeField] private UnityEvent onConnectFailEvent;

	private TcpListener server;
	private TcpClient client;


	public void RunClient()
	{
		try
		{
			client = new TcpClient(serverIP.Value, NetConfiguration.PORT);
			Debug.Log(client);
			onConnectEvent.Invoke();
		}
		catch (Exception e)
		{
			Debug.LogError(e.Message);
			onConnectFailEvent.Invoke();
		}
	}

	public void SendMobileInput(MobileInput mobileInput)
	{
		try
		{
			string json = JsonUtility.ToJson(mobileInput) + NetConfiguration.SPLITCHAR;
			byte[] data = System.Text.Encoding.ASCII.GetBytes(json);

			NetworkStream stream = client.GetStream();
			stream.Write(data, 0, data.Length);

			Debug.LogFormat("Sent message ({0}) to server", json);
		}
		catch(Exception e)
		{
			Debug.LogWarning(e.Message);
		}
	}


	public void SendProxyInput()
	{
		MobileInput input = new MobileInput("vector3tests", Vector3.one);
		SendMobileInput(input);
	}
}
