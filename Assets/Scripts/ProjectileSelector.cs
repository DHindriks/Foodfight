using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileSelector : MonoBehaviour
{
    [SerializeField] SlingShotScript Slingshot;

    [SerializeField]
    List<GameObject> ProjectileList;


    [SerializeField]
    List<GameObject> ProjectileDBGList;

    [SerializeField]
    bool DBG;

    [SerializeField]
    GameObject Button;

    [SerializeField]Animator animator;

    void Start()
    {
        GenerateList();
        animator = GetComponentInParent<Animator>();
    }


    public void ToggleWindow()
    {
        animator.SetBool("Open", !animator.GetBool("Open"));
    }

    public void SetWindowState(bool open)
    {
        animator.SetBool("Open", open);
    }

    void GenerateList()
    {
        foreach(GameObject Projectile in ProjectileList)
        {
            GameObject Btn = Instantiate(Button, transform);
            Btn.GetComponent<Image>().sprite = Projectile.GetComponent<ProjectileData>().Icon;
            Btn.GetComponent<Button>().onClick.AddListener(delegate { ToggleWindow(); });
            Btn.GetComponent<Button>().onClick.AddListener(delegate { Slingshot.SpawnProjectile(Projectile); });
        }

        if (DBG)
        {
            foreach (GameObject Projectile in ProjectileDBGList)
            {
                GameObject Btn = Instantiate(Button, transform);
                Btn.GetComponent<Image>().sprite = Projectile.GetComponent<ProjectileData>().Icon;
                Btn.GetComponent<Button>().onClick.AddListener(delegate { ToggleWindow(); });
                Btn.GetComponent<Button>().onClick.AddListener(delegate { Slingshot.SpawnProjectile(Projectile); });
            }
        }
    }
}
