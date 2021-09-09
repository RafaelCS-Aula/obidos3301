using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class MiniGame : MonoBehaviour
{
    public abstract void SetupGame();

    public Treasure treasureReward;

    public UnityEvent OnGameComplete;
 
}
