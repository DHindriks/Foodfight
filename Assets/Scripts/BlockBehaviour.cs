﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Health))]
public class BlockBehaviour : MonoBehaviour
{
    Health health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        health.OnHealthChanged.AddListener(CheckHealth);
        GameManager.Instance.CurrentLevel.LevelBlocks.Add(gameObject);
    }

    void CheckHealth()
    {
        if (health.health <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.CurrentLevel.CheckBlocks();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (health != null)
        {
            health.Damage(collision.relativeVelocity.magnitude);
        }
    }
}
