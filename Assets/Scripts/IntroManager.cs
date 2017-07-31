using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroManager : MonoBehaviour {



    void Update() {
        if (Input.GetKey("escape"))
            Application.Quit();
        
    }


	public void Play(){
		SceneManager.LoadScene("Main");
	}

	public void About(){

	}
}
