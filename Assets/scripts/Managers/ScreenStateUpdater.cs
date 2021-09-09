using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class ScreenStateUpdater : MonoBehaviour
{
    public static ScreenStateUpdater Manager;
    public UIDocument MainControlMenuDocument;
    public UIDocument DialogueMenuDocument;

    [SerializeField] private MenuState _initialState;
    
    private MenuState _previousState;

    private MenuState _currentState;

    private VisualElement _mainRoot => MainControlMenuDocument.rootVisualElement;
    private VisualElement _dialogueRoot => DialogueMenuDocument.rootVisualElement;

   // General Elements
    private string _sideButtonName = "side-button";

    //MainControl elements
    private string _centralButtonName = "central-button";
    private string _centralButtonLabelName = "button-label";

    private string _topLeftLabelName = "top-left-label";
    private string _topRightLabelName = "top-right-label";


    // Dialogue Elements
    private string _dialogueLabelName = "dialogue-label";
    private string _dialogueAvatarName = "dialogue-avatar";



    private GameObject _currentInteractable;

    private void OnEnable() 
    {
        if(Manager == null)
            Manager = this;
        if(Manager != this)
        {
            Destroy(Manager.gameObject);
            Manager = this;
        }
        
        
        DontDestroyOnLoad(this.gameObject);

        if(MainControlMenuDocument == null || DialogueMenuDocument == null)
        {
            Debug.Log("UI Document component missing - assign it to inspector");
            gameObject.SetActive(false);
        }    

        LoadNewMenu(_initialState);

    }

    public void LoadNewMenu(MenuState newState)
    {
        Button sideButton;
        _previousState = _currentState;
        _currentState = newState;
        
        switch(_currentState.MenuType)
        {
            case EMenuTypes.MAINCONTROL:
            MainControlScreenState explicitMainState = _currentState as MainControlScreenState;

            // Enable the correct UI
            MainControlMenuDocument.gameObject.SetActive(true);
            DialogueMenuDocument.gameObject.SetActive(false);

            // Get the elements of the UI relevant to us
            sideButton = _mainRoot.Q<Button>(_sideButtonName);
            Button centralButton = _mainRoot.Q<Button>(_centralButtonName);
            Label topLeftLabel = _mainRoot.Q<Label>(_topLeftLabelName);
            Label topRightLabel = _mainRoot.Q<Label>(_topRightLabelName);
            Label centralButtonLabel = _mainRoot.Q<Label>(_centralButtonLabelName);
            

            // Assign the contents
            sideButton.clicked -= OnSideButtonClicked;
            sideButton.clicked += OnSideButtonClicked;

            centralButton.clicked -= OnCenterButtonClicked;
            centralButton.clicked += OnCenterButtonClicked;

            topLeftLabel.text = ParseTextVariables(explicitMainState.TopLeftText);
            topRightLabel.text = ParseTextVariables(explicitMainState.TopRightText);
            centralButtonLabel.text = ParseTextVariables(explicitMainState.CenterButtonText);
            
            GameObject interactable = explicitMainState.ScreenInteractable;
            _currentInteractable = Instantiate(interactable,Vector2.zero,Quaternion.identity);

            break;

            case EMenuTypes.DIALOGUE:
            DialogueScreenState explicitDialogueState = _currentState as DialogueScreenState;

            // Enable the correct UI
            MainControlMenuDocument.gameObject.SetActive(false);
            DialogueMenuDocument.gameObject.SetActive(true);

            // Get the elements of the UI relevant to us
            sideButton = _dialogueRoot.Q<Button>(_sideButtonName);
            //IMGUIContainer speakerAvatar = _dialogueRoot.Q<IMGUIContainer>(_dialogueAvatarName);
            Label dialogue = _dialogueRoot.Q<Label>(_dialogueLabelName);

            //Assing the contents
            dialogue.text = ParseTextVariables(explicitDialogueState.DialogueText);
            
     
            sideButton.clicked -= OnSideButtonClicked;
            sideButton.clicked += OnSideButtonClicked;
            

            break;
        }
    }

    public void SwitchInteractable(GameObject newInteractable)
    {
        Destroy(_currentInteractable);
        _currentInteractable = Instantiate(newInteractable,Vector2.zero,Quaternion.identity);
    }
    private void OnSideButtonClicked()
    {
        if(_currentState.SideButtonTargetState != null)
            LoadNewMenu(_currentState.SideButtonTargetState);
        else
            LoadNewMenu(_previousState);
    } 

    private void OnCenterButtonClicked() => LoadNewMenu(
        (_currentState as MainControlScreenState).CenterButtonNewState);

    private string ParseTextVariables(string originalText)
    {
        string playerNameVariable = ">player";
        string playerScoreVariable = ">score";

        string finalText = originalText.Replace(playerNameVariable, PlayerManager.Manager.playerName);
        finalText = finalText.Replace(playerScoreVariable,PlayerManager.Manager.playerScore.ToString());

        return finalText;
    }
    
}
