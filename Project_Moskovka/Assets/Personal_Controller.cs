using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personal_Controller : MonoBehaviour
{
    float _vertical, _horizontal, _jump;
    public float _speed = 5, _jumpSpeed = 200;
    bool _isGround;

    // Start вызывается перед первым обновлением кадра
    void Start()
    {
        
    }

    void OnCollisionStay(Collision other)//если находится на земле
    {
        if(other.gameObject.tag == "_ground")//сравниваем, стоит ли на земле
        {
            _isGround = true;//да, стоит на земле
        }
    }

    void OnCollisionExit(Collision other)//если находиться в воздухе
    {
        if (other.gameObject.tag == "_ground")
        {
            _isGround = false;
        }
    }


    // Update вызывается один раз за кадр
    void Update()
    {
        if (_isGround)
        {
            /*получаем данные из класса Input.GetAxis
            Input.GetAxis - возвращает значения (1 или -1 при нажатии клавиши)
            Time.deltaTime - время нажатия
            _speed - усиление
            */
            _vertical = Input.GetAxis("Vertical") * Time.deltaTime * _speed;
            _horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * _speed;
            _jump = Input.GetAxis("Jump") * Time.deltaTime * _jumpSpeed;

            //передвижение объекта через класс transform, присваеваем новый вектор на новом кадре
            //Vector 3 - значит 3 координаты
            transform.Translate(new Vector3(_horizontal, 0, _vertical));

            /*GetComponent - получили компонент, <Rigidbody>() - получили физический компонент
            AddForce - добавление силы, transform.up - направленной вверх, умножили на высоту прыжка
            и на импульс(прыжок это импульс сам по себе)
            */
            GetComponent<Rigidbody>().AddForce(transform.up * _jump, ForceMode.Impulse);
        }
        transform.Translate(new Vector3(_horizontal, 0, _vertical));
    }
}
