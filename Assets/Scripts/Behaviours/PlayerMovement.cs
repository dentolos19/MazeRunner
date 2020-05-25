﻿using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

	private CharacterController _controller;

	public float speed = 5;

	private void Awake()
	{
		_controller = GetComponent<CharacterController>();
	}
	
	private void Update()
	{
		var x = Input.GetAxis("Horizontal");
		var z = Input.GetAxis("Vertical");
		var body = transform;
		var move = body.right * x + body.forward * z;
		_controller.Move(move * (speed * Time.deltaTime));
	}
	
}