using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleScript : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnMouseUpAsButton()
    {
        bool _drawerState = anim.GetBool("DrawerOpen");
        anim.SetBool("DrawerOpen", !_drawerState);
    }
}
