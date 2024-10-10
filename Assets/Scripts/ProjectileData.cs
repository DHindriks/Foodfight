using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData : MonoBehaviour
{
    public Sprite Icon;

    public Color ProjectileColor;

    Rigidbody rb;
    bool HasReset = false;

    [SerializeField] SpriteRenderer sprite;

    private void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
        }else
        {
            Debug.LogWarning("Could not find rigidbody on this object: " + gameObject.name);
        }

    }

    [ExecuteInEditMode]
    private void OnValidate()
    {
        if (sprite != null)
        {
            sprite.sprite = Icon;
        }
    }
}
