using UnityEngine;
using System.Collections.Generic;

public class Lily : Interactable
{
	[SerializeField] List<Sprite> spriteStates;
	[SerializeField] private Collider2D platform;
	private int spriteState = 0;


	public override void Dehighlight()
	{
		Debug.Log("Dehighlighting!!");
		Renderer.material.shader = DefaultShader;
	}

	public override void Highlight()
	{
		Debug.Log("Highlighting!!");
		Renderer.material.shader = HightlightShader;
	}

	public override void Interact(MobileInput input)
	{
		isPressedThisFrame = true;
		Debug.Log("Interacting!!");
		if (!isPressed)
		{
			isPressed = true;
			if (spriteState <= 2)
			{
				GetComponent<SpriteRenderer>().sprite = spriteStates[spriteState];
				spriteState++;
				if (spriteState == 2)
				{
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
