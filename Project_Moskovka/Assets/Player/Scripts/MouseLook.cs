using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //чувствительность мышы
    public float mouseSensitivityX = 2f;
    public float mouseSensitivityY = 2f;
    //ссылка не на камеру, а на весь объект целиком(игрока)
    //голова персонажа
    [SerializeField] public Transform playerHead;
    [SerializeField] public Transform playerCamera;

    //вращение вверх и вниз
    float yRotation = 0f;
    float yRotationHead = 0f;
    const float limitYRotation = 70f;
    float mouseX;
    float mouseY;


    void Start()
    {
        //инизиализируем здесь, потому-что будем часто вызывать это
        playerHead = playerHead.GetComponent<Transform>();
        // player = player.GetComponent<Transform>();
        playerCamera = playerCamera.GetComponent<Transform>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    //Debug.Log($"По Y: {mouseY}, по X: {mouseX}");
    void Update()
    {
        //получение ввода мышы и умножение полученный величины на чувствительность мыши
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        //вращение вверх и вниз (вычитаем, потому-что сложение это инверсия)
        yRotation -= mouseY;
        //ограничиваем вращение камеры вверх и вниз на 90 градусов вниз и на 90 градусов вверх
        yRotation = Mathf.Clamp(yRotation, -limitYRotation, limitYRotation);
        // Ограничение поднятие и опускание головы персонажа
        yRotationHead = Mathf.Clamp(yRotation, -limitYRotation+15, limitYRotation-60);
        //следом используем квантернионы для применения самого вращения
    }

    void LateUpdate()
    {
        //Применяем вращение, используя Кватернионы (потому-что они отвечают за вращение в Unity)
        //Добавляем yRotation для X, 0f для Y, 0f для Z
        //поворот камеры по Y
        playerCamera.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        //поворот головы по Y
        playerHead.localRotation = Quaternion.Euler(yRotationHead, 0f, 0f);
        transform.Rotate(Vector3.up, mouseX);
    }
}
