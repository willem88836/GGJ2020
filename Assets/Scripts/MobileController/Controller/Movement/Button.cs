﻿using UnityEngine;

public class Button : BaseButton
{
	bool previouslyClicked = false;

	protected override void HandleFailedInput()
	{
		if (previouslyClicked)
		{
			previouslyClicked = false;
			controller.SendMobileInput(new MobileInput(MobileInputType, Vector3.up));
		}
	}

	protected override void HandleInput(Vector3 delta, float distance)
	{
		if (!previouslyClicked)
		{
			previouslyClicked = true;
			controller.SendMobileInput(new MobileInput(MobileInputType, Vector3.down));
		}
	}
}
