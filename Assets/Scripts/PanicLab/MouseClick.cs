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
                CheckPositionSet();
            }
            if (gameObject.name == "exitMenu")
            {
                GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(0, 0, -100);
                SceneController.ScoringExit();
            }
        }
        else if(gameObject.tag == "levels")
        {
            Static.wait = 1f;
            Static.gamesCount = 0;
            Static.score = 0;
            Static.myScore=0;
            GameObject check = GameObject.FindGameObjectWithTag("check");

            switch (gameObject.name)
            {
                case "level1":
                    Static.level = 1;
                    check.transform.position = new Vector3(12.98f, -3.86f, -2f);
                    break;
                case "level2":
                    Static.level = 2;
                    Static.wait -=0.15f;
                    check.transform.position = new Vector3(20.47f, -3.87f, -2f);
                    break;
                case "level3":
                    Static.level = 3;
                    Static.wait -= 0.3f;
                    check.transform.position = new Vector3(16.57f, -2.58f, -2f);
                    break;
                case "level4":
                    Static.level = 4;
                    Static.wait -=0.45f;
                    check.transform.position = new Vector3(13.83f, -0.95f, -2f);
                    break;
                case "level5":
                    Static.level = 5;
                    Static.wait -= 0.6f;
                    check.transform.position = new Vector3(19.18f, -0.18f, -2f);
                    break;
                case "level6":
                    Static.level = 6;
                    Static.wait -= 0.75f;
                    check.transform.position = new Vector3(16.07f, 1.15f, -2f);
                    break;
                case "level7":
                    Static.level = 7;
                    Static.wait -= 0.9f;
                    check.transform.position = new Vector3(13.36f, 3.15f, -2f);
                    break;
            }
            
            StartCoroutine("Pulse");
        }
    }

    private IEnumerator Pulse()
    {
        float wait = 0.03f;
        Vector3 scale = gameObject.transform.localScale;
        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.localScale = new Vector3(scale.x * 0.5f, scale.y * 0.5f, scale.z * 0.5f);
            yield return new WaitForSeconds(wait);
            gameObject.transform.localScale = scale;
            yield return new WaitForSeconds(wait);
        }

    }

    private void CheckPositionSet()
    {
        GameObject check = GameObject.FindGameObjectWithTag("check");

        switch (Static.level)
        {
            case 1:
                check.transform.position = new Vector3(12.98f, -3.86f, -2f);
                break;
            case 2:
                check.transform.position = new Vector3(20.47f, -3.87f, -2f);
                break;
            case 3:
                check.transform.position = new Vector3(16.57f, -2.58f, -2f);
                break;
            case 4:
                check.transform.position = new Vector3(13.83f, -0.95f, -2f);
                break;
            case 5:
                check.transform.position = new Vector3(19.18f, -0.18f, -2f);
                break;
            case 6:
                check.transform.position = new Vector3(16.4f, 1.4f, -2f);
                break;
            case 7:
                check.transform.position = new Vector3(13.36f, 3.15f, -2f);
                break;
        }
    }
}
