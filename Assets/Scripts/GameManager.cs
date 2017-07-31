using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	
    void Update() {
        if (Input.GetKey("escape"))
            Application.Quit();
        
    }


	public void GameOver(){
		Debug.Log("Game Over");
		Invoke("SetIntroScene", 3);
	}

	void SetWinningScene () {

	}

	void SetIntroScene () {
		SceneManager.LoadScene("Intro");
	}
}
