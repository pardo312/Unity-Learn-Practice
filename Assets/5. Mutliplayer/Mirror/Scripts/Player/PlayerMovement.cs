using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Mirror;
using UnityEngine.AI;

namespace MultiplayerMirror {
    public class PlayerMovement : NetworkBehaviour {
        public GameObject prefabBullet;
        public Transform bulletTransform;

        NavMeshAgent navMeshAgent;

        public void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update() {
            if (!hasAuthority) {
                return;
            }
            
            if (Input.GetMouseButtonDown(1)) {
                RaycastHit tempHit;
                Ray tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(tempRay, out tempHit, 100, LayerMask.GetMask("WorldMap"))) {
                    CmdScrPlayerSetDestination(tempHit.point);
                }
            }
        }

        [Command]
        public void CmdScrPlayerSetDestination(Vector3 argPosition)
        {
            navMeshAgent.SetDestination(argPosition);
            RpcScrPlayerSetDestination(argPosition);    
        }

        [ClientRpc]
        public void RpcScrPlayerSetDestination(Vector3 argPosition)
        {
            navMeshAgent.SetDestination(argPosition);
        }
    }
}
