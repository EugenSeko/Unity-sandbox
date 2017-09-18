using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive_M : MonoBehaviour {

public void setActive()
    {
        if (Static_M.gamesCount == 3)
        {
            GameObject.Find("CARDS_1").SetActive(false);
        }
        else if(Static_M.gamesCount == 6)
        {
            GameObject.Find("CARDS_0").SetActive(false);
        }

        Static_M.gamesCount++;
    }

}
