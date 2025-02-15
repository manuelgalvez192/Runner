using Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public static LevelGenerator instance;

    [SerializeField]
    private GameObject defaultPiece;

    [SerializeField]
    private List<GameObject> levelPieces;

    private float pieceLenght = 30f;
    private float currentOffset = 0f;
    private int initAmount = 4;

    public int randomPiece;

    private List<PieceExitTrigger> spawnedLevelPieces = new List<PieceExitTrigger>();

    private void Awake() {
        instance = this;
    }

    private void Start() {

        Transform firstPiece = SpawnPool.Instance.Spawn(defaultPiece.transform, new Vector3(0f, 0f, currentOffset), Quaternion.identity, Vector3.one);
        currentOffset += pieceLenght;
        spawnedLevelPieces.Add(firstPiece.GetComponentInChildren<PieceExitTrigger>());

        for (int i = 0; i < initAmount; i++) {
            SpawnNewPiece();
        }
    }

    public void SpawnNewPiece() {
        randomPiece = Random.Range(0, levelPieces.Count);
        GameObject prefab = levelPieces[randomPiece];
        Transform piece = SpawnPool.Instance.Spawn(prefab.transform, new Vector3(0f, 0f, currentOffset), Quaternion.identity, Vector3.one);
        currentOffset += pieceLenght;
        spawnedLevelPieces.Add(piece.GetComponentInChildren<PieceExitTrigger>());
    }

    public void DespawnLevelPiece(PieceExitTrigger piece) {
        spawnedLevelPieces.Remove(piece);
        SpawnPool.Instance.Despawn(piece.transform.root);
        SpawnNewPiece();
    }

    public void UpdateOffset(Vector3 newOffset) {
        currentOffset = currentOffset + newOffset.z;
    }

    private void CheckIfDespawnImmediately() {
        for (int i = 0; i < spawnedLevelPieces.Count; i++) {
            if (spawnedLevelPieces[i].IsMarkedToDespawn) {
                Debug.Log("Reset Piece");
                spawnedLevelPieces[i].ResetPiece();
                DespawnLevelPiece(spawnedLevelPieces[i]);
            }
        }
    }

    public void ResetLevelLayout() {

        CheckIfDespawnImmediately();

        Vector3 cameraPosition = Camera.main.transform.position;
        cameraPosition.x = 0f;
        cameraPosition.y = 0f;
        FloatingOrigin.ResetLevelPostion(cameraPosition);

        currentOffset = 0f;
        Transform firstPiece = SpawnPool.Instance.Spawn(defaultPiece.transform, Vector3.zero, Quaternion.identity, Vector3.one);
        spawnedLevelPieces.Insert(0, firstPiece.GetComponentInChildren<PieceExitTrigger>());
        currentOffset += pieceLenght;

        for (int i = 1; i < spawnedLevelPieces.Count; i++) {
            Transform piece = spawnedLevelPieces[i].transform.root;
            piece.transform.position = new Vector3(0f, 0f, currentOffset);
            currentOffset += pieceLenght;
        }
    }
}
