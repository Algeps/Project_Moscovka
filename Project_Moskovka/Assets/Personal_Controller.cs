using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personal_Controller : MonoBehaviour
{
    public GameObject _camera;

    Quaternion StartingRotation;
 
    float _vertical, _horizontal, _jump, _rotationVertical, _rotationHorizontal;
    public float _speed = 5, _jumpSpeed = 100, _cameraSensitivity = 4;
    bool _isGround;

    // Start ���������� ����� ������ ����������� �����
    void Start()
    {
        //
        StartingRotation = transform.rotation;
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
        _rotationHorizontal += Input.GetAxis("Mouse X") * _cameraSensitivity;
        _rotationVertical += Input.GetAxis("Mouse Y") * _cameraSensitivity;

        //������������ ������ �������� -60 � 60
        _rotationVertical = Mathf.Clamp(_rotationVertical, -60, 60);

        //��������� �����������
        Quaternion _rotationY = Quaternion.AngleAxis(_rotationHorizontal, Vector3.up);
        Quaternion _rotationX = Quaternion.AngleAxis(- _rotationVertical, Vector3.right);

        //
        _camera.transform.rotation = StartingRotation * _rotationX * transform.rotation;
        //�������� ��� ����� ������
        transform.rotation = StartingRotation * _rotationY;

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
