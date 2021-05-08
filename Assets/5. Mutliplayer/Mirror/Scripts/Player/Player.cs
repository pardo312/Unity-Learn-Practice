using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace MultiplayerMirror {

    public class Player : NetworkBehaviour  {
        [SerializeField] private Vector3 movement = new Vector3(); 

        [Client]
        private void Update() {
            if(hasAuthority) 
                return;
            if(!Input.GetKeyDown(KeyCode.Space)) 
                return;
            
            CmdMove();
        }

        [Command]
        private void CmdMove(){
            // Validate logic here
            
                RcpMove();
            }

        [ClientRpc]
        private void RcpMove() {
            transform.Translate(movement);
        }
    }

}