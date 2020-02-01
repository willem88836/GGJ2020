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
		delta = delta.normalized * (distance / MaxDistance);
		clickableThumbStick.position = centre.position + delta * distance;
		previouslyZero = false;
		controller.SendMobileInput(new MobileInput(MobileInputType, delta));
	}
}
