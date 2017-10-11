using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameHelper : MonoBehaviour {

    public InputField input;
    public Text textBlock;
    private PlayerHelper _currentPlayer;
    private SceneController_M _sceneController;
    private UIController _uiController;

    [SerializeField] private TextMesh[] PlayersLabels;
    [SerializeField] private TextMesh[] PlayersScoreLabels;
    [SerializeField] private TextMesh[] PlayersReadyLabels;
    [SerializeField] private Text[] PlayersRate;
    [SerializeField] private Text[] PlayersRateScore;


    [SerializeField] private TextMesh countdown;



    public PlayerHelper currentPlayer { get { return _currentPlayer; } set { _currentPlayer = value; } }

	void Start () {

        _sceneController = GameObject.FindObjectOfType<SceneController_M>();
        _uiController = GameObject.FindObjectOfType<UIController>();
        StartCoroutine(LabelsFill());
        StartCoroutine(InitID());
        _uiController.CanvasOnOff(false);
        
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
        PlayersScoreLabels[id].text = Static_M.PlayersScore[id].ToString();//заполняет поле очков в игре.
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

    public void SendLoadScore()
    {
        _currentPlayer.CmdLoadScore();
    }
    public void LoadScores()//метод переключает камеру, включает канвас и выводит игровые достижения и обнуляет эти достижения.
    {
        _uiController.CanvasOnOff(true);
        Static_M.gamesCount = 0;
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(0, -11.06f, -100);
        Scoring();
        for (int i = 0; i < Static_M.PlayersScore.Length; i++)
        {
            Static_M.PlayersScore[i] = 0;
        }
    }
    private void Scoring()//метод выводит имена и очки игроков на экран в порядке набранных очков.
    {
        int ind = 0;

        for (int i = Static_M.numOfGames; i >-1; i--)
        {
            for (int j=0; j< Static_M.PlayersScore.Length; j++)
            {
                if (Static_M.PlayersScore[j] == i)
                {
                    PlayersRate[ind].text = PlayersLabels[j].text;
                    PlayersRateScore[ind].text = PlayersScoreLabels[j].text;//заполняет поле очков в чате.
                    ind++;
                    break;
                }
            }
        }
        

    }
    private void PlayerRateClean()
    {
        foreach (Text pr in PlayersRate)
        {
            pr.text = "";
        }
        foreach (Text pr in PlayersRateScore)
        {
            pr.text = "";
        }
    }//очищает данные игроков отображаемые в чате.
    private void PlayerScoreClean()
    {
        foreach (TextMesh ps in PlayersScoreLabels)
        {
            ps.text = "";
        }
    }//очищает данные игроков отображаемые в игре.



    public void SetDeactiveCanvas()// метод на кнопку старт.
    {
        PlayerRateClean();
        PlayerScoreClean();

        _currentPlayer.CmdPlayersLabelsFill();
        _uiController.CanvasOnOff(false);
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(0, -0.55f, -100);
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
        Static_M.isCardButtonsActive = true;//делаем карточки активными.
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
