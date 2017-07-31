using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingScript : MonoBehaviour {

		void LateUpdate()
	{
		GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100);
	}

}
