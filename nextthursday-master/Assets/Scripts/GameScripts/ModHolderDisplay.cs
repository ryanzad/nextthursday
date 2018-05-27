using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModHolderDisplay : MonoBehaviour {

    public List<GameObject> modHolders;
    int modsAdded = 0;


    [System.Serializable]
    public struct ModifierDisplay
    {
        public Modifiers.Modifier mod;
        public Texture icon;
    }

    public List<ModifierDisplay> modDisplays;


    private void Start()
    {
        AllOff();
    }


    void AllOff ()
    {
        foreach (GameObject modHolder in modHolders)
        {
            modHolder.SetActive(false);
        }
        modsAdded = 0;
    }

    public void Add (Modifiers.Modifier mod)
    {
        modHolders[modsAdded].SetActive(true);
        ModifierDisplay modDisplay = GetModDisplay(mod);
        Renderer modRender = modHolders[modsAdded].GetComponent<Renderer>();
        modRender.material.mainTexture = modDisplay.icon;
        modsAdded++;
    }
    




    // HELPER:


    ModifierDisplay GetModDisplay(Modifiers.Modifier mod)
    {
        foreach (ModifierDisplay modDisplay in modDisplays)
        {
            if (modDisplay.mod == mod) return modDisplay;
        }
        return new ModifierDisplay();
    }
}
