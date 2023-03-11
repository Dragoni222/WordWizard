using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon;
using Unity.VisualScripting;
using System;
using System.IO;
using DG.Tweening;
public class PlayerSpellcast : Photon.MonoBehaviour
{

    public SpellSlot currentSpellSlot;

    [SerializeField] TextMeshProUGUI spell1Slot;
    [SerializeField] TextMeshProUGUI spell1Scroll;
    [SerializeField] TextMeshProUGUI spell1Phrase1;
    [SerializeField] TextMeshProUGUI spell1Phrase2;
    [SerializeField] TextMeshProUGUI spell1Phrase3;

    [SerializeField] TextMeshProUGUI spell2Slot;
    [SerializeField] TextMeshProUGUI spell2Scroll;
    [SerializeField] TextMeshProUGUI spell2Phrase1;
    [SerializeField] TextMeshProUGUI spell2Phrase2;
    [SerializeField] TextMeshProUGUI spell2Phrase3;

    [SerializeField] TextMeshProUGUI spell3Slot;
    [SerializeField] TextMeshProUGUI spell3Scroll;
    [SerializeField] TextMeshProUGUI spell3Phrase1;
    [SerializeField] TextMeshProUGUI spell3Phrase2;
    [SerializeField] TextMeshProUGUI spell3Phrase3;

    public SpellSlot spell1;
    public SpellSlot spell2;
    public SpellSlot spell3;

    public PlayerStats otherPlayer;
    public PlayerStats thisPlayer;

    [SerializeField] SpellFactory spellFactory;

    PhotonView view;

    private SpellList spells;

    void Start()
    {
        view = gameObject.GetPhotonView();

        if(thisPlayer.playernum == 2 && view.isMine && PhotonNetwork.isMasterClient)
        {
            view.TransferOwnership(PhotonNetwork.otherPlayers[0]);
        }

        spells = GameObject.Find("Spells").GetComponent<SpellList>();
        spell1 = new SpellSlot(spell1Slot, spell1Scroll, spell1Phrase1, spell1Phrase2, spell1Phrase3, thisPlayer.playernum);
        spell2 = new SpellSlot(spell2Slot, spell2Scroll, spell2Phrase1, spell2Phrase2, spell2Phrase3, thisPlayer.playernum);
        spell3 = new SpellSlot(spell3Slot, spell3Scroll, spell3Phrase1, spell3Phrase2, spell3Phrase3, thisPlayer.playernum);
        currentSpellSlot = spell1;


    }

    public class SpellSlot
    {
        public TextMeshProUGUI spellTextSlot;
        public TextMeshProUGUI spellScroll;
        public TextMeshProUGUI spellPhrase1;
        public TextMeshProUGUI spellPhrase2;
        public TextMeshProUGUI spellPhrase3;

        public string currentText;
        public Spell currentSpell;
        public List<char> currentSpellList;

        public bool phrase1finished;
        public bool phrase2finished;
        private int playerNum;


        public SpellSlot(TextMeshProUGUI SpellTextSlot, TextMeshProUGUI SpellScroll,
            TextMeshProUGUI SpellPhrase1, TextMeshProUGUI SpellPhrase2, TextMeshProUGUI SpellPhrase3, int PlayerNum)
        {


            this.spellTextSlot = SpellTextSlot;
            this.spellScroll = SpellScroll;
            this.spellPhrase1 = SpellPhrase1;
            this.spellPhrase2 = SpellPhrase2;
            this.spellPhrase3 = SpellPhrase3;

            currentSpellList = new List<char>();
            currentText = new string(currentSpellList.ToArray());

            phrase1finished = false;
            phrase2finished = false;

            playerNum = PlayerNum;

            currentSpell = new Spell(PlayerNum);

            spellPhrase1.text = "";
            spellPhrase2.text = "";
            spellScroll.text = "";
            spellTextSlot.text = "";
        }


        public void EraseSpell()
        {
            currentSpellList = new List<char>();
            currentText = new string(currentSpellList.ToArray());

            phrase1finished = false;
            phrase2finished = false;

            currentSpell = new Spell(playerNum);

            spellPhrase1.text = "";
            spellPhrase2.text = "";
            spellScroll.text = "";
            spellTextSlot.text = "";
        }
        
        public void SetPhrase(int phraseNum, string starter, SpellList spells)
        {
            if(phraseNum == 1)
                currentSpell.phrase1 = starter;
            else if(phraseNum == 2)
                currentSpell.phrase2 = starter;
            else
                currentSpell.phrase3 = starter;

            string spellPhrase = spells.incants[Array.IndexOf(spells.elementStarters, starter), phraseNum - 1];
            spellScroll.text = spellPhrase;
            currentSpellList = new List<char>();
            currentText = new string(currentSpellList.ToArray());
            spellTextSlot.text = "";
        }

    }


