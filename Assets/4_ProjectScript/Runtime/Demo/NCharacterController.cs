using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NCharacterController : MonoBehaviour
{

    CharacterController characterController;

    Vector3 velocity = Vector3.zero;

    float gravity = -9.3f;


    public float moveSpeed;

    public GameObject CinemachineCameraTarget;


    private int _animIDWork;
    private int _animIDFallingToLanding;


    protected Animator _animator;

    [SerializeField]
    protected Camera _mainCamera;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
        _animator = GetComponent<Animator>();

        _animIDWork = Animator.StringToHash("work");
        _animIDFallingToLanding = Animator.StringToHash("FallingToLanding");


    }


    private void Update()
    {

      

        if (characterController != null)
        {
            Vector3 movement = Vector3.zero;

            //人物行走
            //movement = transform.forward * variableJoystick.Vertical + transform.right * variableJoystick.Horizontal;



            Move(new Vector2(movement.x, movement.z));
            //characterController.Move(movement* moveSpeed * Time.deltaTime);
            //velocity.y += gravity * Time.deltaTime;
            //characterController.Move(velocity*Time.deltaTime);

            if (!movement.Equals(Vector3.zero))
            {
                _animator.SetBool(_animIDWork, true);


                return;
            }

            _animator.SetBool(_animIDWork, false);
            //镜头旋转
#if UNITY_EDITOR

            if (Input.GetKey(KeyCode.Mouse1))
            {
        
                float s01 = Input.GetAxis("Mouse X");  
                float s02 = Input.GetAxis("Mouse Y"); 


                look = new Vector2(s01, s02);

                CameraRotation();

            }
            return;
#endif

            if (Input.touchCount == 1)
            {
                if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    // 手指滑动时，要触发的代码 
                    float s01 = Input.GetAxis("Mouse X");    //手指水平移动的距离
                    float s02 = Input.GetAxis("Mouse Y");    //手指垂直移动的距离


                    look = new Vector2(s01, s02);

                    CameraRotation();
                }

            }




        }
     }


    public Vector2 look = Vector2.zero;

    private const float _threshold = 0.01f;

    //是否锁定摄像机位置
    protected bool LockCameraPosition = false;

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;

    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;

    private void CameraRotation()
    {
        // if there is an input and camera position is not fixed
        if (look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            //Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = 1;

            _cinemachineTargetYaw += look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += look.y * deltaTimeMultiplier;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }


    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }



    private float _animationBlend;

   [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    private float _targetRotation = 0.0f;

    private float _rotationVelocity;


    private float _speed;

    private float _verticalVelocity;

    protected void Move(Vector2 move)
    {

        float targetSpeed = 2;
        _speed = 2;
        _verticalVelocity = 0;
        if (move == Vector2.zero) targetSpeed = 0.0f;


        float currentHorizontalSpeed = new Vector3(characterController.velocity.x, 0.0f, characterController.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude =1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }


        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        // normalise input direction
        Vector3 inputDirection = new Vector3(move.x, 0.0f,move.y).normalized;


        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // move the player
        characterController.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);


    }

}
