using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oasis : MonoBehaviour {

	public ParticleSystem waterEffect;

	private GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		var distance = (transform.position - player.gameObject.transform.position).magnitude;

		if(distance < 2){
			var playerScript = player.GetComponent<PlayerScript>();
			playerScript.RestoreWater();
			if(!waterEffect.isPlaying)
				waterEffect.Play();
		} else {
			if(waterEffect.isPlaying){
				waterEffect.Stop();
			}
		}


	}

}
