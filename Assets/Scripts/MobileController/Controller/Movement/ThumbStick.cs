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
			controller.SendMobileInput(new MobileInput(MobileInputType, Vector3.zero));
			previouslyZero = true;
		}
	}

	protected override void HandleInput(Vector3 delta, float distance)
	{
		delta.Normalize();
		clickableThumbStick.position = centre.position + delta * distance;
		controller.SendMobileInput(new MobileInput(MobileInputType, delta));
		previouslyZero = false;
	}
}
