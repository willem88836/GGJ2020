﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrower : MonoBehaviour
{
	[SerializeField] float _snappingDistance = 2;
	[SerializeField] GameObject _treeCollider;

	BoxCollider2D _boxCollider;

	Vector2 _currentPosition;
	Vector2 _dragPosition;

	void Start()
	{
		_currentPosition = transform.position;
		_boxCollider = GetComponent<BoxCollider2D>();
	}

	void OnMouseDrag()
	{
		_dragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if (Vector2.Distance(_currentPosition, _dragPosition) >= _snappingDistance)
		{
			Debug.DrawLine(_currentPosition, _dragPosition, Color.red, 2);

			RaycastHit2D[] hit = Physics2D.LinecastAll(_currentPosition, _dragPosition);
			if (hit.Length > 1)
				return;

			transform.position = NewPosition();
			AddCollider(NewPosition(), _currentPosition);
			_currentPosition = transform.position;
		}
	}

	Vector2 NewPosition()
	{
		Vector2 position = Vector2.MoveTowards(_currentPosition, _dragPosition, _snappingDistance);
		return position;
	}

	/// <summary>
	/// Adds a collider and places it in between the Vectors
	/// </summary>
	/// <param name="start"></param>
	/// <param name="end"></param>
	void AddCollider(Vector2 start, Vector2 end)
	{
		Vector2 spawnPosition = (start + end) / 2;
		Instantiate(_treeCollider, spawnPosition, Quaternion.identity);
	}
}
