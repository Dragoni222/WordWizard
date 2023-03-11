using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerSpellcast;

public class SpellSelector : MonoBehaviour
{
    [SerializeField] SpellSlot spellSlot;
    List<SpellSelectorPanelScript> panels = new List<SpellSelectorPanelScript>();
    [SerializeField] GameObject panelPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spellSlot.currentSpell.phrase1 == null)
        {
            if(panels.Count == 0)
                panels.Add(Instantiate(panelPrefab, new Vector3(0,-250,0), Quaternion.identity, this.transform).GetComponent<SpellSelectorPanelScript>());
        }
        else
        {
            foreach(SpellSelectorPanelScript panel in panels)
            {
                Destroy(panel.gameObject);
            }
            panels.Clear();
        }
    }
}
