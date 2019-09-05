using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 5f;
	public float jumpSpeed = 5f;
	private float movement = 0f;
	private Rigidbody2D rigidBody2D;
	public Transform groundCheckPoint;
	public float groundCheckRadius;
    public LayerMask groundLayer;
	private bool isOnGround;
	public Vector3 respawnPoint;
	
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
		respawnPoint = transform.position;
    }

	// Update is called once per frame
	void Update()
	{
		isOnGround = Physics2D.OverlapCircle(groundCheckPoint.position,groundCheckRadius,groundLayer);
		movement = Input.GetAxis("Horizontal");
		if (movement != 0f)
		{
			rigidBody2D.velocity = new Vector2(movement * speed, rigidBody2D.velocity.y);
		}
		else
		{
			rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
		}

		if (Input.GetButtonDown("Jump") && isOnGround)
        {
			rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpSpeed);
		}
    }

    void OnTriggerEnter2D(Collider2D other)
	{
        if ("FallDetector" == other.tag)
		{
			Debug.Log("Fall Detected");
			gameObject.SetActive(false);
			gameObject.transform.position = respawnPoint;
			gameObject.SetActive(true);
		}
	}
}
