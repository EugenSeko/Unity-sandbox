using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHelper : MonoBehaviour {

    public InputField input;
    public Text textBlock;
    private PlayerHelper _currentPlayer;

    [SerializeField] private TextMesh[] PlayersLabels;
    [SerializeField] private TextMesh[] PlayersScoreLabels;



    public PlayerHelper currentPlayer { get { return _currentPlayer; } set { _currentPlayer = value; } }

	void Start () {
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
        PlayersScoreLabels[id].text = id.ToString();
    }




    public void SetDeactiveCanvas()// метод на кнопку старт.
    {
        _currentPlayer.CmdPlayersLabelsFill();

        GameObject.FindGameObjectWithTag("canvas").SetActive(false);
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(0, 0, -100);
    }
}
