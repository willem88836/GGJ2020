using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
	public float PowerLeft;

	bool _isActive = true;
	public int MaxPower = 7;
	public Slider Powerbar;

	public void Start()
	{
		Init();
	}

	public void Toggle(bool active)
	{
		_isActive = active;
	}

	public virtual void Init()
	{
		if (_isActive == false)
			return;

		Powerbar.maxValue = MaxPower;
		Powerbar.value = MaxPower;
		PowerLeft = MaxPower;
	}

	public void UsePower(float amount)
	{
		PowerLeft -= amount;
		Powerbar.value = PowerLeft;

		if (PowerLeft <= 0)
		{
			NullifyPower();
		}
	}

	// for inheriting purposes
	public virtual void NullifyPower()
	{

	}

	public bool HasPowerLeft()
	{
		if (PowerLeft > 0)
			return true;
		else
			return false;
	}
}
