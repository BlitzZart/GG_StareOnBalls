using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


public enum PowerUpType {
    SkyboxRotator, EdgeDetection, GreyScale, FlipCamera, Blur
}
namespace Assets.Fun {
    public class APowerUp : MonoBehaviour {
        private static bool debugging = false;

        [SerializeField]
        public PowerUpType type;
        [SerializeField]
        public bool IsABadPowerUp;

        public KeyCode testKey = KeyCode.Alpha0;

        public void Activate() {
            NetworkInstanceId nid = GetComponentInParent<NetworkIdentity>().netId;
            if (Communicator.Player.isServer) {
                if (IsABadPowerUp) {
                    Communicator.Player.UsePowerUp(type, transform.parent.gameObject);
                }
                else {
                    Communicator.Player.RpcUsePowerUpOnEnemy(type, nid);
                    Destroy(transform.parent.gameObject);
                }
            }
            else {
                if (IsABadPowerUp) {
                    Communicator.Player.UsePowerUp(type, transform.parent.gameObject);
                    Communicator.Player.CmdDestroyObject(nid);
                }
                else {
                    Communicator.Player.CmdUsePowerUpOnEnemy(type, nid);
                }
            }
        }

        void Update() {
            if (debugging)
                if (Input.GetKeyDown(testKey))
                    Activate();
        }
    }
}