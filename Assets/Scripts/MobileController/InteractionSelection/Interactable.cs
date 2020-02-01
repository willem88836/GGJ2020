using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	public string PowerType;


	public abstract void Dehighlight();
	public abstract void Highlight();

	public abstract void Interact(MobileInput input);
}
