using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "NewTreasure", menuName = "Treasure", order = 0)]
public class Treasure : ScriptableObject {
    
    [SerializeField] private int _scoreVale;
    [SerializeField] private string _treasureName;
    [SerializeField] private string _treasureLore;

    [ShowAssetPreview]
    [SerializeField] private Sprite _treasureIcon;

    [SerializeField] private GameObject _treasurePrefab;

    public int ScoreValue {get => _scoreVale;}
    public string TreasureName {get => _treasureName;}

    public string TreasureLore {get => _treasureLore;}

    public Sprite TreasureIcon {get => _treasureIcon;}

    public GameObject TreasurePregab {get => _treasurePrefab;}

    

    public bool Unlocked;

    public bool Hidden;


}
