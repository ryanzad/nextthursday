using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour {
    

    
    public enum SelectionMode { INTRO, SINGULAR, BINARY };

    [System.Serializable]
    public struct Selection
    {
        public int level;
        public string intro_msg;
        public SelectionMode selectionMode;
        [HideInInspector] public List<Modifiers.Modifier> mods;
    }

    public List<Selection> Selections = new List<Selection>();


    [System.Serializable]
    public struct ModifierDisplay
    {
        public Modifiers.Modifier mod;
        public string text;
    }

    public List<ModifierDisplay> modDisplays;



    [Header("REFERENCES")]

    public TextMesh textDisplay;

    [HideInInspector] public Modifiers modifiers;
    [HideInInspector] public GameInit gameInit;


    public void Init (int level)
    {
        Selection selection = GetSelection(level);

        GetMods(selection);
        DisplayChoices(selection);
        DisplayText(selection);        
    }
    
    void GetMods(Selection selection)
    {
        for (int i = 0; i < GetChoiceCount(selection); i++)
        {
            selection.mods.Add(GetMod());
        }
    }

    void DisplayChoices (Selection selection)
    {
        SelectionSet selectionSet = GetSelectionSet(selection);

        selectionSet.gameObject.SetActive(true);


        int choiceID = 0;
        foreach (Modifiers.Modifier mod in selection.mods)
        {
            ModifierDisplay modDisplay = GetModDisplay(mod);
            selectionSet.SetupChoice(mod, modDisplay, choiceID);
            choiceID ++;
        }
        
    }

    void DisplayText (Selection selection)
    {
        textDisplay.text = selection.intro_msg;
    }
    



    public void Select (Modifiers.Modifier mod)
    {
        modifiers.AddMod(mod);
        gameInit.StartGame();
        Destroy(this.gameObject);
    }








    // HELPER:


    SelectionSet GetSelectionSet (Selection selection)
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains(selection.selectionMode.ToString())) return child.GetComponent<SelectionSet>();
        }
        return new SelectionSet();
    }

    Modifiers.Modifier GetMod()
    {
        return modifiers.GetRandomMod();
    }



    ModifierDisplay GetModDisplay(Modifiers.Modifier mod)
    {
        foreach (ModifierDisplay modDisplay in modDisplays)
        {
            if (modDisplay.mod == mod) return modDisplay;
        }
        return new ModifierDisplay();
    }

    Selection GetSelection(int level)
    {
        foreach (Selection selection in Selections)
        {
            if (selection.level == level) return selection;
        }
        return new Selection();
    }
    

    int GetChoiceCount(Selection selection)
    {
        int choiceCount = 0;
        switch (selection.selectionMode)
        {
            case SelectionMode.INTRO:
                choiceCount = 0;
                break;

            case SelectionMode.SINGULAR:
                choiceCount = 1;
                break;

            case SelectionMode.BINARY:
                choiceCount = 2;
                break;
        }
        return choiceCount;
    }
}
