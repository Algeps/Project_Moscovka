using Unity.Netcode;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
[RequireComponent(typeof(Transform))]
public class PlayerMovement : NetworkBehaviour
{
    /*ПЕРЕДВИЖЕНИЕ*/
    /// <summary>
    /// Вектор перемещения
    /// </summary>
    [SerializeField] private Vector3 movePlayer = Vector3.zero;
    /// <summary>
    /// Вектор вращения
    /// </summary>
    [SerializeField] private Vector3 rotatePlayer = Vector3.zero;
    /// <summary>
    /// Основная скорость перемещения персонажа
    /// </summary>
    [SerializeField] public float playerSpeed = 0f;
    // ускорение совободного падения (гравитация)
    [SerializeField] Vector3 velocity = Vector3.zero;
    // старое введённое значения перемещения
    private Vector3 oldInputMoving = Vector3.zero;
    // Старое введёное значение вращения
    private Vector3 oldInputRotation = Vector3.zero;

    // Движение игрока
    [SerializeField] private NetworkVariable<Vector3> networkPositionDirection = new NetworkVariable<Vector3>();
    // Вращение игрока
    [SerializeField] private NetworkVariable<Vector3> networkRotationDirection = new NetworkVariable<Vector3>();


    // Ускорение свободного падения
    [SerializeField] private float gravity = -9.81f;
    // высота прыжка
    [SerializeField] private float JumpHeight = 3.5f;
    [SerializeField] private float velocityGravity = 2.5f;
    private CharacterController characterController;
    // скорость перемещния персонажа
    
    /*ПЕРЕМЕЩЕНИЕ*/
    // Разброс респауна
    [SerializeField] private Vector2 defaultInitialPositionOnPlane = new Vector2(10, 10);
    // Ввод по X
    private float x;
    // Ввод по Z
    private float z;
    // Погрешность результатов при сравнении
    const float errorInMovement = 0.01f;
    // Ввод мыши по Х
    private float mouseX;
    //чувствительность мышы
    public float mouseSensitivityX = 2f;
    

    /*АНИМАЦИЯ*/
    // анимация персонажа
    private Animator animator;
    // сетевая переменная анимации
    [SerializeField] private NetworkVariable<int> networkPlayerState = new NetworkVariable<int>();
    // номер анимации
    private int oldPlayerState = 0;
    [SerializeField] private int _state = 0;


