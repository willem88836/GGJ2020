using Framework.ScriptableObjects.Variables;
using UnityEngine;

public class Blackboard : BaseButton
{
	public SharedFloat NodeInterval;
	public RectTransform DrawImage;

	private bool previouslyHeld = false;
	private Vector3 previousLocation;

	protected override void HandleFailedInput()
	{
		DrawImage.gameObject.SetActive(false);
		previouslyHeld = false;
	}

	protected override void HandleInput(Vector3 delta, float distance)
	{
		if (!previouslyHeld)
		{
			DrawImage.gameObject.SetActive(true);
			previouslyHeld = true;

			Vector3 pos = delta + centre.position;
			DrawImage.position = pos;

			pos = Camera.main.ScreenToWorldPoint(pos);
			previousLocation = pos;
		}
		else
		{
			Vector3 pos = delta + centre.position;
			DrawImage.position = pos;

			pos = Camera.main.ScreenToWorldPoint(pos);
			float nodeDistance = (pos - previousLocation).sqrMagnitude;
				
			if (nodeDistance >= NodeInterval.Value)
			{
				controller.SendMobileInput(new MobileInput(InputTypes.PlantDraw, pos));
				previousLocation = pos;
			}
		}
	}

	private void UpdateNodePosition(Vector3 pos)
	{

	}

	protected override bool InRange(Vector3 position)
	{
		return position.x >= centre.position.x - centre.sizeDelta.x / 2f
			&& position.x <= centre.position.x + centre.sizeDelta.x / 2f
			&& position.y >= centre.position.y - centre.sizeDelta.y / 2f
			&& position.y <= centre.position.y + centre.sizeDelta.y / 2f;
	}
}
