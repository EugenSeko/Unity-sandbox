using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour {


    public void OnMouseDown()
    {
        

        if (gameObject.name == "start_button")
        {
            Static.throwDice = true;
            Static.setCount++;
        }
        else if(Static.isActive)
        {
            Debug.Log("card id     " + gameObject.GetComponent<Card>().id);
            Debug.Log("obj name     " + gameObject.name);
        }

    }
}
