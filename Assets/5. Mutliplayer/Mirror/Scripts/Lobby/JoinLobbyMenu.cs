using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace MultiplayerMirror
{
    public class JoinLobbyMenu : MonoBehaviour
    {
        [SerializeField] private NetworkManagerLobby networkManager = null;
        
        [Header("UI")]
        [SerializeField] private GameObject landingPagePanel = null;
        [SerializeField] private TMP_InputField ipAddresInputField = null;
        [SerializeField] private Button joinButton = null;


        private void OnEnable(){
            NetworkManagerLobby.OnClientConnected +=  HandleClientConnected;
            NetworkManagerLobby.OnClientDisconnected +=  HandleClientDisconnected;
        }
        private void OnDisable(){
            NetworkManagerLobby.OnClientConnected -=  HandleClientConnected;
            NetworkManagerLobby.OnClientDisconnected -=  HandleClientDisconnected;
        }

        public void JoinLobby(){
            string ipAddress = ipAddresInputField.text;
            if(string.IsNullOrEmpty(ipAddress))
                ipAddress = "localhost";
            networkManager.networkAddress = ipAddress;
            networkManager.StartClient();

            joinButton.interactable = false;
        }


        private void HandleClientConnected() {
            joinButton.interactable = true;

            gameObject.SetActive(false);
            landingPagePanel.SetActive(false);
        }

        private void HandleClientDisconnected() {

            joinButton.interactable = true;
        }
    }
}
