using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //ссылка на колизию персонажа, это будем своего рода "мотор" который перемещает персонажа
    public CharacterController controller;
    //скорость перемещния персонажа
    public float speed = 0f;
    //переменная, хранящая текущую скорость
    Vector3 velocity;
    //ускорение совободного падения (гравитация)
    public float gravity = -9.81f;
    //высота прыжка
    public float JumpHeight = 3f;
    public float velocityGravity = 2.5f;
    //радиус сферы, которую мы будем использовать для проверки
    public float groundDistance = 0.1f;
    //Общедоступная маска земли
    public LayerMask groundMask;
    //будет присваиваться = стоим мы на земле или нет
    //public bool isGrounded;
    float x;
    float z;
    const float errorInMovement = 0.01f;


    void Start() 
    {
        controller = GetComponent<CharacterController>();
    }

   
    void Update()
    {
         
        if (controller.isGrounded)
        {
            //Присваеваем переменные значения WASD с клавиатуры
            //D = -1, A = 1
            x = Input.GetAxis("Horizontal");
            //W = 1, S = -1
            z = Input.GetAxis("Vertical");
            if(Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
            }

            if(PlayerShift(z, x))
            {
                Debug.Log("Бегу!");
                speed = 12f;
            }
            else if(PlayerWalk(z, x))
            {
                Debug.Log("Иду!");
                speed = 5f;
            }
            else if(PlayerStand(z, x))
            {
                Debug.Log("Стою!");
                //что-то делает
            }
        }
        //transform.right * x - будем идти влево и право в относительных(не глобальных) координат
        //transform.forward * z - будем перемещаться вперёд и назад в то направление в которое смотрит игрок
        Vector3 move = transform.right * x + transform.forward * z;
        //перемещние персонажа в пространстве (двигаем сам объект)
        controller.Move(move * speed * Time.deltaTime);


        //реализация падения персонажа
        velocity.y += gravity * Time.deltaTime * velocityGravity;
        //Присваиваем гравитацию нашему перснажу (так как усорение это секунды в квадрате, то ещё раз умножаем на Time.deltaTime)
        controller.Move(velocity * Time.deltaTime);
    }

    bool PlayerWalk(float _z, float _x)
    {
        //Debug.Log($"Есть скорость: {_shift},  Есть перемещение: {_z}");
        if(Mathf.Abs(_z) > errorInMovement || Mathf.Abs(_x) > errorInMovement)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //проверка на бег
    bool PlayerShift(float _z, float _x)
    {
        if((_z > errorInMovement || (Mathf.Abs(_x) > errorInMovement) && _z > 0)
        && Input.GetButton("Shift"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //проверка на то что персонаж стоит
    bool PlayerStand(float _z, float _x)
    {
        if(Mathf.Abs(_z) < errorInMovement && Mathf.Abs(_x) < errorInMovement)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

