using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifiers : MonoBehaviour {

    public MasterReferences master;

    public enum Modifier
    {
        ANGRY, //enemies move angrily
        WITHOUT_A_SUN, //night
        BOUNCY, //bouncy
        SLIPPERY, //slippery
        GRUESOME, //blood stains
        BIGGER, //players are smaller
        STORMY, //lightning storm
        TRIPPY, //trippy
        RED, //red tint
        COOL_AF, //explosions
        FROM_ANOTHER_TIME, //grainy
        FASTER, //faster
        PUNISHING, //allies occasionally explode, screenshake increases
        HOT //floor is lava
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
