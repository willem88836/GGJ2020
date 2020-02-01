using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
	[HideInInspector] public float Movespeed;
	[SerializeField] float _lifetime = 1; 

	private void Update()
	{
		transform.Translate(Movespeed * Time.deltaTime, 0, 0);

		_lifetime -= Time.deltaTime;
		if (_lifetime <= 0)
			Remove();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("triggered");
		Remove();
	}

	void Remove()
	{
		Destroy(gameObject); // probably change this
	}
}
