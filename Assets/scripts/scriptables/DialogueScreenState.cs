using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueScreen", menuName = "MenuState/DialogueScreen", order = 0)]
public sealed class DialogueScreenState : MenuState
{
    public DialogueScreenState(string dialogueText, GameObject avatar, MenuState sideButtonTargetState)
    {
        _dialogueText = dialogueText;
        _speakerAvatar = avatar;
        SideButtonTargetState = sideButtonTargetState;
    }

    public override EMenuTypes MenuType{get => EMenuTypes.DIALOGUE;}

    [SerializeField] private string _dialogueText;
    [SerializeField] private GameObject _speakerAvatar;
    public string DialogueText => _dialogueText;
    public GameObject SpeakerAvatar => _speakerAvatar;


    

}
