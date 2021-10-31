using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterControl : MonoBehaviour
{
    public Transform playerCamera;  //Ссылка на камеру, которая фокусирует игрок.
    private ThirdPersonOrbitCamBasic camScript;   //ссылка на основную камеру в преобразовании сцен
    public Animator animator;
    private Vector3 сamForward;     //чтобы камера смотрела прямо всегда

    private int behaviourLocked;    // Ссылка на временное заблокированное поведение, запрещающее переопределение
    private List<GenericBehaviour> overridingBehaviours;  // Список текущих переопределяющих моделей поведения
    private List<GenericBehaviour> behaviours;            // Список, содержащий все разрешенные варианты поведения игрока
    private int currentBehaviour;                         // Ссылка на текущее поведение игрока.
    private Vector3 lastDirection;                        // Последнее направление, в котором двигался игрок
    bool isAnyBehaviourActive = false;
    public float turnSmoothing = 0.06f;                   // Скорость поворота при движении в соответствии с направлением камеры.

    
    //private int _state; //позы персонажа
    private float h;
    private float v;


    
    private Vector3 camForward;   // Текущее направление движения камеры вперед
    private Vector3 move; //передвижение
    Vector3 m_GroundNormal;
    float m_TurnAmount;
	float m_ForwardAmount;
    


    public float _speedPerson = 5;
    private const float speedMove = 5, _speedNormal = 5, _jumpHeight = 10 , _gravity = 15, _speedShift = 10;
    bool isGround;

    private Vector3 _moveDir = Vector3.zero;
    private CharacterController controller;

    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
        //для изменения FOV
        camScript = playerCamera.GetComponent<ThirdPersonOrbitCamBasic> ();
        overridingBehaviours = new List<GenericBehaviour>();
        behaviours = new List<GenericBehaviour> ();
    }

    void Update() {
        h = CrossPlatformInputManager.GetAxis("Horizontal");
        v = CrossPlatformInputManager.GetAxis("Vertical");
    }

    
    void FixedUpdate()
    {
       // Вызвать активное поведение, если никакое другое не переопределяет
		isAnyBehaviourActive = false;
		if (behaviourLocked > 0 || overridingBehaviours.Count == 0)
		{
			foreach (GenericBehaviour behaviour in behaviours)
			{
				if (behaviour.isActiveAndEnabled && currentBehaviour == behaviour.GetBehaviourCode())
				{
					isAnyBehaviourActive = true;
					behaviour.LocalFixedUpdate();
				}
			}
		}
		// Вызвать переопределяющие модели поведения, если таковое имеется
		else
		{
			foreach (GenericBehaviour behaviour in overridingBehaviours)
			{
				behaviour.LocalFixedUpdate();
			}
		}
        
        // Ensure the player will stand on ground if no behaviour is active or overriding.
		if (!isAnyBehaviourActive && overridingBehaviours.Count == 0)
		{
			//rBody.useGravity = true;
			Repositioning();
		}

       /* if ((IsMoving() && targetDirection != Vector3.zero))
		{
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

			Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, behaviourManager.turnSmoothing);
			behaviourManager.GetRigidBody.MoveRotation(newRotation);
			behaviourManager.SetLastDirection(targetDirection);
		}
		// If idle, Ignore current camera facing and consider last moving direction.
		if (!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9))
		{
			behaviourManager.Repositioning();
		}*/

        if (controller.isGrounded)
        {
            Vector3 forward = playerCamera.TransformDirection(Vector3.forward);

		    // Player is moving on ground, Y component of camera facing is not relevant.
            forward.y = 0.0f;
            forward = forward.normalized;

            // Calculate target direction based on camera forward and direction key.
            Vector3 right = new Vector3(forward.z, 0, -forward.x);
            //Vector3 targetDirection;
            move = forward * v + right * h;
            /*
            if (cam != null)
            {
                // calculate camera relative direction to move:
                camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
                move = v*camForward* _speedPerson + h*cam.right* _speedPerson;
                
                Quaternion target = Quaternion.Euler (0, 0, 0); 
                transform.rotation = Quaternion.Slerp (transform.rotation, target , 1);
            }
            else    
            {
                // we use world-relative directions in the case of no main camera
                move = v*Vector3.forward + h*Vector3.right;
            }   
            */
            if(Input.GetKey(KeyCode.LeftShift))
            {
                _speedPerson = _speedShift;
                
            }
            else
            {
                _speedPerson = _speedNormal;
            }
        }


        if ( Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            Debug.Log(move.y);
            move.y = _jumpHeight ;
        }
        
        move.y -= (_gravity * Time.deltaTime);
        controller.Move(move * _speedPerson * 0.01f);
        //animator.SetInteger("_state", _state);
    }

    public void Repositioning()
	{
		if(lastDirection != Vector3.zero)   //поворачивается, если ось "y" не совпадает с камерой
            {
                lastDirection.y = 0;
                Quaternion targetRotation = Quaternion.LookRotation (lastDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSmoothing);
            }
	}
}

