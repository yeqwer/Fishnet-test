using FishNet.Component.Animating;
using FishNet.Object;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [Header("Palyer settings")]
    [SerializeField] private float _mouseSensitivities = 0.5f;
    [SerializeField] private GameObject _playerModel;
    [SerializeField] private float _minimumAngle = 70f;    
    [SerializeField] private float _maximumAngle = 310f;

    [Header("Info")]
    [SerializeField] private Animator _anim;
    [SerializeField] private NetworkAnimator _netAnim;
    [SerializeField] private GameObject _cameraFollow;
    [SerializeField] private Camera _personalCam;
    private PlayerInput _playerInput;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(base.IsOwner)
        {
            _playerInput.Enable();
        }
        else
        {
            _playerInput.Disable();
        }
    }

    private void Awake() 
    {
        _playerInput = new PlayerInput();
    }

    private void Start() 
    {
        // Hide and lock cursor
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    private void Update()
    {
        if (!IsOwner) return;
        PlayerMove();
        CameraMove();

        // Camera follow the body
        _cameraFollow.transform.position = new Vector3(_playerModel.transform.position.x, _playerModel.transform.position.y + 1.8f, _playerModel.transform.position.z);
    }

    private void CameraMove()
    {
        // Set camera dir
        Vector2 direction = _playerInput.InGame.CameraMove.ReadValue<Vector2>();
        _cameraFollow.transform.eulerAngles += new Vector3(-direction.y * _mouseSensitivities, direction.x * _mouseSensitivities, 0);
        
        // Set min and mix angles for camera 
        float angleX = _cameraFollow.transform.eulerAngles.x;
        if (angleX > 180 && angleX < _maximumAngle)
        {
            angleX = _maximumAngle;
        } 
        else if (angleX < 180 && angleX > _minimumAngle)
        {
            angleX = _minimumAngle;
        }
        _cameraFollow.transform.eulerAngles = new Vector3(angleX, _cameraFollow.transform.eulerAngles.y, 0); 
        
        // Set smooth follow body for camera
        if (_playerInput.InGame.Move.IsInProgress())
        {
            _playerModel.transform.rotation = Quaternion.Slerp(_playerModel.transform.rotation, Quaternion.Euler(0, _cameraFollow.transform.localEulerAngles.y, 0), Time.deltaTime * 20);
        }
    }

    private void PlayerMove()
    {
        //Set dir for move
        Vector2 direction;
        if (_playerInput.InGame.Move.IsInProgress())
        {
            direction = _playerInput.InGame.Move.ReadValue<Vector2>();

            if (_playerInput.InGame.Sprint.IsInProgress()) 
            {
                direction = direction + new Vector2(0, 1);
            }
        }
        else
        {
            direction = Vector2.zero;
        }

        //Move animations
        _netAnim.Animator.SetFloat("X", Mathf.Lerp(_netAnim.Animator.GetFloat("X"), direction.x, Time.deltaTime * 20));
        _netAnim.Animator.SetFloat("Y", Mathf.Lerp(_netAnim.Animator.GetFloat("Y"), direction.y, Time.deltaTime * 20));
    }

    private void OnEnable() 
    {
        _playerInput.Enable();
    }

    private void OnDisable() 
    {
        _playerInput.Disable();
    }
}