    private void Awake() 
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (IsClient && IsOwner)
        {
            // рандомный респаун из диапазона вектора "defaultInitialPositionOnPlane"
            transform.position = new Vector3(Random.Range(defaultInitialPositionOnPlane.x, defaultInitialPositionOnPlane.y), 25,
                   Random.Range(defaultInitialPositionOnPlane.x, defaultInitialPositionOnPlane.y));
            CameraFP.FollowPlayer(transform.Find("PlayerCameraRoot"));
        }
    }

    private void Update()
    {
        if (IsClient && IsOwner)
        {
            ClientInput();
        }

        ClientMoveAndRotate();
        ClientVisuals();
    }

    private void ClientMoveAndRotate()
    {
        if (networkPositionDirection.Value != Vector3.zero)
        {
            //перемещние персонажа в пространстве (двигаем сам объект)
            characterController.SimpleMove(networkPositionDirection.Value);
        }
        if (networkRotationDirection.Value != Vector3.zero)
        {
            //перемещние персонажа в пространстве (двигаем сам объект)
            transform.Rotate(networkRotationDirection.Value);
        }
    }

    private void ClientVisuals()
    {
        if (oldPlayerState != networkPlayerState.Value)
        {
            oldPlayerState = networkPlayerState.Value;
            animator.SetInteger("state", networkPlayerState.Value);
        }
    }

    private void ClientInput()
    {
        if (characterController.isGrounded)
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
            
            // Смена анимации и смена скорости передвижения
            if(PlayerShift(z, x))
            {
                playerSpeed = 15f;
                _state = 2;
                UpdatePlayerStateServerRpc(_state);
                
            }
            else if(PlayerWalkForward(z, x))
            {
                playerSpeed = 5f;
                _state = 1;
                UpdatePlayerStateServerRpc(_state);
            }
            else if(PlayerWalkReverse(z, x))
            {
                playerSpeed = 5f;
                _state = 1;
                UpdatePlayerStateServerRpc(_state);
            }
            else if(PlayerStand(z, x))
            {
                _state = 0;
                UpdatePlayerStateServerRpc(_state);
            }
            
        }
        else
        {
            /* РЕАЛИЗАЦИЯ ГРАВИТАЦИИ ПЕРСОНАЖА */
            //Присваиваем гравитацию нашему перснажу (так как усорение это секунды в квадрате, то ещё раз умножаем на Time.deltaTime)
            //characterController.Move(velocity * Time.deltaTime);
            velocity.y += gravity * velocityGravity * Time.deltaTime * Time.deltaTime;
        }
        // Получение ввода мышы по Х
        mouseX = Input.GetAxis("Mouse X");
        
        // *НЕОБХОДИМО РАЗОБРАТЬСЯ С ГРАВИТАЦИЕЙ
       

        /* РЕАЛИЗАЦИЯ ПЕРЕДВИЖЕНИЯ ПЕРСОНАЖА ПО ПОВЕРХНОСТИ ЗЕМЛИ*/
        //transform.right * x - будем идти влево и право в относительных(не глобальных) координат
        //transform.forward * z - будем перемещаться вперёд и назад в то направление в которое смотрит игрок
        movePlayer = (transform.right * x + transform.forward * z + transform.up * velocity.y);
        rotatePlayer = new Vector3(0, mouseX, 0);
        //Debug.Log($"Вектор перемещения без изменений: {movePlayer}");

        if (oldInputMoving != movePlayer||
            oldInputRotation != rotatePlayer)
        {
            oldInputMoving = movePlayer;
            // вектор передвижения умножаем на скорость персонажа
            movePlayer.x *= playerSpeed;
            movePlayer.z *= playerSpeed;
            rotatePlayer *= mouseSensitivityX;
            UpdateClientPositionAndRotationServerRpc(movePlayer, rotatePlayer);
        }
    }

    [ServerRpc]
    public void UpdateClientPositionAndRotationServerRpc(Vector3 newPosition, Vector3 newRotation)
    {
        networkPositionDirection.Value = newPosition;
        networkRotationDirection.Value = newRotation;
    }

    [ServerRpc]
    public void UpdatePlayerStateServerRpc(int state)
    {
        networkPlayerState.Value = state;
    }

    /// <summary>
    /// Проверка на состояние "Ходьба вперёд"
    /// </summary>
    public bool PlayerWalkForward(float _z, float _x)
    {
        return ((Mathf.RoundToInt(_z) == 1 || Mathf.Abs(Mathf.RoundToInt(_x)) == 1) ?  true : false);
    }

    /// <summary>
    /// Проверка на состояние "Ходьба назад"
    /// </summary>
    bool PlayerWalkReverse(float _z, float _x)
    {
        return ((Mathf.RoundToInt(_z) == -1 || Mathf.Abs(Mathf.RoundToInt(_x)) == 1) ?  true : false);
    }

    /// <summary>
    /// Проверка на состояние "Бег"
    /// </summary>
    public bool PlayerShift(float _z, float _x)
    {
        return (((Mathf.RoundToInt(_z) == 1 || Mathf.Abs(Mathf.RoundToInt(_x)) == 1)
        && Input.GetButton("Shift")) ?  true : false);
    }

    /// <summary>
    /// Проверка на состояние "Стоит"
    /// </summary>
    public bool PlayerStand(float _z, float _x)
    {
        return (Mathf.Abs(Mathf.RoundToInt(_z)) == 0 && Mathf.Abs(Mathf.RoundToInt(_x)) == 0 ? true : false);
    }
}

