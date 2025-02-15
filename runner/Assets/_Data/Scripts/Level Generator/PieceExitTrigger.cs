using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {
    public class PieceExitTrigger : MonoBehaviour {

        IEnumerator despawnCoroutine;

        public bool IsMarkedToDespawn {
            get {
                return despawnCoroutine != null;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.Equals(PlayerController.Instance.PlayerCollider)) {
                despawnCoroutine = DespawnPiece();
                StartCoroutine(despawnCoroutine);
            }
        }

        private IEnumerator DespawnPiece() {
            yield return new WaitForSeconds(1f);
            despawnCoroutine = null;
            LevelGenerator.instance.DespawnLevelPiece(this);
        }

        public void ResetPiece() {
            StopCoroutine(despawnCoroutine);
            despawnCoroutine = null;
        }
    }
}
