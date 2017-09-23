using UnityEngine; using System; using Prototype.NetworkLobby; using System.Collections; using UnityEngine.Networking;  public class NetworkLobbyHookMy : LobbyHook {         public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)     {         LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();         PlayerHelper player = gamePlayer.GetComponent<PlayerHelper>();          player.playerId = Static_M.numOfPlayers++;// статическое поле заполняется только на сервере.
        if (lobby.nameInput.text.Length > 12)
        {
            player.playerName = lobby.nameInput.text.Substring(0, 12);// задаем имя игрока из лобби и обрезаем его до 12 символов.
        }
        else
        {
            player.playerName = lobby.nameInput.text;
        }

    } } 