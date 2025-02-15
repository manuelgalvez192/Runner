using Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {


    [SerializeField]
    private GameEvent pickCoinEvent;

    public IntValue scoreValue;

    [SerializeField]
    private GameObject prefabReference;
    [SerializeField]
    private Transform instanceParent;

    public int cantidadSumar = 1;
    public static int multiplicador = 1;

    private Transform instance;


    private void OnApplicationQuit()
    {
        if (this.gameObject.scene.isLoaded) SpawnPool.Instance.Despawn(instance);

    }

    private void OnEnable() {
        instance = SpawnPool.Instance.Spawn(prefabReference.transform, instanceParent);
    }

    private void OnDisable() {
        if (instance != null) {
            SpawnPool.Instance.Despawn(instance);
        }
        instance = null;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (instance != null && other.Equals(PlayerController.Instance.PlayerCollider)) {
            SpawnPool.Instance.Despawn(instance);
            instance = null;
            SumarPuntos();
            pickCoinEvent.Raise();
        }
    }

    public void SumarPuntos()
    {
        scoreValue.runtimeValue += cantidadSumar * multiplicador;
    }

}
