using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class MiniGame : MonoBehaviour
{
    public abstract void SetupGame();

    public Treasure treasureReward;

    public MenuState nextGame;

    public UnityEvent OnGameComplete;
 
    public KeyCode instantWinKey = KeyCode.P;
    protected virtual void WinGame()
    {
        if(treasureReward != null)
        {
            PlayerManager.Manager.UnlockTreasure(treasureReward);
        }
        else
        {
            //ScreenStateUpdater.Manager.SwitchOnScreenInteractable(nextGame.gameObject);

            ScreenStateUpdater.Manager.LoadNewMenu(nextGame);
            print("Switching minigame");
        }        
    }

    protected virtual void Update() {
        if(Input.GetKeyDown(instantWinKey))
        {
            WinGame();
        }
    }
}
