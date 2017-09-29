using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class PlayerHelper : NetworkBehaviour {

    private GameHelper _gameHelper;

    [SyncVar]
    public string playerName;
    [SyncVar]
    public int playerId;


    // Use this for initialization
    void Start () {
        _gameHelper = GameObject.FindObjectOfType<GameHelper>();

        if (isLocalPlayer)
        {
            _gameHelper.currentPlayer = this;//передача ссылки в свойство currentPlayer на этот объект.
        }
    }


    //chat--------------------------------
    public void Send(string message)
    {
       // CmdSend(Network.player.guid, message);
        CmdSend(playerName+"  "+playerId, message);
    }
    [Command]
    public void CmdSend(string id, string message)
    {
        int rand = UnityEngine.Random.Range(0, 100);

        RpcSend(id, message, rand);
    }
    [ClientRpc]
    public void RpcSend(string id, string message, int random)
    {
        _gameHelper.textBlock.text += System.Environment.NewLine +id+"<<"+message+">>"+"/"+random;
    }
    //chat-----------------------------------

    [Command]
    public void CmdPlayersLabelsFill()
    {
        RpcPlayersLabelsFill(playerId, playerName);
    }
    [Command]
    public void CmdPlayerReadyFill(bool empty)
    {
        RpcPlayerReadyFill(playerId, empty);
        Static_M.numOfPlayersReady++;
    }
    [Command]
    public void CmdSetGoodAnswer() {

        RpcSendGoodAnswer(playerId);
    }

    [ClientRpc]
    public void RpcPlayersLabelsFill(int id, string name)
    {
        _gameHelper.PlayerLabelFill(id, name);
    }
    [ClientRpc]
    public void RpcPlayerReadyFill(int id,bool empty)
    {
        _gameHelper.PlayerReadyFill(id,empty);
    }
    [ClientRpc]
    public void RpcCountDown()
    {
        _gameHelper.CountDown();
    }
    [ClientRpc]
    public void RpcSetDicesOnClients(int[] diceIndexes)
    {
        _gameHelper.SetDicesOnClient(diceIndexes );
    }
    [ClientRpc]
    public void RpcSetSearchDataOnClients(int id)
    {
        _gameHelper.SetSearchDataOnClient(id);
    }
    [ClientRpc]
    public void RpcSendGoodAnswer(int id)
    {
        _gameHelper.PlayerScoreFill(id);
    }
}
