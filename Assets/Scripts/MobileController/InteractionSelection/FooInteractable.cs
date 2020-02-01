using UnityEngine;

public class FooInteractable : Interactable
{
	[SerializeField] GameObject highlight;

	public override void Dehighlight()
	{
		Debug.Log("Dehighlighting!!");
		highlight.SetActive(false);
	}

	public override void Highlight()
	{
		Debug.Log("Highlighting!!");
		highlight.SetActive(true);
	}

	public override void Interact(MobileInput input)
	{
		Debug.Log("Interacting!!");
	}
}
