using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootingEnemy : MonoBehaviour {

	public float reloadTimeInSeconds; 

	public float aggroDistance = 6;

	public Rigidbody2D projectile;

	public float projectileSpeed;

	private GameObject player;
	private float ticker; 

	private Vector2 startScale;

	private DeathScript deathScript;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");	
		startScale = transform.localScale;
		deathScript = GetComponent<DeathScript>();
	}
	
	// Update is called once per frame
	void Update () {


		if(!deathScript.isAlive()){
			var spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
			gameObject.GetComponent<BoxCollider2D>().enabled = false;

			foreach(SpriteRenderer sr in spriteRenderers){
				sr.enabled = false;
			}
			
			return;
		}


		var distanceToPlayer = (transform.position - player.transform.position).magnitude;

		if(player.transform.position.x > transform.position.x){
			transform.localScale = startScale;
		} else {
			transform.localScale = new Vector2(-startScale.x, startScale.y);
		}

		//Debug.Log(distanceToPlayer);

		if(distanceToPlayer < aggroDistance && ticker < 0){
			

			var direction = (player.transform.position - transform.position).normalized;

			Instantiate<Rigidbody2D>(projectile, transform.position, transform.rotation).AddForce(direction * projectileSpeed);

			ticker = reloadTimeInSeconds;
		};

		ticker -= Time.deltaTime;
	}

}
