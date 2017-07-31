using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour {

	private GameObject player;

	private bool levelIsRunning = true;


	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");		
	}
	
	// Update is called once per frame
	void Update () {
		
		if((transform.position - player.transform.position).magnitude < 1 && levelIsRunning){
			levelIsRunning = false;
			var gameManager = GameObject.FindGameObjectWithTag("GameManager");
			var gameManagerScript = gameManager.GetComponent<GameManager>();
			gameManagerScript.GameOver();

		}
	}
}
