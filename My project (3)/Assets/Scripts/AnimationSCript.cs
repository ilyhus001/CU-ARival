using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSCript : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void ButtonPress(){
        animator.SetBool("ButtonPress",true);
    }
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("ButtonPress",false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ButtonPress: " + animator.GetBool("ButtonPress"));
    }
}
