using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour {

public void setActive()
    {
        if (Static.setCount == 3)
        {
            GameObject.Find("CARDS_1").SetActive(false);
        }
        else if(Static.setCount == 6)
        {
            GameObject.Find("CARDS_0").SetActive(false);
        }

        Static.setCount++;
    }

}
