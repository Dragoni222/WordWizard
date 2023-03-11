using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Reflection;
using System;

public class SpellFactory : MonoBehaviour
{
    

    public List<Spell> spellsToMake = new List<Spell>();
    void Update()
    {

        while(spellsToMake.Count > 0)
        {
            for(int i = 0; i < spellsToMake.Count; i++)
            {
                if (spellsToMake[i] == null)
                {
                    spellsToMake.RemoveAt(i);
                }
            }
            if(spellsToMake.Count == 0)
            {
                return;
            }
            Vector2 spawnPoint = Vector2.zero;
            if (spellsToMake[spellsToMake.Count - 1].playerNum == 1)
            {
                spawnPoint = new Vector2(-6, 2);
            }
            else
            {
                spawnPoint = new Vector2(6, 2);
            }
            GameObject spell = PhotonNetwork.Instantiate("Magic",spawnPoint, Quaternion.identity, 0);

            spell.AddComponent(Type.GetType(spellsToMake[0].phrase1 + spellsToMake[0].phrase2));
            spell.GetComponent<Magic>().spell = spellsToMake[0];
            spellsToMake[0] = null;
            


        }
    }
}
