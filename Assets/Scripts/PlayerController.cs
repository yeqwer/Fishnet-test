using FishNet.Component.Animating;
using FishNet.Object;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [Header("Palyer settings")]
    [SerializeField] private float _rotateSpeed = 10f;
    [SerializeField] private float _runSpeed = 2f;

    [Header("Info")]
    [SerializeField] private Animator _anim;
    [SerializeField] private NetworkAnimator _netAnim;
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

    private void Update()
    {
        CheckDance();
        PlayerMove();
    }

    private void PlayerMove()
    {
        if(_playerInput.InGame.Move.inProgress) 
        {
            var dir = _playerInput.InGame.Move.ReadValue<Vector2>();
            
            if (dir != Vector2.zero) 
            {
                Vector3 movement = new Vector3(dir.x, 0, dir.y) * _runSpeed * Time.deltaTime;
                transform.Translate(movement);

                if (movement.magnitude > 0.001f) 
                {
                    Quaternion lookRotate = Quaternion.LookRotation(movement);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotate, Time.deltaTime * _rotateSpeed);
                }

                _anim.SetBool("Run", true);
            } 
            else 
            {
                _anim.SetBool("Run", false);
            }
        } 
    }

    private void CheckDance()
    {
        if (_playerInput.InGame.Dance.triggered) 
        {
            _netAnim.SetTrigger("Dance");
        }
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