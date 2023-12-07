using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class Movement : NetworkBehaviour
{
    [SerializeField] private float _speed = 10f;

    private void Update() 
    { 
      if (!IsOwner) return;
      transform.position += new Vector3(Input.GetAxis("Horizontal") * _speed, 0, Input.GetAxis("Vertical") * _speed);
    }
}
