using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSCript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Animator navTarget;

    

    public void ButtonPress(){
        animator.SetBool("ButtonPress",true);
        animator.SetBool("NavigationTargetSelected",false);
    }

    public void navigationTargetSelected(){
        animator.SetBool("NavigationTargetSelected",true);
        animator.SetBool("ButtonPress",false);
        navTarget.SetBool("locationPressed",true);
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
