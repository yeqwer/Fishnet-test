using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSolo : MonoBehaviour
{
    [SerializeField] private float _speed = 0.1f;
    void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal") * _speed, 0, Input.GetAxis("Vertical") * _speed);
    }
}
