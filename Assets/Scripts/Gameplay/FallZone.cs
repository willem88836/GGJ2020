using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class FallZone : MonoBehaviour
{
	[SerializeField] Transform _spawnPoint;
	[SerializeField] Transform _player;
	[SerializeField] float _respawnTime;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform == _player)
		{
			Death();
		}
	}

	void Death()
	{
		_player.GetComponent<PlayerController>().Death(true);
		StartCoroutine(Respawn());
	}

	IEnumerator Respawn()
	{
		yield return new WaitForSeconds(_respawnTime);

		_player.GetComponent<PlayerController>().Death(false);
		_player.position = _spawnPoint.position;
	}
}
