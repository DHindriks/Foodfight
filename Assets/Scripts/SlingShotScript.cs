﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody ballRB;

    [SerializeField]
    public GameObject Origin;

    [SerializeField]
    public Camera cam;

    [SerializeField]
    bool CanGrab = false;

    bool IsShot = false;

    float Currentcooldown = 1;

    TrajectoryPreview preview;

    [SerializeField] TurnManager turnManager;

    float currentlayer;

    float Layer;


    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        preview = GetComponent<TrajectoryPreview>();
        preview.enabled = false;
    }

    void OnMouseDrag()
    {
        if (ballRB != null)
        {
            //if ball hasn't been shot yet, move the ball to player's mouse location, max 2 units from the slingshot origin.
            if (Input.GetMouseButton(0) && !IsShot)
            {
                //calculate mouse position on the screen
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Vector3.Distance(cam.transform.position, ballRB.position) - cam.nearClipPlane;
                mousePos = cam.ScreenToWorldPoint(mousePos);
                if (Vector3.Distance(mousePos, Origin.transform.position) > 2)
                {
                    ballRB.position = Origin.transform.position + (mousePos - Origin.transform.position).normalized * 2;
                }
                else
                {
                    ballRB.position = mousePos;
                }

                if (!preview.isActiveAndEnabled)
                {
                    preview.enabled = true;
                    if (ballRB.GetComponent<ProjectileData>())
                    {
                        preview.line.startColor = ballRB.GetComponent<ProjectileData>().ProjectileColor;
                        preview.line.endColor = ballRB.GetComponent<ProjectileData>().ProjectileColor;
                    }
                }
                else
                {
                    Vector3 vel = ((transform.position - ballRB.transform.position) * 15);
                    preview.velocity.x = vel.x;
                    preview.velocity.y = vel.y;
                }

            }

            
        }
    }

    void OnMouseUp()
    {
        //shoots the object
        if (ballRB != null && Input.GetMouseButtonUp(0) && Vector3.Distance(ballRB.position, Origin.transform.position) > 0.3f)
        {
            IsShot = true;
            ballRB.isKinematic = false;
            ballRB.AddForce(((transform.position - ballRB.transform.position) * 15), ForceMode.VelocityChange);
            ballRB.AddRelativeTorque(new Vector3(Random.Range(0, 0), Random.Range(0, 0), Random.Range(0, 10)), ForceMode.VelocityChange);
            if (ballRB.gameObject.GetComponent<AbilityBase>())
            {
                ballRB.GetComponent<AbilityBase>().AbilityLocked = false;
            }
            if (GameManager.Instance != null)
            {
                GameManager.Instance.cameraScript.SetTarget(ballRB.gameObject);
            }
            ballRB = null;
            Currentcooldown = Time.time + 1.5f;

            if (preview.isActiveAndEnabled)
            {
                preview.enabled = false;
            }
            turnManager.SwapTurn();
        }
    }

    public void SpawnProjectile(GameObject Prefab)
    {
        if (ballRB == null)
        {
            GameObject NewProjectile = Instantiate(Prefab);
            if (GameManager.Instance != null)
            {
                GameManager.Instance.CurrentLevel.ShotsFired++;
            }
            if (NewProjectile.GetComponent<Rigidbody>())
            {
                SetProjectile(NewProjectile.GetComponent<Rigidbody>());
            }else
            {
                Debug.LogError("Couldn't find RigidBody component on spawned projectile.");
            }
        }
    }

    void SetProjectile (Rigidbody rb)
    {
        if (GameManager.Instance != null && rb.gameObject == GameManager.Instance.cameraScript.FollowTarget || 0 == 0)
        {
            //GameManager.Instance.cameraScript.SetTarget(gameObject);
        }
        rb.isKinematic = true;
        rb.gameObject.transform.position = transform.position;
        ballRB = rb;
        IsShot = false;
        if (ballRB.gameObject.GetComponent<AbilityBase>())
        {
            ballRB.GetComponent<AbilityBase>().AbilityLocked = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() && ballRB == null && Currentcooldown < Time.time && CanGrab)
        {
            SetProjectile(other.gameObject.GetComponent<Rigidbody>());
        }    
    }


}