using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifiers : MonoBehaviour {

    public MasterReferences master;

    public enum Modifier
    {
        ANGRY,
        WITHOUT_A_SUN,
        BOUNCY,
        SLIPPERY,
        TEST5,
        TEST6,
        TEST7,
        TEST8,
        TEST9,
        TEST10,
        TEST11,
        TEST12,
        TEST13,
        TEST14,
        TEST15
    };
    
    public List<Modifier> mods = new List<Modifier>();

    public void AddMod (Modifier mod)
    {
        mods.Add(mod);
        master.modHolderDisplay.Add(mod);
    }

    public Modifier AddModAndReturn(Modifier mod)
    {
        mods.Add(mod);
        master.modHolderDisplay.Add(mod);
        return mod;
    }

    public Modifier GetRandomMod ()
    {
        int randomModIndex = Random.Range(0, System.Enum.GetValues(typeof(Modifier)).Length);
        Modifier getMod = GetMod(randomModIndex);
        return mods.Contains(getMod) ? GetRandomMod() : getMod; //if mod already exists on mods list, find another random mod, otherwise use this mod.
    }


    Modifier GetMod (int index)
    {
        return (Modifier)index;
    }

}
