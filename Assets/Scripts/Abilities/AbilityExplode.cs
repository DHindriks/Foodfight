﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityExplode : AbilityBase
{
    [SerializeField]
    int ExplosionRange;


    [SerializeField]
    int ExplosionForce = 40000;

    [SerializeField]
    GameObject ParticlePrefab;

    [SerializeField]
    int Delay = 0;

    bool Exploded = false;

    bool exploding = false;
    void OnCollisionEnter(Collision collision)
{
        if (!AbilityLocked && !exploding && !Exploded)
        {
            exploding = true;
            //GetComponent<Rigidbody>().isKinematic = true;
            GameObject Particle = Instantiate(ParticlePrefab);
            Particle.transform.position = transform.position;
            Invoke("Explode", Delay);
            //GameManager.Instance.cameraScript.Invoke("ResetCam", Delay / 2);
        }
    }

    void Explode()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, ExplosionRange);
        foreach (Collider c in objects)
        {
            Rigidbody r = c.GetComponent<Rigidbody>();
            if (r != null)
            {
                r.AddExplosionForce(ExplosionForce, transform.position, ExplosionRange);
            }

        }
        Exploded = true;
    }

}
