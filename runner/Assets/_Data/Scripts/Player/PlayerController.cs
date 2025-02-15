using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {
    public class PlayerController : MonoBehaviour {

        [SerializeField]
        private Transform trPlayer;
        [SerializeField]
        private GameEvent updateDistance;
        [SerializeField]
        private IntValue distanceValue;

        public static PlayerController Instance;

        private int distanciaTotalRecorrida;

        public IntValue playerHealth;
        public GameEvent onPlayerLifeChange;

        private PlayerMovement playerMovement;
        public PlayerMovement PlayerMovement {
            get {
                return playerMovement;
            }
        }
        private Collider playerCollider;
        public Collider PlayerCollider {
            get {
                return playerCollider;
            }
        }

        private void Awake() {
            Instance = this;

            playerMovement = GetComponent<PlayerMovement>();
            playerCollider = GetComponent<Collider>();
        }

        public void SubstractHealth() {
            playerHealth.runtimeValue--;
            
            onPlayerLifeChange.Raise();
        }

        public void AddDistance()//metodo para aumentar la distancia recorrida
        {//si la velociad es mayor que 0, es decir, se esta moviendo
            //y el resto de la posicon entre dos es mayor que 1.5, esto lo hago porque daba la sensacion de que la distancia
            //aumentaba muy rapido, entonces para que aumente mas lento
            if ((PlayerMovement.Instance.currentForwardSpeed > 0) && (transform.position.z % 2 > 1.5f))
            {
                distanceValue.runtimeValue++;//sumo distancia recorrida
                updateDistance.Raise();//la actualizo

            }
            
        }
    }
}
