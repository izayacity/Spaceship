using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject exploision;
	public GameObject playerExploision;
	public int scoreValue;

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary")
			return;

		Instantiate(exploision, transform.position, transform.rotation);
		if (other.tag == "Player") {
			Instantiate(playerExploision, other.transform.position, other.transform.rotation);
		}
		GameController.instance.AddScore(scoreValue);
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
