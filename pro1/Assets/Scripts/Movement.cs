using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody capsule;
    public Vector2 moveVal;
    public float speed=10;
    
    private void Awake()
    {
        capsule = GetComponent<Rigidbody>();

    }

    public void Moving(InputAction.CallbackContext value)
    {

       

      
        if (value.performed)
        {
            

            moveVal = value.ReadValue<Vector2>();
            //capsule.AddForce(new Vector3(moveVal.x * speed,0f,moveVal.y*speed),ForceMode.Impulse);*/
        }

        if (value.canceled)
        {
            moveVal = value.ReadValue<Vector2>();
        }


    }
}



