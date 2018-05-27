using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public MoveMotor motor;

    public Renderer renderer;

    private void Start()
    {

        foreach (Modifiers.Modifier mod in motor.master.modifiers.mods)
        {
            ModSettings_Start(mod);
        }


    }






    void ModSettings_Start(Modifiers.Modifier mod)
    {
        switch (mod)
        { 
            case Modifiers.Modifier.ANGRY:
                Mod_Angry();
                break;
        }
    }


    void Mod_Angry()
    {
        renderer.material.color = Color.red;
    }




}
