using UnityEngine;

public abstract class BaseButton : MonoBehaviour
{
	public MobileController controller;

	public RectTransform centre;
	public float MaxDistance;

	public string MobileInputType;


	protected void Update()
	{
		foreach (Touch touch in Input.touches)
		{
			Vector3 delta = (Vector3)touch.position - centre.position;
			float distance = Mathf.Abs(delta.magnitude);

			if (distance <= MaxDistance)
			{
				HandleInput(delta, distance);
				break;
			}
		}

		HandleFailedInput();
	}

	protected abstract void HandleInput(Vector3 delta, float distance);
	protected abstract void HandleFailedInput();
}