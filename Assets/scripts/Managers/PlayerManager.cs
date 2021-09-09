using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Manager;

    public int playerScore = 000;

    public string playerName = "Obama";

    public IList<Treasure> fullTreasureInventory = new Treasure[20];

    public MenuState InventoryMenuState;

    private void Awake() 
    {
        if(Manager == null)
            Manager = this;
        if(Manager != this)
        {
            Destroy(Manager.gameObject);
            Manager = this;
        }
        
        
        DontDestroyOnLoad(this.gameObject);
        
    }

    public void UnlockTreasure(Treasure treasure)
    {
        
        // Check if treasure is new, doesnt matter for now
        foreach(Treasure t in fullTreasureInventory)
        {
            if (t == null)
                continue;
            // This treasure is registred in the inventory
            if(t.TreasureName == treasure.TreasureName)
            {
                
                if(!t.Unlocked)
                {
                    // Anything special to happen on a new unlock?
                    playerScore += t.ScoreValue;

                    t.Unlocked = true;
                    t.Hidden = false;

                }
                
                ScreenStateUpdater.Manager.LoadNewMenu( new DialogueScreenState(t.TreasureLore, t.TreasurePregab,InventoryMenuState));

                return;
            }

        }

        Debug.Log("No treasure of that name on the inventory");

    }
}
