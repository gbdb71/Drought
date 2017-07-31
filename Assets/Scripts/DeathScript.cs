using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour {
	public int health;

	public ParticleSystem partSystem;

	private bool alive = true;

	public bool isAlive(){
		return alive;
	}


	public void Dmg () {

		partSystem.Play();

		health -=1;

		if(health <= 0){
			Kill();
		}
	}

	void Kill(){
		Invoke("Destroy", 0.5f);
		alive = false;

		partSystem.Play();
	}

	void Destroy(){

		partSystem.Stop();
		Destroy(this.gameObject);
		Destroy(this);
	}

}
