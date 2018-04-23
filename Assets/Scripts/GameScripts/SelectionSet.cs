using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSet : MonoBehaviour {

    public List<SelectionButton> choices;
    
    public void SetupChoice (Modifiers.Modifier mod, SelectionHandler.ModifierDisplay modDisplay,  int choiceID)
    {
        choices[choiceID].Setup(mod, modDisplay);
    }
}
