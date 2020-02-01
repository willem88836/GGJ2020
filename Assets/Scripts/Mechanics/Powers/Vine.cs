using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Vine : MonoBehaviour
{
	[SerializeField] float _maxLength;

	[SerializeField] float _growSpeed;

	[SerializeField] float _powerUsage = 1;

	public Action<float> OnPowerUsage;

	BoxCollider2D _collider;

	Vector2 _startPosition;

	void Start()
	{
		_collider = GetComponent<BoxCollider2D>();
		_startPosition = transform.position;	
	}

	void Update()
	{
		if (OnPowerUsage == null)
			return;

		if (Input.GetKey(KeyCode.Alpha3))
			Grow();
	}

	void Grow()
	{
		if (Vector2.Distance(_startPosition, transform.position) > _maxLength)
			return;

		transform.Translate(Vector2.down * _growSpeed * Time.deltaTime);

		MoveCollider();

		OnPowerUsage.Invoke(_powerUsage * Time.deltaTime);
	}

	void MoveCollider()
	{
		Vector2 offset = _collider.offset;
		offset.y += _growSpeed * Time.deltaTime;
		_collider.offset = offset;

		Vector2 size = _collider.size;
		size.y += _growSpeed * Time.deltaTime * 2;
		_collider.size = size;
	}
}
