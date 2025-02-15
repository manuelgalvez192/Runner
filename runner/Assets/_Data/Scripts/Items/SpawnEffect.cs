using Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    public static SpawnEffect Instance;

    [SerializeField]
    public float probAparecerMulti;

    [SerializeField]
    public float probAparecerRepDown;
    [SerializeField]
    public float probAparecerRepUp;

    [SerializeField]
    public float probAparecerRaro;

    [SerializeField]
    public GameObject prefabReferenceMulti;

    [SerializeField]
    public GameObject prefabReferenceRep;

    [SerializeField]
    public GameObject prefabReferenceRaro;


    [SerializeField]
    public Transform InstanceParent;

    private Transform instance;

    private float prob;

    private int opTonico = 0;

    private int pieza;

    private void Awake()
    {
        //empieza creando un numero aleatorio entre 0 y 100
        prob = Random.Range(0, 100);
    }


    public void OnEnable()
    {
        //si cuando se crea la probabilidad entra
        if (prob <= probAparecerMulti)
        {
            if (TonicosManager.Instance.isSpawneable)//compruebo si puede spawnear, ya que hay una condicion de no poder aparecer en 30seg
            {
                instance = SpawnPool.Instance.Spawn(prefabReferenceMulti.transform, InstanceParent);//creo la instancia de spawneo
                opTonico = 1;//opcion para el switch mas adelante
                TonicosManager.Instance.listaMulti.Add(instance);//lo añado en una lista para poder recorrer y despawnear los restantes creados
                if (TonicosManager.Instance.listaMulti.Count > 1)
                {
                    //despawneo si hay mas de un tonico en la lista
                    SpawnPool.Instance.Despawn(instance);
                    OnDisable();
                }
            }

        }//si la anterior probabilidad no entra, comprueba esta
        else if(prob >= probAparecerRepDown && prob <= probAparecerRepUp)
        {//comprueba si es una de las piezas en las que puede spawnear
            if(LevelGenerator.instance.randomPiece == 0 || LevelGenerator.instance.randomPiece == 3)
            {
                opTonico = 2;//opcion de witch
                instance = SpawnPool.Instance.Spawn(prefabReferenceRep.transform, InstanceParent);//se crea la instancia y se spawnea

                TonicosManager.Instance.listaRep.Add(instance);//añade en lista

                if (TonicosManager.Instance.listaRep.Count > 1)//despawnea si hay mas
                {
                    SpawnPool.Instance.Despawn(instance);
                    OnDisable();
                }
            }
            

        }else if(prob >= probAparecerRaro)//comprueba si entra
        {//comprueba si es una de las piezas correspondientes
            if (LevelGenerator.instance.randomPiece == 1 || LevelGenerator.instance.randomPiece == 4)
            {
                opTonico = 3;
                instance = SpawnPool.Instance.Spawn(prefabReferenceRaro.transform, InstanceParent);//spawneo

                TonicosManager.Instance.listaRaro.Add(instance);//añado en lista

                if (TonicosManager.Instance.listaRaro.Count > 1)//despawn si hay mas de uno
                {
                    SpawnPool.Instance.Despawn(instance);
                    OnDisable();
                }
            }
            
        }

    }

    public void OnDisable()
    {
        //elimino las instancias de la lista si han sido dejadas atras por el jugador
        if(instance != null)
        {
            SpawnPool.Instance.Despawn(instance);
        }
        TonicosManager.Instance.listaMulti.Remove(instance);
        TonicosManager.Instance.listaRep.Remove(instance);
        TonicosManager.Instance.listaRaro.Remove(instance);
        instance = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (opTonico)//compruebo con que pieza a colisionado segun la opcion del switch
        {
           
            case 1:
                TonicoMultiplicador(other);
                break;
            case 2:
                TonicoReparador(other);
                break;
            case 3:
                TonicoRaro(other);
                break;
        }
        
    }

    private void TonicoMultiplicador(Collider other)
    {//al coger un multiplicador de x2
        if (instance != null && other.Equals(PlayerController.Instance.PlayerCollider))
        {
            TonicosManager.Instance.listaMulti.Remove(instance);//lo quito de la lista
            CoinController.multiplicador = 2;//cambio el valor del multiplicador a dos, para que las monedas valgan 2
            SpawnPool.Instance.Despawn(instance);//despawneo la instancia
            instance = null;

            //llamo a corrutinas para aplicar sus efectos
            TonicosManager.Instance.LlamarCorrutina(1);

            TonicosManager.Instance.LlamarCorrutina(2);

            TonicosManager.Instance.LlamarCorrutina(3);
            
        }
    }

    private void TonicoReparador(Collider other)
    {//igual que el anterior
        if (instance != null && other.Equals(PlayerController.Instance.PlayerCollider))
        {
            TonicosManager.Instance.listaRep.Remove(instance);
            SpawnPool.Instance.Despawn(instance);
            instance = null;
            TonicosManager.Instance.AumentarVida();//aumenta la vida del jugador y su icono
            
        }
    }

    private void TonicoRaro(Collider other)
    {
        if (instance != null && other.Equals(PlayerController.Instance.PlayerCollider))
        {
            CoinController.multiplicador = 10;//el multiplicador a 10, para que las monedas valgan 10
            TonicosManager.Instance.listaRaro.Remove(instance);
            SpawnPool.Instance.Despawn(instance);
            instance = null;

            TonicosManager.Instance.LlamarCorrutina(4);

            TonicosManager.Instance.LlamarCorrutina(5);
        }
    }


}
