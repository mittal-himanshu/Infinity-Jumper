﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	Animator anim;
	public float jumpVel = 7;
	public bool isGrounded = true;
	public float moveSpeed = 200;
	public AudioClip[] jump;
    public static bool Boosted = false;
	// Use this for initialization
	void Awake () {
		anim = transform.GetComponent<Animator> ();
	}

//	void Start()
//	{
//		InvokeRepeating ("UpdateSpeed", 1f, 1f);
//	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        //if(Input.GetKeyDown(KeyCode.N))
        //{
        //    Boost();
        //}
        float move = CrossPlatformInput.GetAxis("Horizontal");
        //#if CROSS_PLATFORM_INPUT
        //    float move = CrossPlatformInput.GetAxis("Horizontal");
        //#else
        //    float move = Input.GetAxis("Horizontal");
        //#endif
        rigidbody2D.velocity = new Vector2 (move * moveSpeed * Time.fixedDeltaTime, rigidbody2D.velocity.y);
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		anim.SetBool ("isGrounded", isGrounded);
	}

	void Jump()
	{
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVel);
		isGrounded = false;
		audio.clip = jump [Random.Range (0, jump.Length)];
		audio.pitch = Random.Range(0.9f,1.1f);
		audio.Play();
	}

    IEnumerator Boost()
	{
        if(rigidbody2D.velocity.y < 0)
		    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 20);
        else
            rigidbody2D.velocity += new Vector2(rigidbody2D.velocity.x, 20);
		isGrounded = false;
        Boosted = true;
        yield return new WaitForSeconds(4);
        Boosted = false;
	}

    IEnumerator SuperBoost()
	{
        if (rigidbody2D.velocity.y < 0)
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 30);
        else
            rigidbody2D.velocity += new Vector2(rigidbody2D.velocity.x, 30);
        isGrounded = false;
        Boosted = true;
        yield return new WaitForSeconds(6);
        Boosted = false;
	}

	IEnumerator InvertControl()
	{
		Invert ();
        yield return new WaitForSeconds(5);
        if (moveSpeed < 0)
            Invert();
	}

	void Invert()
	{
		moveSpeed = -moveSpeed;
	}

	void OnCollisionEnter2D()
	{
		isGrounded = true;
	}

    public void ToggleSound()
    {
        audio.enabled = !audio.enabled;
    }
	
//	void UpdateSpeed()
//	{
//		jumpVel = jumpVel*1.001f;
//	}
}