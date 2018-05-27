using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMod : MonoBehaviour {

    public MasterReferences master;
    public Assets.Pixelation.Scripts.Pixelation pixelateEffect;
    
    public void ModSettings ()
    {
        foreach (Modifiers.Modifier mod in master.modifiers.mods)
        {
            ModSettings_Check(mod);
        }
    }

    void ModSettings_Check(Modifiers.Modifier mod)
    {
        switch (mod)
        {
            case Modifiers.Modifier.FROM_ANOTHER_TIME:
                Mod_Retro();
                break;
        }
    }

    void Mod_Retro()
    {
        pixelateEffect.enabled = true;
    }



}