    public void RotateSpells()
    {
        if(currentSpellSlot == spell1)
        {
            DOTween.CompleteAll();
            spell1.spellTextSlot.transform.parent.localPosition = new Vector3(0, 0, 0);
            spell1.spellTextSlot.transform.parent.DOLocalMoveX(2000, 0.3f);
            spell2.spellTextSlot.transform.parent.localPosition = new Vector3(-2000, 0, 0);
            spell2.spellTextSlot.transform.parent.DOLocalMoveX(0, 0.3f);
            spell3.spellTextSlot.transform.parent.localPosition = new Vector3(-2000, 0, 0);
            currentSpellSlot = spell2;
        }
        else if (currentSpellSlot == spell2)
        {
            DOTween.CompleteAll();
            spell2.spellTextSlot.transform.parent.localPosition = new Vector3(0, 0, 0);
            spell2.spellTextSlot.transform.parent.DOLocalMoveX(2000, 0.3f);
            spell3.spellTextSlot.transform.parent.localPosition = new Vector3(-2000, 0, 0);
            spell3.spellTextSlot.transform.parent.DOLocalMoveX(0, 0.3f);
            spell1.spellTextSlot.transform.parent.localPosition = new Vector3(-2000, 0, 0);
            currentSpellSlot = spell3;

        }
        else if (currentSpellSlot == spell3)
        {
            DOTween.CompleteAll();
            spell3.spellTextSlot.transform.parent.localPosition = new Vector3(0, 0, 0);
            spell3.spellTextSlot.transform.parent.DOLocalMoveX(2000, 0.3f);
            spell1.spellTextSlot.transform.parent.localPosition = new Vector3(-2000, 0, 0);
            spell1.spellTextSlot.transform.parent.DOLocalMoveX(0, 0.3f);
            spell2.spellTextSlot.transform.parent.localPosition = new Vector3(-2000, 0, 0);
            currentSpellSlot = spell1;
        }
    }

