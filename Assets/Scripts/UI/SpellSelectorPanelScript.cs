using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSelectorPanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> items = new List<string>();
    public int selectedItem;
    public bool inFocus;
    private GameObject selector;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(transform.localScale.x, items.Count * 60);
        if (Input.GetButtonUp("w") && selectedItem >=0)
        {
            selectedItem--;
        }
        else if (Input.GetButtonUp("d") && selectedItem < items.Count)
        {
            selectedItem++;
        }
        
    }

    public void CreatePanel(List<string> items, bool inFocus)
    {
        this.items = items;
        this.inFocus = inFocus;


        selectedItem = items.Count / 2;
    }
}
