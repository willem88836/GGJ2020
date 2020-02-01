using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Interactable
{
	Vector2 _startSize;
	[SerializeField] Vector2 _newSize;

	[SerializeField] float _growSpeed;
	float T;

	[SerializeField] float _powerUsage = 1;

	public Action<float> OnPowerUsage;

    void Start()
    {
		_startSize = transform.localScale;
    }

    void Update()
    {
		if (OnPowerUsage == null)
			return;

		//if (Input.GetKey(KeyCode.Alpha2))
		//	Grow();
    }

	void Grow()
	{
		if (T > 1)
			return;

		T += _growSpeed * Time.deltaTime;
		transform.localScale = Vector2.Lerp(_startSize, _newSize, T);

		OnPowerUsage.Invoke(_powerUsage * Time.deltaTime);
	}


	public override void Dehighlight()
	{
		Debug.LogWarning("Dehighlighting shroom");
	}

	public override void Highlight()
	{
		Debug.LogWarning("Highlighting shroom");
	}

	public override void Interact(MobileInput input)
	{
		Grow();
	}
}