    void Update()
    {
        
        //inputs
        if (view.isMine)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(vKey))
                    {
                        if (vKey == KeyCode.Backspace && currentSpellSlot.currentSpellList.Count >= 1)
                        {
                            currentSpellSlot.currentSpellList.RemoveAt(currentSpellSlot.currentSpellList.Count - 1);
                        }
                        else if(vKey == KeyCode.Space)
                        {
                            currentSpellSlot.currentSpellList.Add(' ');
                        }
                        else if(vKey == KeyCode.Tab)
                        {
                            RotateSpells();
                        }
                        else if(vKey == KeyCode.Alpha1)
                        {
                            currentSpellSlot.currentSpellList.Add('1');
                        }
                        else if (vKey == KeyCode.Alpha2)
                        {
                            currentSpellSlot.currentSpellList.Add('2');
                        }
                        else if (vKey == KeyCode.Alpha3)
                        {
                            currentSpellSlot.currentSpellList.Add('3');
                        }
                        else if (vKey == KeyCode.Alpha4)
                        {
                            currentSpellSlot.currentSpellList.Add('4');
                        }
                        else if (vKey == KeyCode.Alpha5)
                        {
                            currentSpellSlot.currentSpellList.Add('5');
                        }
                        else if (vKey == KeyCode.Alpha6)
                        {
                            currentSpellSlot.currentSpellList.Add('6');
                        }
                        else if (vKey == KeyCode.Alpha7)
                        {
                            currentSpellSlot.currentSpellList.Add('7');
                        }
                        else if (vKey == KeyCode.Alpha8)
                        {
                            currentSpellSlot.currentSpellList.Add('8');
                        }
                        else if (vKey == KeyCode.Alpha9)
                        {
                            currentSpellSlot.currentSpellList.Add('8');
                        }
                        else if (vKey == KeyCode.Alpha0)
                        {
                            currentSpellSlot.currentSpellList.Add('9');
                        }
                        else if (vKey == KeyCode.Alpha1)
                        {
                            currentSpellSlot.currentSpellList.Add('0');
                        }
                        else
                            currentSpellSlot.currentSpellList.Add(vKey.ToString()[0]);



                    }
                }
            }
            
            //checks which scroll to pull up and assigns the element to the spell
            foreach(string starter in spells.elementStarters)
            {
                if(currentSpellSlot.currentText == starter + " BASE" && !currentSpellSlot.phrase1finished)
                {
                    currentSpellSlot.SetPhrase(1, starter, spells);

                }
                else if (currentSpellSlot.currentText == starter + " BLESS" && !currentSpellSlot.phrase2finished)
                {
                    currentSpellSlot.SetPhrase(2, starter, spells);
                }
                else if (currentSpellSlot.currentText == starter + " ENCHANT" && currentSpellSlot.phrase2finished)
                {
                    currentSpellSlot.SetPhrase(3, starter, spells);
                }
            }

            //if we have started a spell
            if(currentSpellSlot.currentSpell.phrase1 != null)
            {
                

                //started 1st phrase
                if (!currentSpellSlot.phrase1finished)
                {
                    //the current correct spell incantation
                    string correctSpell = spells.incants[Array.IndexOf(spells.elementStarters, currentSpellSlot.currentSpell.phrase1), 0];

                    //finished 1st phrase
                    if (currentSpellSlot.currentText.Length == correctSpell.Length)
                    {
                        currentSpellSlot.currentSpell.power1 = CalculateSpellPower(correctSpell, currentSpellSlot.currentText);
                        currentSpellSlot.currentSpellList = new List<char>();
                        currentSpellSlot.spellPhrase1.text = currentSpellSlot.currentText;
                        currentSpellSlot.spellScroll.text = "";
                        currentSpellSlot.phrase1finished = true;
                        
                    }
                }
                //started 2nd phrase
                else if (!currentSpellSlot.phrase2finished && currentSpellSlot.currentSpell.phrase2 != null)
                {
                    //the current correct spell incantation
                    string correctSpell = spells.incants[Array.IndexOf(spells.elementStarters, currentSpellSlot.currentSpell.phrase2), 1];

                    //finished 2nd phrase
                    if (currentSpellSlot.currentText.Length == correctSpell.Length)
                    {
                        currentSpellSlot.currentSpell.power2 = CalculateSpellPower(correctSpell, currentSpellSlot.currentText);
                        currentSpellSlot.currentSpellList = new List<char>();
                        currentSpellSlot.spellPhrase2.text = currentSpellSlot.currentText;
                        currentSpellSlot.spellScroll.text = "";
                        currentSpellSlot.phrase2finished = true;
                    }
                }
                //started 3rd phrase
                else if(PhotonNetwork.isMasterClient && currentSpellSlot.currentSpell.phrase3 != null)
                {
                    //the current correct spell incantation
                    string correctSpell = spells.incants[Array.IndexOf(spells.elementStarters, currentSpellSlot.currentSpell.phrase3), 2];

                    //finished 3rd phrase
                    if (currentSpellSlot.currentText.Length == correctSpell.Length)
                    {
                        currentSpellSlot.currentSpell.power3 = CalculateSpellPower(correctSpell, currentSpellSlot.currentText);
                        SendSpell(currentSpellSlot.currentSpell, null);
                    }
                }

                

            }
            currentSpellSlot.currentText = new string(currentSpellSlot.currentSpellList.ToArray());
            currentSpellSlot.spellTextSlot.text = currentSpellSlot.currentText;

        }

        

    }


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting && !PhotonNetwork.isMasterClient)
        {
            if(currentSpellSlot.phrase2finished)
            {
                //the current correct spell incantation
                string correctSpell = spells.incants[Array.IndexOf(spells.elementStarters, currentSpellSlot.currentSpell.phrase3), 2];

                //finished 3rd phrase
                if (currentSpellSlot.currentText.Length == correctSpell.Length)
                {
                    currentSpellSlot.currentSpell.power3 = CalculateSpellPower(correctSpell, currentSpellSlot.currentText);
                    SendSpell(currentSpellSlot.currentSpell, stream);
                    
                }
            }
            
        }
        else if(!stream.isWriting)
        {
            if(PhotonNetwork.isMasterClient)
            {
                spellFactory.spellsToMake.Add(currentSpellSlot.currentSpell);
            }
        }
    }

    private void SendSpell(Spell spellToSend, PhotonStream stream)
    {
        if (PhotonNetwork.isMasterClient)
        {
            spellFactory.spellsToMake.Add(spellToSend);
        }
        else
        {
            stream.SendNext(SpellToArray(spellToSend));
            
        }
        currentSpellSlot.EraseSpell();
    }

    private string[] SpellToArray(Spell spell)
    {
        return new string[] {spell.phrase1,spell.power1.ToString(), spell.phrase2, 
            spell.power2.ToString(), spell.phrase3, spell.power3.ToString(),
            spell.playerNum.ToString() };
    }
    private Spell ArrayToSpell(string[] spell)
    {
        return new Spell(spell[0],float.Parse(spell[1]), spell[2], float.Parse(spell[3]), spell[4],float.Parse(spell[5]), Int32.Parse(spell[6])   );
    }

    private float CalculateSpellPower(string original, string input)
    {
        float output = 0;
        if(original.Length == input.Length)
        {
            for(int i = 0; i < original.Length; i++)
            {
                if(original[i] == input[i])
                {
                    output++;
                }
            }
            //output is a multiplier
            return output / original.Length;
        }
        else
        {
            return 0;
        }
    }

    



}
