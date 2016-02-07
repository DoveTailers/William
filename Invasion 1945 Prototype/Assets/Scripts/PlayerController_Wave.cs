using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController_Wave : MonoBehaviour {
	public float speed;
	Rigidbody2D rb;
	public Boundary boundary;
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	
	private float nextFire;
	
	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}

	void FixedUpdate()
	{
		rb.velocity = transform.up.normalized*speed;
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical); 
		rb.velocity = movement * speed;

		rb.position = new Vector2 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),  
			Mathf.Clamp (rb.position.y, boundary.yMin, boundary.yMax)
		);
	}
}
