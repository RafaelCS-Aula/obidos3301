using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewTreasure", menuName = "Treasure", order = 0)]
public class Treasure : ScriptableObject {
    
    [SerializeField] private int _scoreVale;
    [SerializeField] private string _treasureName;
    [SerializeField] private string _treasureLore;
    [SerializeField] private Sprite _treasureIcon;


}
