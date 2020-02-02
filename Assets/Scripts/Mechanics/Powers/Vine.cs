using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : Interactable
{
	[SerializeField] float _maxLength;

	[SerializeField] float _growSpeed;

	[SerializeField] float _powerUsage = 1;

	public Action<float> OnPowerUsage;

	[SerializeField] BoxCollider2D _collider;

	Vector2 _startPosition;

	void Start()
	{
		_startPosition = transform.position;	
	}

	void Update()
	{
		if (OnPowerUsage == null)
			return;

		//if (Input.GetKey(KeyCode.Alpha3))
		//	Grow();
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

	public override void Dehighlight()
	{
		Renderer.sprite = DefaultSprite[0];
	}

	public override void Highlight()
	{
		Renderer.sprite = HightlightSprite[0];
	}

	public override void Interact(MobileInput input)
	{
		Grow();
	}
}
