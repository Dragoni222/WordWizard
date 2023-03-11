using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellList : MonoBehaviour
{
    public string[] elementStarters = { "FIRE", "WATER", "EARTH", "AIR", "LIGHT", "DARK"};
    public string[] fire = {
        "SUMMON HELLFIRE TO MY HAND AND LET IT BURN MY ENEMIES",
        "BLESS THIS MAGIC WITH THE BURNING MAWS OF THE FIRE DRAGONS" , 
        "LET THIS CREATION BURN WITH THE HEAT OF A THOUSAND HEARTHS"};
    public string[] water = { "W1", "W2", "W3" };
    public string[] earth = { "E1", "E2", "E3" };
    public string[] air = { "A1", "A2", "A3" };
    public string[] holy = { "H1", "H2", "H3" };
    public string[] dark = { "D1", "D2", "D3" };
    public string[,] incants;
                                    

    private void Start()
    {
        incants = new string[,] { 
            { fire[0] , fire[1] , fire[2] }, 
            { water[0], water[1], water[2] }, 
            { earth[0], earth[1], earth[2] }, 
            { air[0]  , air[1]  , air[2] }, 
            { holy[0] , holy[1] , holy[2] }, 
            { dark[0] , dark[1] , dark[2] }, };


    }

}




public class Spell
{
    public string phrase1;
    public string phrase2;
    public string phrase3;
    public float power1;
    public float power2;
    public float power3;
    public int playerNum;

    public Spell(string phrase1, float power1, string phrase2, float power2,string phrase3, float power3, int playerNum)
    {
        this.phrase1 = phrase1;
        this.power1 = power1;

        this.phrase2 = phrase2;
        this.power2 = power2;

        this.phrase3 = phrase3;
        this.power3 = power3;

        this.playerNum = playerNum;

    }

    public Spell(int playerNum)
    {
        this.playerNum = playerNum;
    }
}



