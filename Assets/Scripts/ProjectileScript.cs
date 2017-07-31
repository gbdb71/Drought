using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("Kill", 4f);
	}
	
	void Kill() {
		Destroy(this.gameObject);
		Destroy(this);
	}

	void Update(){

		transform.Rotate(new Vector3(0,0,5));

	}

}
