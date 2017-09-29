using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameHelper : MonoBehaviour {

    public InputField input;
    public Text textBlock;
    private PlayerHelper _currentPlayer;
    private SceneController_M _sceneController;

    [SerializeField] private TextMesh[] PlayersLabels;
    [SerializeField] private TextMesh[] PlayersScoreLabels;
    [SerializeField] private TextMesh[] PlayersReadyLabels;

    [SerializeField] private TextMesh countdown;



    public PlayerHelper currentPlayer { get { return _currentPlayer; } set { _currentPlayer = value; } }

	void Start () {

        _sceneController = GameObject.FindObjectOfType<SceneController_M>();
        StartCoroutine(LabelsFill());
        StartCoroutine(InitID());
    }

    private void Update()
    {
        if (Static_M.server && Static_M.go)
        {
            if (Static_M.numOfPlayers == Static_M.numOfPlayersReady)//автоматически бросит кости когда все готовы.
            {
                   _currentPlayer.RpcCountDown();
                    Static_M.go = false;
            }
        }
    }



    //chat-----------------------
    public void Send()
    {
        _currentPlayer.Send(input.text);
    }
 //chat----------------------


    public void PlayerLabelFill(int id, string name)
    {
        PlayersLabels[id].text = name;
        PlayersScoreLabels[id].text = "";
    }
    public void PlayerReadyFill(int id, bool empty)
    {
        if (empty)
        {
            PlayersReadyLabels[id].text = "";
            Static_M.numOfPlayersReady = 0;
        }
        else
        PlayersReadyLabels[id].text = "Ready";

        if (Static_M.server)
        {
            Static_M.go = true;
        }
    }
    public void PlayerScoreFill(int id)
    {
        Static_M.isCardButtonsActive = false;//деактивирует нажатие на карточки.
        Static_M.PlayersScore[id]++;
        if (Static_M.playerId==id)
        {
            _sceneController.StartCoroutine("Rotation");
        }
        else
        {
                _sceneController.StartCoroutine("Pulse");
        }
        PlayersScoreLabels[id].text = Static_M.PlayersScore[id].ToString();
    }

    public void CountDown()
    {
        StartCoroutine(Countdown());
    }
    public void ThrowDices()
    {
        _sceneController.ThrowingDice();
    }
    public void SetDicesOnClient(int[]diceIndexes)
    {
        if (!Static_M.server)
        {
            for (int i = 0; i < Static_M.DiceIndexes.Length; i++)
            {
                Static_M.DiceIndexes[i] = diceIndexes[i];
                Debug.Log("Static_M.DiceIndexes"+i+"  = " + Static_M.DiceIndexes[i]);
            }
            ThrowDices();
        }
    }
    public void SetSearchDataOnClient(int id)
    {
        if (!Static_M.server)
        {
            Static_M.id = id;
        }
    }
    public void SendSearchDataOnClient()
    {
        _currentPlayer.RpcSetSearchDataOnClients(Static_M.id);
    }
    public void SetGoogAnswer()
    {
        _currentPlayer.CmdSetGoodAnswer();
    }




    public void SetDeactiveCanvas()// метод на кнопку старт.
    {
        _currentPlayer.CmdPlayersLabelsFill();

        GameObject.FindGameObjectWithTag("canvas").SetActive(false);
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(0, 0, -100);
    }
    IEnumerator LabelsFill()
    {
        bool b = true;
        while (b)
        {
            if (_currentPlayer != null)
            {
                _currentPlayer.CmdPlayersLabelsFill();
                b = false;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    IEnumerator LabelsReadyFill(bool empty)
    {
        bool b = true;
        while (b)
        {
            if (_currentPlayer != null)
            {
                _currentPlayer.CmdPlayerReadyFill(empty);
                b = false;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    IEnumerator Countdown( )
    {
        for (int i = 3; i > 0; i--)
        {
            countdown.text = i.ToString();
            yield return new WaitForSeconds(0.5f);
            countdown.text = "";
        }
        _currentPlayer.CmdPlayerReadyFill(true);
        if (Static_M.server)
        {
            ThrowDices();
            _currentPlayer.RpcSetDicesOnClients(Static_M.DiceIndexes);//рассылаем клиентам значения костей.
        }
    }
    IEnumerator InitID()
    {
        bool b = true;
        while (b)
        {
            if (_currentPlayer != null)
            {
                Static_M.playerId = _currentPlayer.playerId;
                b = false;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }


}
