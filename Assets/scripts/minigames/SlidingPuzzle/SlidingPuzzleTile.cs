using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlidingPuzzleTile 
{
   // public Texture2D image;
    public Vector2 gridSolutionPosition { get; private set; }
    public Vector2 gridCurrentPosition;
    public int gridSize { get; private set; }
    public GameObject inWorldTile;
    public GameObject emptyDummy;

    public SlidingPuzzleTile(Vector2 solutionPosition, int gridSize, GameObject inWorldTile)
    {
        gridSolutionPosition = solutionPosition;
        gridCurrentPosition = gridSolutionPosition;
        this.gridSize = gridSize;
        this.inWorldTile = inWorldTile;

        //Debug.Log("Placing Tile at correct position:" + gridSolutionPosition);
    }

    public void TryMove()
    {
        //Debug.Log("Try move on tile at:" + gridCurrentPosition + "child index: " + GetChildIndex());
        //Debug.Log("Tile at:" + gridCurrentPosition + "has grid spot marked as occupied: " + SlidingPuzzleMiniGame.occupationGrid[(int)gridCurrentPosition.x, (int)gridCurrentPosition.y]);

        Vector2[] neighbourCoords = 
            {
            new Vector2(0, 1),
            new Vector2(1, 0),
            new Vector2(0, -1),
            new Vector2(-1, 0) };

        foreach(Vector2 c in neighbourCoords)
        {
            Vector2 tentativeCoords = gridCurrentPosition + c;
            Debug.Log("At " + gridCurrentPosition + "Trying to move to coord:" + tentativeCoords);
            try
            {
                if (SlidingPuzzleMiniGame.occupationGrid[(int)tentativeCoords.x, (int)tentativeCoords.y] == false)
                {
                    Debug.Log("Found empty");
                    // where I am is now empty
                    SlidingPuzzleMiniGame.occupationGrid[(int)gridCurrentPosition.x, (int)gridCurrentPosition.y] = false;

                    // update coords
                    emptyDummy.transform.SetSiblingIndex(GetChildIndex());
                    gridCurrentPosition = tentativeCoords;

                    // where i moved to is now occupied
                    SlidingPuzzleMiniGame.occupationGrid[(int)gridCurrentPosition.x, (int)gridCurrentPosition.y] = true;

                    // update visuals of the gridLayout component
                    inWorldTile.transform.SetSiblingIndex(GetChildIndex());
                    return;
                }


            } catch (IndexOutOfRangeException e) { continue; }
            
        }



    }

    public int GetChildIndex()
    {
        //Grid is filled from left to right, bottom to top
        // assuming grid size 3:
        // (0,0) is child 0
        // (1,0) is child 1
        // (2,0) is child 2
        // (0,1) is child 3
        // ...

        int childIndex = (int)(gridCurrentPosition.x + (gridCurrentPosition.y * gridSize));
        return childIndex;
    }


}
