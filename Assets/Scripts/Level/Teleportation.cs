using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [SerializeField] private GameObject _pointEnd;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.position = _pointEnd.transform.position;
    }
}
