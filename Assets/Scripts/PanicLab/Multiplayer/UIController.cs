using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {


    public void CanvasOnOff(bool b)
    {
        if (b)
        {
            gameObject.GetComponent<Canvas>().enabled=true;
        }
        else
            gameObject.GetComponent<Canvas>().enabled = false;
    }
}
