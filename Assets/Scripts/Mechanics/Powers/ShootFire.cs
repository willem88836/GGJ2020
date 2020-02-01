using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShootFire : MonoBehaviour
{
	[SerializeField] GameObject _firePrefab;
	[SerializeField] float _offset = 0.5f;
	[SerializeField] float _projectileSpeed = 5;

	SpriteRenderer _character;

	public Action<float> OnPowerUsage;

	private void Start()
	{
		_character = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			Shoot();
	}

	public void Shoot()
	{
		GameObject projectile = Instantiate(_firePrefab, StartingPoint(), Quaternion.identity) as GameObject;
		Fire fire = projectile.GetComponent<Fire>();
		fire.Movespeed = Movespeed();
	}

	Vector2 StartingPoint()
	{
		float offset = 0;

		if (_character.flipX == false)
			offset = _offset;
		else
			offset = -_offset;

		Vector2 position = (Vector2)transform.position + new Vector2(offset, 0);

		return position;
	}

	float Movespeed()
	{
		float speed = 0;

		if (_character.flipX == false)
			speed = _projectileSpeed;
		else
			speed = -_projectileSpeed;

		return speed;
	}
}
