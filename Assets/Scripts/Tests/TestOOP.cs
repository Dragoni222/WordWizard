using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOOP : OOPMonobehavior
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<OOPMonobehavior>().testVar += 1;
        Debug.Log(gameObject.GetComponent<OOPMonobehavior>().testVar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
