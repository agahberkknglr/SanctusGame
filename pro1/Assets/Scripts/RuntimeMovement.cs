using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]


public class RuntimeMovement : MonoBehaviour
{
    private Movement _input;
    private CharacterController _controller;
    [SerializeField] private float fraction;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<Movement>();

    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _controller.Move(new Vector3((_input.moveVal.x * _input.speed)/fraction,0f, (_input.moveVal.y * _input.speed)/fraction));
    }


}






 



