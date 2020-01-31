using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantPower : MonoBehaviour
{
	[SerializeField] int _maxPower = 7;
	public Slider Powerbar;

	void Start()
	{
		Powerbar.maxValue = _maxPower;
		Powerbar.value = _maxPower;
	}
}
