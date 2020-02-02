using UnityEngine;
using System.Collections.Generic;

public class Lily : Interactable
{
	[SerializeField] private Collider2D platform;
	private int spriteState = 0;


	public override void Dehighlight()
	{
		Renderer.sprite = DefaultSprite[spriteState];
	}

	public override void Highlight()
	{
		Renderer.sprite = HightlightSprite[spriteState];
	}

	public override void Interact(MobileInput input)
	{
		isPressedThisFrame = true;

		if (!isPressed)
		{
			isPressed = true;
			if (spriteState <= 2)
			{
				spriteState++;
				Renderer.sprite = HightlightSprite[spriteState];
				if (spriteState == 2)
				{
					Dehighlight();
					platform.enabled = true;
					this.Active = false;
				}
			}
		}
	}

	bool isPressed = false;
	bool isPressedThisFrame = false;

	void Update()
	{
		if (isPressed && !isPressedThisFrame)
		{
			isPressed = false;
		}

		isPressedThisFrame = false;
	}
}
