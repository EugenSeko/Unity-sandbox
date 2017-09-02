using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour {

public void setActive()
    {
        if (Static.gamesCount == 3)
        {
            GameObject.Find("CARDS_1").SetActive(false);
        }
        else if(Static.gamesCount == 6)
        {
            GameObject.Find("CARDS_0").SetActive(false);
        }

        Static.gamesCount++;
    }

}
