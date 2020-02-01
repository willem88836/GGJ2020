using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantPower : Power
{
	[SerializeField] PlantGrower[] _plants;
	[SerializeField] Mushroom[] _mushrooms;
	[SerializeField] Vine[] _vines;

	public override void Init()
	{
		base.Init();

		foreach (PlantGrower plant in _plants)
			plant.OnPowerUsage += UsePower;

		foreach (Mushroom mushroom in _mushrooms)
			mushroom.OnPowerUsage += UsePower;

		foreach (Vine vine in _vines)
			vine.OnPowerUsage += UsePower;
	}

	public override void NullifyPower()
	{
		base.NullifyPower();

		foreach (PlantGrower plant in _plants)
			plant.OnPowerUsage = null;

		foreach (Mushroom mushroom in _mushrooms)
			mushroom.OnPowerUsage = null;

		foreach (Vine vine in _vines)
			vine.OnPowerUsage = null;
	}
}
