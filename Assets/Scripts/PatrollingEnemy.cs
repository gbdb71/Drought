using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour {

	public float patrollingSpeed;

	public float chasingSpeed;

	public float aggroDistance = 6;

	private GameObject player;


	private Vector2 targetLocation;

	private Vector2 origoPosition;

	private bool chasedLastFrame = false; 

	private bool movingTowardsTarget = true;


	private bool movingEnabled = true;

	private Rigidbody2D rBody2d;


	private DeathScript deathScript;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");	
		origoPosition = transform.position;
		rBody2d = GetComponent<Rigidbody2D>();
		deathScript = GetComponent<DeathScript>();
		targetLocation = RandomVector2D() + (Vector2)transform.position;
	}


	Vector2 RandomVector2D () {

		Vector2 rnd = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f,1f));
		return rnd.normalized * Random.Range(3,5);

	}

	void EnableMoving () {
		movingEnabled = true;
	}


	void OnCollisionStay2D(Collision2D other)	{
		if(other.gameObject.tag != "Player"){
			movingTowardsTarget = true;
			targetLocation = RandomVector2D() + (Vector2)transform.position;

		}
	}


	
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

		// * Chase
		if(distanceToPlayer < aggroDistance){		

			var direction = (player.transform.position - transform.position).normalized;

			rBody2d.velocity = chasingSpeed * direction * Time.deltaTime;

			chasedLastFrame = true;

		} else if(movingEnabled) { // * Patrol

			if(chasedLastFrame){
				origoPosition = transform.position;
				targetLocation = RandomVector2D() + (Vector2)transform.position;
				movingTowardsTarget = true;

				movingEnabled = false;
				Invoke("EnableMoving", 1f);
			}

			if(movingTowardsTarget){
				
				var distanceToTargetLocation = ((Vector2)transform.position - targetLocation).magnitude;
				var direction = (targetLocation - (Vector2)transform.position).normalized;

				if(distanceToTargetLocation < 0.3f){
					movingTowardsTarget = false;
					movingEnabled = false;
					Invoke("EnableMoving", 1f); 
				} else {
					rBody2d.velocity = patrollingSpeed * direction * Time.deltaTime;
				} 


			} else {

				var distanceToOrigo = ((Vector2)transform.position - origoPosition).magnitude;
				var direction = (origoPosition - (Vector2)transform.position).normalized;

				if(distanceToOrigo < 0.3f){
					targetLocation = RandomVector2D() + (Vector2)transform.position;
					movingTowardsTarget = true;
					movingEnabled = false;
					Invoke("EnableMoving", 1f);
				} else {
					rBody2d.velocity = patrollingSpeed * direction * Time.deltaTime;
				}


			}

			chasedLastFrame = false;

		}

	}
}
