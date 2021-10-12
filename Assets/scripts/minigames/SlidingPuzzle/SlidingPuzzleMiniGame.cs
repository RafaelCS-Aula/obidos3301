using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;


public class SlidingPuzzleMiniGame : MiniGame
{

    [ShowAssetPreview]
    public Texture2D[] possibleImages; 

    [Range(3,25)]
    public int boardSize;
    
    
    private Texture2D _chosenImage;

    private Texture2D[] _boardImages;

    public SlidingPuzzleTile[] boardTiles;

    static public bool[,] occupationGrid;


    public GameObject baseTileButton;

    public bool shuffle = true;
    private GameObject emptyDummy;

    public GridLayoutGroup grid;

    private void Start()
    {
        SetupGame();
    }

    public override void SetupGame()
    {
        boardTiles = new SlidingPuzzleTile[(boardSize * boardSize) - 1];
        _chosenImage = possibleImages[Random.Range(0, possibleImages.Length - 1)];
        _boardImages = SplitImage(_chosenImage);

        occupationGrid = new bool[boardSize, boardSize];


        if(grid != null)
        {
            grid.constraintCount = boardSize;
        }
       
        
      

        // Populate the board
        Vector2 emptyTileCoords = new Vector2(boardSize - 1, boardSize-1);
       // SlidingPuzzleTile emptyTile = new SlidingPuzzleTile(emptyTileCoords, boardSize, nu);

        int placedTileCount = 0;

        print(boardSize);
        for(int y = 0; y < boardSize; y++)
        {
            for(int x = 0; x < boardSize; x++)
            {
                if (x == emptyTileCoords.x && y == emptyTileCoords.y)
                {
                    occupationGrid[x, y] = false;
                    print("Placed empty at: " + x + y);
                    continue;
                }
                    

                GameObject btn = Instantiate(baseTileButton, this.transform);
                btn.GetComponent<Image>().sprite = Sprite.Create(_boardImages[placedTileCount], 
                    new Rect(0.0f, 0.0f, _boardImages[placedTileCount].width, _boardImages[placedTileCount].height), 
                    new Vector2(_boardImages[placedTileCount].width / 2, _boardImages[placedTileCount].height / 2));


                SlidingPuzzleTile tile = new SlidingPuzzleTile(new Vector2(x, y),boardSize,btn);
                boardTiles[placedTileCount] = tile;
                btn.GetComponent<Button>().onClick.AddListener(tile.TryMove);
                btn.name = $"Tile: {tile.gridSolutionPosition} , child Index: {tile.GetChildIndex()}";
                occupationGrid[x, y] = true;
                placedTileCount++;
                

            }

        }


        //Shuffle board

        if (shuffle)
        {
            System.Array.Sort(boardTiles, (x, y) => Random.Range(-1, 1));

            int counter = 0;
            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    if (counter >= boardTiles.Length)
                        continue;
                   // print("shuffling tile at index" + counter);
                    boardTiles[counter].gridCurrentPosition = new Vector2(x, y);



                    counter++;
                }
            }

        }
       

        //Add a dummy empty tile
        emptyDummy = Instantiate(baseTileButton, this.transform);
        emptyDummy.GetComponent<Button>().enabled = false;
        emptyDummy.GetComponent<Image>().enabled = false;

        //Place the Tiles according to their shuffling
        foreach (SlidingPuzzleTile t in boardTiles)
        {
            t.inWorldTile.transform.SetSiblingIndex(t.GetChildIndex());
            t.emptyDummy = emptyDummy;
        }

    }





    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (CheckVictory())
            WinGame();
                

    }


    private bool CheckVictory()
    {
        foreach (SlidingPuzzleTile t in boardTiles)
        {
            if (t.gridCurrentPosition != t.gridSolutionPosition)
                return false;
        }

        return true;
    }

    private Texture2D[] SplitImage(Texture2D fullImage)
    {
        List<Texture2D> cells = new List<Texture2D>();
        int cellPixelSizeX = fullImage.width / boardSize;
        int cellPixelSizeY = fullImage.height / boardSize;

        for(int y = 0; y < boardSize; y++)
        {
            for (int x = 0;x < boardSize; x++)
            {
                Texture2D t = new Texture2D(cellPixelSizeX, cellPixelSizeY);
                t.SetPixels(fullImage.GetPixels(cellPixelSizeX * x, cellPixelSizeY * y, cellPixelSizeX, cellPixelSizeY));
                t.Apply();
                cells.Add(t);
            }
                


        }

        return cells.ToArray();

    }

}
