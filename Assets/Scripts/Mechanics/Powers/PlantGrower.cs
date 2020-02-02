using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrower : Interactable
{
	[SerializeField] float _snappingDistance = 2;
	[SerializeField] GameObject _treeCollider;
	[SerializeField] private int _hitTreshold;
	[SerializeField] private Transform chunkContainer;

	BoxCollider2D _boxCollider;

	Vector2 _currentPosition;
	Vector2 _drawerCurrent;
	int insertIndex = 0;

	public Action<float> OnPowerUsage;

	void Start()
	{
		_currentPosition = transform.position;
		_boxCollider = GetComponent<BoxCollider2D>();
	}

	public override void Interact(MobileInput mobileInput)
	{
		if (mobileInput.InputType == InputTypes.PlantDraw)
		{
			ProcessPosition(mobileInput.Value);
		}
	}

	void OnMouseDrag()
	{
		ProcessPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}

	private void ProcessPosition(Vector2 dragPosition)
	{
		if (OnPowerUsage == null)
			return;

		if (insertIndex == -1)
		{
			_drawerCurrent = dragPosition;
			insertIndex = 0;
		}


		// mozart, "Write It", Muse, Doom, The Swanlake 
		if (Vector2.Distance(_drawerCurrent, dragPosition) >= _snappingDistance)
		{
			Vector2 worldDragPosition = _currentPosition + dragPosition - _drawerCurrent;

			RaycastHit2D[] hit = Physics2D.LinecastAll(_currentPosition, worldDragPosition);
			if (hit.Length > _hitTreshold)
			{
				Debug.Log("plant is blocked"); 
				return;
			}

			transform.position = NewPosition(worldDragPosition);
			AddCollider(NewPosition(worldDragPosition), _currentPosition);
			_currentPosition = transform.position;
			_drawerCurrent = dragPosition;

			OnPowerUsage.Invoke(1);

			insertIndex++;
		}
	}

	Vector2 NewPosition(Vector2 dragPosition)
	{
		Vector2 position = Vector2.MoveTowards(_currentPosition, dragPosition, _snappingDistance);
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
		Instantiate(_treeCollider, spawnPosition, Quaternion.identity, chunkContainer);
	}

	public override void Dehighlight()
	{
		Renderer.sprite = DefaultSprite[0];
	}

	public override void Highlight()
	{
		Renderer.sprite = HightlightSprite[0];
	}
}
