using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Vector3 followOffset;
        private float followSmoothSpeed = 3f;

        private Transform followTransform;

        private void Start() {
            followTransform = PlayerController.Instance.transform;
        }

        private void FixedUpdate() {
            Vector3 followPosition = followTransform.position;
            //followPosition.x = 0f;
            followPosition.y = 0f;
            this.transform.position = Vector3.Lerp(transform.position, followPosition + followOffset, Time.fixedDeltaTime * followSmoothSpeed);
        }
    }
}
