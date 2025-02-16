using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiedFromLavaOrPoisonGrass : MonoBehaviour
{
    [SerializeField] private GameObject _text;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other is BoxCollider2D && other.TryGetComponent(out Player player))
        {
            _text.SetActive(true);
            Destroy(player.gameObject);
        }
    }
}
