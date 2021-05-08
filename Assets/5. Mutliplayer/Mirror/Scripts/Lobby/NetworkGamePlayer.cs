using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;
using TMPro;

namespace MultiplayerMirror
{
    public class NetworkGamePlayer : NetworkBehaviour {

        [SyncVar]
        private string displayName = "Loading...";

        private NetworkManagerLobby room;
        private NetworkManagerLobby Room{
            get{
                if(room != null)
                    return room;
                return room = NetworkManager.singleton as NetworkManagerLobby;
            }
        }



        public override void OnStartClient() {
            DontDestroyOnLoad(gameObject);
            Room.GamePlayers.Add(this);
        }

        public override void OnStopClient() {
            Room.GamePlayers.Remove(this);
        }

        [Server]
        public void SetDisplayName(string displayName){
            this.displayName = displayName;
        }

        
    }
}
