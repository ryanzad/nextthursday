using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHandler : MonoBehaviour {

    public MasterReferences master;
    public string endScene;

    public Animator blackFader;

    bool playerDied = false;


    public void NextScene ()
    {
        if (playerDied) return;


        PlayerPrefs.SetInt("LevelLoad", !PlayerPrefs.HasKey("LevelLoad") ? 2 : PlayerPrefs.GetInt("LevelLoad") + 1); //increases level number
        PlayerPrefs.SetInt("Allies", master.scorer.GetAllyCount()); //saves the number of allies for the next round

        int roundScore = master.scorer.GetAllyCount() + master.scorer.GetEnemyDeaths() ;

        
        PlayerPrefs.SetInt("GameScore", PlayerPrefs.HasKey("GameScore") ? PlayerPrefs.GetInt("GameScore") + roundScore : roundScore);

        int levelToLoad = PlayerPrefs.GetInt("LevelLoad");
        if (levelToLoad <= 5)
        {
            SaveMods();
            Application.LoadLevel(Application.loadedLevel);
        } else
        {
          EndGame("WIN", 0);
        }
    }


    public void EndGame (string state, float delay)
    {
        if (state == "DEATH")
        {
            playerDied = true;
            PlayerPrefs.SetInt("GameScore", 0);
            master.scorer.Death();
        }

        PlayerPrefs.SetString("GameEndState", state);
        Debug.Log("next scene " + delay);
        StartCoroutine(LoadNextScene(state, delay));
    }

    IEnumerator LoadNextScene(string state, float delay)
    {
        Debug.Log("NEEEXT");
        yield return new WaitForSeconds(delay);
        Debug.Log("NEEEXT!!!");
        if (state == "DEATH")
        {
            blackFader.enabled = true;
        }
        Application.LoadLevel(endScene);
    }


    public void LoadMods()
    {
        string modList = PlayerPrefs.HasKey("ModList") ? PlayerPrefs.GetString("ModList") : "";
        if (modList == "") return;
        foreach (string modNum in modList.Split(','))
        {
            if (modNum != "")
            {
                master.modifiers.AddMod((Modifiers.Modifier)int.Parse(modNum));
            }
            
        }
    }

    public void SaveMods ()
    {
        string modList = "";
        List<Modifiers.Modifier> mods = master.modifiers.mods;
        foreach (Modifiers.Modifier mod in mods)
        {
            modList += (int)mod + ",";
        }
        Debug.Log("MODLIST: " + modList);
        PlayerPrefs.SetString("ModList", modList);
    }

    public int GetLevel ()
    {
        return PlayerPrefs.HasKey("LevelLoad") ? PlayerPrefs.GetInt("LevelLoad") : 1;
    }

    public int GetAllies()
    {
        return PlayerPrefs.HasKey("Allies") ? PlayerPrefs.GetInt("Allies") : 0;
    }
}
