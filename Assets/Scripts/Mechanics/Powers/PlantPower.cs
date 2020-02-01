using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantPower : Power
{
	[SerializeField] PlantGrower[] _plants;

	public override void Init()
	{
		base.Init();

		foreach (PlantGrower plant in _plants)
		{
			plant.OnPowerUsage += UsePower;
		}
	}

	public override void NullifyPower()
	{
		base.NullifyPower();

		foreach (PlantGrower plant in _plants)
		{
			plant.OnPowerUsage = null;
		}
	}
}
