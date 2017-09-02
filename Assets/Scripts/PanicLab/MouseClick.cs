using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour {


    public void OnMouseDown()
    {

        if (gameObject.tag!="button" && gameObject.tag != "levels" && gameObject.tag != "dices" && Static.isCardButtonsActive )
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
        else if(gameObject.tag != "levels")
        {
            if (gameObject.name == "start_button" && Static.isStartButtonActive)
            {
                Static.throwDice = true;
                Static.isStartButtonActive = false;
                Static.isCardButtonsActive = true;//делаем карточки активными.
            }

            if (gameObject.name == "menu")
            {
                GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(19.23f, 0, -100);
            }
            if (gameObject.name == "exitMenu")
            {
                GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(0, 0, -100);

            }
        }
        else if(gameObject.tag == "levels")
        {
            Static.wait = 0.4f;
            Static.gamesCount = 0;
            Static.score = 0;
            Static.myScore=0;
            Static.gamesCount = 0;

            switch (gameObject.name)
            {
                case "level1":
                    Debug.Log("lev1");
                    Static.wait +=0.3f;
                    break;
                case "level2":
                    Debug.Log("lev2");
                    Static.wait +=0.2f;
                    break;
                case "level3":
                    Debug.Log("lev3");
                    Static.wait = 0.4f;
                    break;
                case "level4":
                    Debug.Log("lev4");
                    Static.wait -=0.1f;
                    break;
                case "level5":
                    Debug.Log("lev5");
                    Static.wait -= 0.2f;
                    break;
                case "level6":
                    Debug.Log("lev6");
                    Static.wait -= 0.3f;
                    break;
                case "level7":
                    Debug.Log("lev7");
                    Static.wait -= 0.35f;
                    break;
            }

        }

       /* if (Static.isCardButtonsActive && !(gameObject.name == "start_button") && !(gameObject.name == "menu") && !(gameObject.name == "exitMenu"))
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

        }*/


    }
}
