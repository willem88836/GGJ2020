﻿using UnityEngine;

public struct MobileInput
{
	public string InputType;
	public Vector3 Value;

	public MobileInput(string inputType, Vector3 value)
	{
		this.InputType = inputType;
		this.Value = value;
	}
}

public static class InputTypes
{
	public static string Movement = "MOVEMENTINPUT";
	public static string Jump = "JUMP";
}

