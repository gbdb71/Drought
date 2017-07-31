using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

	public Text waterText;
	public RawImage waterBar;
	private float waterBarWidth;


	public Camera mainCamera;
	public float moveSpeed;
	public float dashSpeed;

	private float health;
	private float idleCost;
	private float moveCost;
	private float dashCost;
	private float attackingCost;
	private Rigidbody2D rBody2d;


	private Animator animator;

	private float startingHealth = 100;

	private bool movingEnabled;

	private bool isAlive = true;

	void Start () {
		rBody2d = GetComponent<Rigidbody2D>();
		health = 65;

		idleCost = 0.01f;
		moveCost = 0.02f;
		dashCost = 2f;
		attackingCost = 1f;

		animator = GetComponent<Animator>();

		waterBarWidth = waterBar.rectTransform.rect.width;

		movingEnabled = true;
	}

	void EnableMoving () {
		movingEnabled = true;
	}

	
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Enemy"){

			var direction = (transform.position -  other.gameObject.transform.position).normalized;

			rBody2d.AddForce(direction * 500);
			health -= 5;
			movingEnabled = false;
			Invoke("EnableMoving", 0.5f);
		}	

		if(other.gameObject.tag == "Projectile"){
			health -= 5;
			
			var direction = (transform.position -  other.gameObject.transform.position).normalized;
			rBody2d.AddForce(direction * 500);
			Destroy(other.gameObject);
			movingEnabled = false;
			Invoke("EnableMoving", 0.5f);
		}
	}


	public void RestoreWater(){
		health = Mathf.Min(startingHealth, health + 0.5f);
	}
	
	void Update () {

		waterBar.rectTransform.sizeDelta = new Vector2(health * (waterBarWidth/startingHealth),waterBar.rectTransform.sizeDelta.y);
		waterText.text = health.ToString();

		moveSpeed = health * 5;

		if(health <= 0 && isAlive){
			isAlive = false;
			var gameManager = GameObject.FindGameObjectWithTag("GameManager");
			var gameManagerScript = gameManager.GetComponent<GameManager>();
			GetComponent<BoxCollider2D>().enabled = false;
			animator.SetTrigger("Death");
			gameManagerScript.GameOver();
		}

		if(!isAlive){
			return;
		}

		float cost = idleCost;

	/* 	// * Dash - for another time
		if(Input.GetKeyDown(KeyCode.W) && movingEnabled){

			RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			if(hit.collider != null){

				Invoke("EnableMoving", 0.5f);
				movingEnabled = false;

				Vector2 direction = (hit.point - (Vector2)transform.position).normalized;
				rBody2d.AddForce(direction * dashSpeed);

				cost += dashCost;

			}
		} */

		// * Move
		if(Input.GetMouseButton(1)){

			RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			if(hit.collider != null && movingEnabled){

				float distance = (hit.point - (Vector2)transform.position).magnitude;
				//var actualSpeed = distance > 0.5 ? moveSpeed:  distance * 10;

				var actualSpeed = Mathf.Min(moveSpeed, distance*500);

			 	Vector2 direction = (hit.point - (Vector2)transform.position).normalized;
				rBody2d.velocity = Vector2.ClampMagnitude(actualSpeed * direction * Time.deltaTime, moveSpeed * Time.deltaTime);
				

				cost += moveCost;
			}
		}

		// * Attacking
		if(Input.GetMouseButtonDown(0)){

			var enemies = GameObject.FindGameObjectsWithTag("Enemy");

			foreach(GameObject enemy in enemies){
				
				float distance = ((Vector2)transform.position - (Vector2)enemy.gameObject.transform.position).magnitude;

				animator.SetTrigger("Attack");

				if(distance < 1.5){
					var comp = enemy.GetComponent<DeathScript>();

					comp.Dmg(); 
				}


			}

			cost += attackingCost;
		}

		health -= cost;

		animator.SetBool("RunningSouth", false);
		animator.SetBool("RunningNorth", false);

		if(rBody2d.velocity.x > 0.1  || rBody2d.velocity.x < -0.1 || rBody2d.velocity.y > 0.1 || rBody2d.velocity.y < -0.1){

			if(rBody2d.velocity.y < 0){
				animator.SetBool("RunningSouth", true);

			} else {
				animator.SetBool("RunningNorth", true);
			}
		}

	}

}
