using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMainControlScreen", menuName = "MenuState/MainControlScreen", order = 0)]
public sealed class MainControlScreenState : MenuState
{

    public MainControlScreenState(string topRight, string topLeft, string centerText, MenuState centerState, GameObject interactable)
    {
        _topRightText = topRight;
        _topLeftText = topLeft;
        _centerButtonText = centerText;
        _centerButtonTargetState = centerState;
        _screenInteractable = interactable;
    }
    public override EMenuTypes MenuType{get => EMenuTypes.MAINCONTROL;}

    [SerializeField] private string _topRightText;
    [SerializeField] private string _topLeftText;
    [SerializeField] private string _centerButtonText;
    [SerializeField] private MenuState _centerButtonTargetState;
    [SerializeField] private GameObject _screenInteractable;
    public string TopRightText => _topRightText;
    public string TopLeftText => _topLeftText;
    public string CenterButtonText => _centerButtonText;
    public MenuState CenterButtonNewState => _centerButtonTargetState;
    public GameObject ScreenInteractable => _screenInteractable;

    

}
