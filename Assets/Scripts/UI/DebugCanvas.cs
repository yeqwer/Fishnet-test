using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _debugText;
    
    private PlayerInput _playerInput;

    void Awake() 
    {
        _playerInput = new PlayerInput();
    }

    void Update()
    {
        _debugText.text = _playerInput.InGame.Move.ReadValue<Vector2>().ToString();
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