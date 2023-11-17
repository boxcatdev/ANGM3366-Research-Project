using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    /// states:
    /// go for gem
    /// go for player
    /// run from players
    /// hide (idle) from players
    /// 

    //components
    private NavMeshAgent agent;

    public AiStates aiState { get; private set; }

    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        agent = GetComponent<NavMeshAgent>();
        //AssignAnimationIDs();
    }
    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }
}
public enum AiStates { Idle, ChaseGem, ChasePlayer, Run}
