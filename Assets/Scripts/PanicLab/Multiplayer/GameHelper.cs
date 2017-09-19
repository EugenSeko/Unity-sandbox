using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHelper : MonoBehaviour {

    public InputField input;
    public Text textBlock;
    private PlayerHelper _currentPlayer;

    public PlayerHelper currentPlayer { get { return _currentPlayer; } set { _currentPlayer = value; } }

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void Send()
    {
        _currentPlayer.Send(input.text);
    }
    public void SetDeactiveCanvas()
    {
        GameObject.FindGameObjectWithTag("canvas").SetActive(false);
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(0, 0, -100);
        if (_currentPlayer.isServer)
        {
            Debug.Log("SERVER");
        }
    }
}
