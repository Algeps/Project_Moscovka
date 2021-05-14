using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float _speed = 5, _jumpSpeed = 8, _gravity = 20, _speedShift = 10;
    bool isGround;

    private Vector3 _moveDir = Vector3.zero;
    private CharacterController controller;

    // Start вызывается перед первым обновлением кадра
    void Start()
    {
        //получаем компонент из персонажа
        controller = GetComponent<CharacterController>();
    }

    // FixedUpdate - фиксированнное значение срабатывания функции
    void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            //присваиваем _moveDir вертикальные и горизонтальные значения с клавиатуры
            _moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _moveDir = transform.TransformDirection(_moveDir);
            _moveDir *= _speed;

            /*if (Input.GetKeyDown(KeyCode.Z) && controller.isGrounded)
            {
                _moveDir *= _speedShift;
            }
            else
            {
                
            }*/
        }
        


        if ( Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            _moveDir.y = _jumpSpeed;
        }

        _moveDir.y -= _gravity * Time.deltaTime;

        controller.Move(_moveDir * Time.deltaTime);
    }
}
