using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHelper : NetworkBehaviour {

    private GameHelper _gameHelper;

	// Use this for initialization
	void Start () {
        _gameHelper = GameObject.FindObjectOfType<GameHelper>();

        if (isLocalPlayer)
        {
            _gameHelper.currentPlayer = this;//передача ссылки в свойство currentPlayer на этот объект.
        }
	}

    public void Send(string message)
    {
        CmdSend(Network.player.guid, message);
    }

    [Command]
    public void CmdSend(string id, string message)
    {
        int rand = Random.Range(0, 100);

        Debug.Log("Message: " + message+"/"+id+"/"+rand);
        RpcSend(id, message, rand);
    }
    [ClientRpc]
    public void RpcSend(string id, string message, int random)
    {
        _gameHelper.textBlock.text += System.Environment.NewLine + "id:"+id+"<<"+message+">>"+"/"+random;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
