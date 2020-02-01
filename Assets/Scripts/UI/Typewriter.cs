using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typewriter : MonoBehaviour
{
	[SerializeField] float _writeSpeed;
	[TextArea(15, 30)]
	[SerializeField] string _story;
	[SerializeField] Text _text;

    void Start()
    {
		StartCoroutine(AddCharacter());
	}

	IEnumerator AddCharacter()
	{
		foreach (char c in _story)
		{
			_text.text += c;

			yield return new WaitForSeconds(1 / _writeSpeed);
		}
	}
}
