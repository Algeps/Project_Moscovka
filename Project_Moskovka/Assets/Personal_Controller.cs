using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personal_Controller : MonoBehaviour
{
    float _vertical, _horizontal, _jump;
    public float _speed = 5, _jumpSpeed = 200;
    bool _isGround;

    // Start ���������� ����� ������ ����������� �����
    void Start()
    {
        
    }

    void OnCollisionStay(Collision other)//���� ��������� �� �����
    {
        if(other.gameObject.tag == "_ground")//����������, ����� �� �� �����
        {
            _isGround = true;//��, ����� �� �����
        }
    }

    void OnCollisionExit(Collision other)//���� ���������� � �������
    {
        if (other.gameObject.tag == "_ground")
        {
            _isGround = false;
        }
    }


    // Update ���������� ���� ��� �� ����
    void Update()
    {
        if (_isGround)
        {
            /*�������� ������ �� ������ Input.GetAxis
            Input.GetAxis - ���������� �������� (1 ��� -1 ��� ������� �������)
            Time.deltaTime - ����� �������
            _speed - ��������
            */
            _vertical = Input.GetAxis("Vertical") * Time.deltaTime * _speed;
            _horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * _speed;
            _jump = Input.GetAxis("Jump") * Time.deltaTime * _jumpSpeed;

            //������������ ������� ����� ����� transform, ����������� ����� ������ �� ����� �����
            //Vector 3 - ������ 3 ����������
            transform.Translate(new Vector3(_horizontal, 0, _vertical));

            /*GetComponent - �������� ���������, <Rigidbody>() - �������� ���������� ���������
            AddForce - ���������� ����, transform.up - ������������ �����, �������� �� ������ ������
            � �� �������(������ ��� ������� ��� �� ����)
            */
            GetComponent<Rigidbody>().AddForce(transform.up * _jump, ForceMode.Impulse);
        }
        transform.Translate(new Vector3(_horizontal, 0, _vertical));
    }
}
