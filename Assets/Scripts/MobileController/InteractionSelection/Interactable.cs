using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	public string PowerType;

	public SpriteRenderer Renderer;
	public Shader DefaultShader;
	public Shader HightlightShader;

	public bool Active { protected set; get; } = true;

	public abstract void Dehighlight();
	public abstract void Highlight();

	public abstract void Interact(MobileInput input);
}
