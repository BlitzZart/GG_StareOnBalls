using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public enum PowerUpType {
    Monochrome, Negative
}
namespace Assets.Fun {
    abstract class APowerUp : MonoBehaviour {
        protected abstract void Spawn();
        protected abstract void  Despawn();
        public virtual void Activate(NetworkPlayer player) {
            //Communicator.Player.
        }
    }
}
