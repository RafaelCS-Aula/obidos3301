using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueScreen", menuName = "MenuState/DialogueScreen", order = 0)]
public sealed class DialogueScreenState : MenuState
{
    public override EMenuTypes MenuType{get => EMenuTypes.DIALOGUE;}

    [SerializeField] private string _dialogueText;
    [SerializeField] private Sprite _speakerAvatar;
    public string DialogueText => _dialogueText;
    public Sprite SpeakerAvatar => _speakerAvatar;


    

}
