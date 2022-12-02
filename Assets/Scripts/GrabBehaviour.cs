using System;
using UnityEngine;

public class GrabBehaviour : StateMachineBehaviour
{
    private GrabController _controller;
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_controller == null)
            _controller = animator.gameObject.GetComponentInChildren<GrabController>();
        
        _controller.Grab();
    }
}