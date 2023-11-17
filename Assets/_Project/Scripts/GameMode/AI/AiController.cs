using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    /// states:
    /// go for gem
    /// go for player
    /// run from players
    /// hide (idle) from players
    /// 

    public AiStates aiState {  get; private set; }

}
public enum AiStates { Idle, ChaseGem, ChasePlayer, Run}
