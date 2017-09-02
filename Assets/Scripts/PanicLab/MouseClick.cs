using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour {


    public void OnMouseDown()
    {



        if (Static.isCardButtonsActive && !(gameObject.name == "start_button") && !(gameObject.name == "menu") && !(gameObject.name == "exitMenu"))
        {
            Static.myId = gameObject.GetComponent<Card>().id;
            if (Static.myId == Static.id)
            {
                Static.goodAnswer = 1;
            }
            else
            {
                Static.goodAnswer = -1;
            }
            Static.isCardButtonsActive = false;
        }


        if (gameObject.name == "start_button" && Static.isStartButtonActive)
        {
            Static.throwDice = true;
            Static.isStartButtonActive = false;
            Static.isCardButtonsActive = true;//делаем карточки активными.
        }


       


        if (gameObject.name == "menu")
        {
            GameObject.FindGameObjectWithTag("MainCamera").transform.position=new Vector3(19.23f, 0, -100);
        }
        if (gameObject.name == "exitMenu")
        {
            GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(0, 0, -100);

        }


    }
}
