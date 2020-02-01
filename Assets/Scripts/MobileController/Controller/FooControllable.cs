using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FooControllable : MonoBehaviour, IControllable
{
	public void OnInputAcquired(MobileInput mobileInput)
	{
		Debug.LogFormat("I'm being controlled, with input ({0})!", mobileInput.Value);
	}
}
