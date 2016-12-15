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
        public PowerUpType type;
        public bool IsABadPowerUp;

        public KeyCode testKey = KeyCode.Alpha0;

        public void Activate() {
            if (Communicator.Player.isServer) {
                if (IsABadPowerUp)
                    Communicator.Player.UsePowerUp(type);
                else
                    Communicator.Player.RpcUsePowerUpOnEnemy(type);
            } else {
                if (IsABadPowerUp)
                    Communicator.Player.UsePowerUp(type);
                else
                    Communicator.Player.CmdUsePowerUpOnEnemy(type);
            }
        }

        void Update() {
            if (Input.GetKeyDown(testKey)) {
                Activate();
            }
        }
    }
}