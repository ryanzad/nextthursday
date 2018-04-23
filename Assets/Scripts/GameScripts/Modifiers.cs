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
        SLIPPERY
    };
    
    public List<Modifier> mods = new List<Modifier>();

    public void AddMod (Modifier mod)
    {
        mods.Add(mod);
    }

    public Modifier AddModAndReturn(Modifier mod)
    {
        mods.Add(mod);
        return mod;
    }

    public Modifier GetRandomMod ()
    {
        int randomModIndex = Random.Range(0, 4);
        Modifier getMod = GetMod(randomModIndex);
        return mods.Contains(getMod) ? GetRandomMod() : getMod; //if mod already exists on mods list, find another random mod, otherwise use this mod.
    }


    Modifier GetMod (int index)
    {
        return (Modifier)index;
    }

}
