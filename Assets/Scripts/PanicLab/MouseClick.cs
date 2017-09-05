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
                SceneController.ScoringExit();
            }
        }
        else if(gameObject.tag == "levels")
        {
            Static.wait = 0.4f;
            Static.gamesCount = 0;
            Static.score = 0;
            Static.myScore=0;

            switch (gameObject.name)
            {
                case "level1":
                    Static.level = 1;
                    break;
                case "level2":
                    Static.level = 2;
                    Static.wait -=0.15f;
                    break;
                case "level3":
                    Static.level = 3;
                    Static.wait -= 0.3f;
                    break;
                case "level4":
                    Static.level = 4;
                    Static.wait -=0.45f;
                    break;
                case "level5":
                    Static.level = 5;
                    Static.wait -= 0.6f;
                    break;
                case "level6":
                    Static.level = 6;
                    Static.wait -= 0.75f;
                    break;
                case "level7":
                    Static.level = 7;
                    Static.wait -= 0.9f;
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
