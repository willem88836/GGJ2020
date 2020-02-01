using Framework.ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.UI;

public class SharedStringSetter : MonoBehaviour
{
	public SharedString Value;
	public InputField inputField;

	private void Start()
	{
		inputField.SetTextWithoutNotify(Value.Value);
	}

	public void SetString()
	{
		Value.Value = inputField.text;
	}
}
