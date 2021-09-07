using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuState : ScriptableObject {
    
    public abstract EMenuTypes MenuType {get;}
    public MenuState SideButtonTargetState;


}


