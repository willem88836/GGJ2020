using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Platformer.Mechanics;

public class Memory : MonoBehaviour
{
	[SerializeField] Transform _player;
	[SerializeField] float waitTime = 0.7f;

	[SerializeField] AnimationCurve _curve;
	[SerializeField] float _speed;
	float T;

	Vector3 _centralPosition;

	void Start()
	{
		_centralPosition = transform.position;	
	}

	void Update()
	{
		T += Time.deltaTime * _speed;
		if (T > 1)
			T = 0;

		transform.position = (Vector2)_centralPosition + Vector2.up * _curve.Evaluate(T); 
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform != _player)
			return;

		StartCoroutine(GoToMemory());
		collision.GetComponent<PlayerController>().controlEnabled = false;
	}

	IEnumerator GoToMemory()
	{
		Time.timeScale = 0.2f;

		yield return new WaitForSeconds(waitTime);
		Time.timeScale = 1;
		SceneManager.LoadScene(1);
	}
}
