using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera-Control/MouseLook")]
public class MouseLook : MonoBehaviour
{
    //���������� ������ ��� ���������� ���� ��������
    public enum RotationAxes { MouseXandY = 0, MouseX = 1, MouseY =2 };
    public RotationAxes axes = RotationAxes.MouseXandY;
    //��������� ���������������� ����
    public float sensivityX = 2;
    public float sensivityY = 2;
    //���������� ������������� ���� �������� X
    public float minimumX = -360;
    public float maxmimumX = 360;
    //���������� ������������� ���� �������� Y
    public float minimumY = -360;
    public float maxmimumY = 360;
    //���������� ������������ ������� ������� ��������
    float rotationX = 0;
    float rotationY = 0;
    //���������� ���������� ��� �������� "Quaternion"
    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        originalRotation = transform.localRotation;
    }

    public static float ClampAngle (float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(axes == RotationAxes.MouseXandY)
        {
            rotationX += Input.GetAxis("Mouse X") * sensivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensivityY;

            rotationX = ClampAngle(rotationX, minimumX, maxmimumX);
            rotationY = ClampAngle(rotationY, minimumY, maxmimumY);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensivityX;
            rotationX = ClampAngle(rotationX, minimumX, maxmimumX);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else if (axes == RotationAxes.MouseY)
        {
            rotationY += Input.GetAxis("Mouse Y") * sensivityY;
            rotationY = ClampAngle(rotationY, minimumY, maxmimumY);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
        }

    }
}
