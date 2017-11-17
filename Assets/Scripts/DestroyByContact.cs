using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject exploision;
	public GameObject playerExploision;
	public int scoreValue;

	private void OnTriggerEnter(Collider other) {

		if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) { 
			return;
		}
				
		if(exploision != null) {
			Instantiate(exploision, transform.position, transform.rotation);
		}

		if (other.CompareTag("Player")) {
			Instantiate(playerExploision, other.transform.position, other.transform.rotation);
			GameController.instance.GameOver();
		}

		GameController.instance.AddScore(scoreValue);
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
