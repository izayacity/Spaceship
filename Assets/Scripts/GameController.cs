using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController instance = null;
	public Text scoreText;
	public Text restartText;
	public Text gameoverText;

	private int score;
	private bool gameover;
	private bool restart;

	public GameObject player;
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	void Awake() {
		//Check if there is already an instance of the game manager
		if (instance == null)
			//if not, set it to this.
			instance = this;
		//If instance already exists:
		else if (instance != this)
			//Destroy this, this enforces our singleton pattern so there can only be one instance of the game manager.
			Destroy(gameObject);

		//Set to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		gameover = false;
		restart = false;
		restartText.text = "";
		gameoverText.text = "";
		score = 0;
		UpdateScore();
		StartCoroutine(SpawnWaves());
	}

	void Update() {
		if(restart) {
			if (Input.GetKeyDown(KeyCode.R)) {
				//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				gameover = false;
				restart = false;
				restartText.text = "";
				gameoverText.text = "";
				score = 0;
				UpdateScore();
				Instantiate(player, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
				StartCoroutine(SpawnWaves());
			}
		}
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds(startWait);

		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);

			if(gameover) {
				restartText.text = "Press 'R' for restart";
				restart = true;
				break;		// End the spawn waves coroutine
			}
		}
	}

	public void AddScore(int newScoreValue) {
		score += newScoreValue;
		UpdateScore();
	}

	void UpdateScore() {
		scoreText.text = "Score: " + score;
	}

	public void GameOver() {
		gameoverText.text = "Game Over!";
		gameover = true;
	}
}
