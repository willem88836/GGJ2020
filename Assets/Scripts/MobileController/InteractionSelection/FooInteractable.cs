using UnityEngine;

public class FooInteractable : Interactable
{
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
	}
}
