using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float _speed = 5, _jumpSpeed = 8, _gravity = 20, _speedShift = 10;
    bool isGround;

    private Vector3 _moveDir = Vector3.zero;
    private CharacterController controller;

    
    void Start()
    {
        
        controller = GetComponent<CharacterController>();
    }

    
    void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            //����������� _moveDir ������������ � �������������� �������� � ����������
            _moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _moveDir = transform.TransformDirection(_moveDir);
            _moveDir *= _speed;

            //ускорение
            if (Input.GetButton("Shift") && controller.isGrounded)
            {
                _speed = 12;
            }
            else
            {
                _speed = 5;
            }
        }
        


        if ( Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            _moveDir.y = _jumpSpeed;
        }

        _moveDir.y -= _gravity * Time.deltaTime;

        controller.Move(_moveDir * Time.deltaTime);
    }    
}
