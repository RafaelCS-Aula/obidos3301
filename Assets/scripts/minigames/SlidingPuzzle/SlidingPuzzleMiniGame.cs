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
    public override void SetupGame()
    {
        boardTiles = new SlidingPuzzleTile[(boardSize * boardSize) - 1];
        _chosenImage = possibleImages[Random.Range(0, possibleImages.Length - 1)];
        _boardImages = SplitImage(_chosenImage);

        occupationGrid = new bool[boardSize, boardSize];

      

        // Populate the board
        Vector2 emptyTile = new Vector2(boardSize - 1, boardSize - 1);

        int placedTileCount = 0;

        for(int y = 0; y < boardSize; y++)
        {
            for(int x = 0; x < boardSize; x++)
            {
                if (x == emptyTile.x && y == emptyTile.y)
                {
                    occupationGrid[x, y] = false;
                    continue;
                }
                    

                GameObject btn = Instantiate(baseTileButton, this.transform);
                btn.GetComponent<Image>().sprite = Sprite.Create(_boardImages[placedTileCount], 
                    new Rect(0.0f, 0.0f, _boardImages[placedTileCount].width, _boardImages[placedTileCount].height), 
                    new Vector2(_boardImages[placedTileCount].width / 2, _boardImages[placedTileCount].height / 2));


                SlidingPuzzleTile tile = new SlidingPuzzleTile(new Vector2(x, y));
                boardTiles[placedTileCount] = tile;
                btn.GetComponent<Button>().onClick.AddListener(tile.TryMove);
                occupationGrid[x, y] = true;
                placedTileCount++;

            }

        }

        //Shuffle board
        System.Array.Sort(boardTiles, (x,y) => Random.Range(-1, 1));

        int counter = 0;
        for(int y = 0; y < boardSize; y++)
        {
            for(int x = 0; x < boardSize; x++)
            {
                boardTiles[0].gridCurrentPosition = new Vector2(x, y);
                counter++;
            }
        }
     

    }



    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

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
