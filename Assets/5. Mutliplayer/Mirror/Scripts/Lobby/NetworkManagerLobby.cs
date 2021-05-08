using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

namespace MultiplayerMirror {
    public class NetworkManagerLobby : NetworkManager {
        [SerializeField] private int minPlayers = 2;
        [Scene] [SerializeField] private string menuScene = string.Empty;

        [Header("Room")]
        [SerializeField] private NetworkRoomPlayerLobby roomPlayerPrefab = null;
        public List<NetworkRoomPlayerLobby> RoomPlayers { get; } = new List<NetworkRoomPlayerLobby>();

        [Header("Game")]
        [SerializeField] private NetworkGamePlayer gamePlayerPrefab = null;
        public List<NetworkGamePlayer> GamePlayers { get; } = new List<NetworkGamePlayer>();


        public static Action OnClientConnected;
        public static Action OnClientDisconnected;

        public override void OnStartServer() {
            spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();
        }

        public override void OnStartClient() {
            var spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

            foreach (var prefab in spawnPrefabs) {
                NetworkClient.RegisterPrefab(prefab);
            }
        }

        public override void OnClientConnect(NetworkConnection conn) {
            base.OnClientConnect(conn);
            OnClientConnected?.Invoke();
        }

        public override void OnClientDisconnect(NetworkConnection conn) {
            base.OnClientConnect(conn);
            OnClientDisconnected?.Invoke();
        }

        public override void OnServerConnect(NetworkConnection conn) {
            if (numPlayers >= maxConnections) {
                conn.Disconnect();
                return;
            }
            if (SceneManager.GetActiveScene().path != menuScene) {
                conn.Disconnect();
                return;
            }
        }

        public override void OnServerAddPlayer(NetworkConnection conn) {
            if (SceneManager.GetActiveScene().path == menuScene) {
                bool isLeader = RoomPlayers.Count == 0;

                NetworkRoomPlayerLobby roomPlayerInstance = Instantiate(roomPlayerPrefab);

                roomPlayerInstance.IsLeader = isLeader;

                NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
            }
        }
        public override void OnServerDisconnect(NetworkConnection conn) {
            if (conn.identity != null) {
                var player = conn.identity.GetComponent<NetworkRoomPlayerLobby>();

                RoomPlayers.Remove(player);

                NotifyPlayersOfReadyState();
            }

            base.OnServerDisconnect(conn);
        }

        public void NotifyPlayersOfReadyState() {
            foreach (var player in RoomPlayers) {
                player.HandleReadyToStart(IsReadyToStart());
            }
            throw new NotImplementedException();
        }

        private bool IsReadyToStart() {
            if (numPlayers < minPlayers)
                return false;

            foreach (var player in RoomPlayers) {
                if (!player.IsReady)
                    return false;
            }

            return true;
        }

        public override void OnStopServer() {
            RoomPlayers.Clear();
        }

        #region Gameplay

        public void StartGame() {
            if (SceneManager.GetActiveScene().path == menuScene) {
                if (!IsReadyToStart())
                    return;

                ServerChangeScene("GameplayMultiplayerMirror");
            }
        }

        public override void ServerChangeScene(string newSceneName) {
            if (SceneManager.GetActiveScene().path == menuScene && newSceneName.StartsWith("GameplayMultiplayerMirror")) {
                for (int i = RoomPlayers.Count; i >= 0; i++) {
                    var conn = RoomPlayers[i].connectionToClient;
                    var gameplayerInstance = Instantiate(gamePlayerPrefab);
                    gameplayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);

                    NetworkServer.Destroy(conn.identity.gameObject);
                    NetworkServer.ReplacePlayerForConnection(conn, gameplayerInstance.gameObject);
                }
            }
            base.ServerChangeScene(newSceneName);
        }

        #endregion
    }
}
