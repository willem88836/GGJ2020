using UnityEngine;
using System.Collections.Generic;

public class Lily : Interactable
{
	[SerializeField] List<Sprite> spriteStates;
	private int spriteState = 0;
	

	public override void Dehighlight()
	{
		Debug.Log("Dehighlighting!!");
	}

	public override void Highlight()
	{
		Debug.Log("Highlighting!!");
	}

	public override void Interact(MobileInput input)
	{
		Debug.Log("Interacting!!");
		if (spriteState <=2) {
			GetComponent<SpriteRenderer>().sprite = spriteStates[spriteState];
			spriteState++;
		}
	}
}
