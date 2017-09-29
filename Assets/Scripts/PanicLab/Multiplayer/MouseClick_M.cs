using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick_M : MonoBehaviour {

    //Звук
   [SerializeField] private AudioSource[] soundSources;
   [SerializeField] private AudioClip[] audioClips;
    //Звук

    private GameHelper _gameHelper;
    private SceneController_M _sceneController;

    private void Start()
    {
        _gameHelper = GameObject.FindObjectOfType<GameHelper>();
        _sceneController = GameObject.FindObjectOfType<SceneController_M>();
    }


    public void OnMouseDown()
    {

        if (gameObject.tag!="button" && gameObject.tag != "levels" && gameObject.tag != "dices" && Static_M.isCardButtonsActive )
        { //нажатие на карту во время игры.
            Static_M.myId = gameObject.GetComponent<Card_M>().id;
            if (Static_M.myId == Static_M.id)
            {
                _gameHelper.SetGoogAnswer();
            }
            else
            {
                _sceneController.StartCoroutine("QuickPulse");
            }
        }

        else if(gameObject.tag != "levels")
        {
            if (gameObject.name == "start_button" && Static_M.isStartButtonActive)
            {               //нажатие на кнопку старт.
                _gameHelper.StartCoroutine("LabelsReadyFill",false);// заполняются поля готовности игроков.

               // Static_M.throwDice = true;
                Static_M.isStartButtonActive = false;
                Static_M.isCardButtonsActive = true;//делаем карточки активными.
            }

            if (gameObject.name == "levelsEnterButton")
            {
                soundSources[0].PlayOneShot(audioClips[0]);//звуковой эффект.
                GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(19.23f, 0, -100);
                CheckPositionSet();
            }
            if (gameObject.name == "exitMenu")
            {
                GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(0, 0, -100);
                SceneController_M.ScoringExit();
            }
        }
        else if(gameObject.tag == "levels")
        {
            Static_M.wait = 1f;
            Static_M.gamesCount = 0;
            Static_M.score = 0;
            Static_M.myScore=0;
            GameObject check = GameObject.FindGameObjectWithTag("check");

            switch (gameObject.name)
            {
                case "level1":
                    Static_M.level = 1;
                    check.transform.position = new Vector3(12.98f, -3.86f, -2f);
                    break;
                case "level2":
                    Static_M.level = 2;
                    Static_M.wait -=0.15f;
                    check.transform.position = new Vector3(20.47f, -3.87f, -2f);
                    break;
                case "level3":
                    Static_M.level = 3;
                    Static_M.wait -= 0.3f;
                    check.transform.position = new Vector3(16.57f, -2.58f, -2f);
                    break;
                case "level4":
                    Static_M.level = 4;
                    Static_M.wait -=0.45f;
                    check.transform.position = new Vector3(13.83f, -0.95f, -2f);
                    break;
                case "level5":
                    Static_M.level = 5;
                    Static_M.wait -= 0.6f;
                    check.transform.position = new Vector3(19.18f, -0.18f, -2f);
                    break;
                case "level6":
                    Static_M.level = 6;
                    Static_M.wait -= 0.75f;
                    check.transform.position = new Vector3(16.07f, 1.15f, -2f);
                    break;
                case "level7":
                    Static_M.level = 7;
                    Static_M.wait -= 0.9f;
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

        switch (Static_M.level)
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
