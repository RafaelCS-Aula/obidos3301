using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzleTile 
{
   // public Texture2D image;
    public Vector2 gridSolutionPosition { get; private set; }
    public Vector2 gridCurrentPosition;

    public SlidingPuzzleTile(Vector2 solutionPosition)
    {
        gridSolutionPosition = solutionPosition;
    }

    public void TryMove()
    {


    }


}
