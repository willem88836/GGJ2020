using UnityEngine;

public class ThumbStick : BaseButton
{
	public RectTransform clickableThumbStick;

	bool previouslyZero = true; 


	protected override void HandleFailedInput()
	{
		if (!previouslyZero)
		{
			clickableThumbStick.position = centre.position;
			previouslyZero = true;
			controller.SendMobileInput(new MobileInput(MobileInputType, Vector3.zero));
		}
	}

	protected override void HandleInput(Vector3 delta, float distance)
	{
		delta = delta.normalized * (Mathf.Min(distance, MaxDistance) / MaxDistance);
		clickableThumbStick.position = centre.position + delta.normalized * Mathf.Min(distance, MaxDistance) / 2f;
		previouslyZero = false;
		controller.SendMobileInput(new MobileInput(MobileInputType, delta));
	}
}
