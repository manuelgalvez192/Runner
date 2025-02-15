using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloatingOrigin : MonoBehaviour {
    private float threshold = 150;

    private Transform cameraTransform;

    private void Start() {
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate() {
        Vector3 cameraPosition = cameraTransform.position;
        cameraPosition.y = 0f;
        cameraPosition.x = 0f;

        if (cameraPosition.magnitude >= threshold) {
            ResetLevelPostion(cameraPosition);
        }
    }

    public static void ResetLevelPostion(Vector3 cameraPosition) {

        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        for (int i = 0; i < rootObjects.Length; i++) {
            rootObjects[i].transform.position -= cameraPosition;
        }

        Vector3 newOffset = Vector3.zero - cameraPosition;
        LevelGenerator.instance.UpdateOffset(newOffset);
    }
}
