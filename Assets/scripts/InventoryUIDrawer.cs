using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIDrawer : MonoBehaviour
{
    public RectTransform canvasRect;
    public GameObject baseInventoryButton;

    public int itemsPerRow = 4;

    // Start is called before the first frame update
    void Start()
    {
        int rowAmount = Mathf.CeilToInt(PlayerManager.Manager.fullTreasureInventory.Length / itemsPerRow);

        int currentTreasureIndex = 0;
        for(int y = 0; y <= rowAmount; y++)
        {
            for(int x = 0; x <= itemsPerRow; x++)
            {
                if (currentTreasureIndex >= PlayerManager.Manager.fullTreasureInventory.Length)
                    return;
                Treasure currentTreasure = PlayerManager.Manager.fullTreasureInventory[currentTreasureIndex];
                if (currentTreasure.Hidden && !currentTreasure.Unlocked)
                {
                    currentTreasureIndex++;
                    continue;
                }
                GameObject newButton = Instantiate(baseInventoryButton, transform);
                Image imgComponent = newButton.GetComponent<Image>();


                try
                {
                    if (currentTreasure.Unlocked)
                    {
                        imgComponent.sprite = currentTreasure.TreasureIcon;
                    }
                    else
                        imgComponent.sprite = PlayerManager.Manager.lockedTreasureSprite;


                }
                catch 
                {
                    print("No Valid Sprites for this one");
                } 
                
                
                currentTreasureIndex++;

            }

        }


        
    }


}
