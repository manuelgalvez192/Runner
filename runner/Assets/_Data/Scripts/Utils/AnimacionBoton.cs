using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimacionBoton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    private Button boton;
    [SerializeField]
    private Color colorHover;
    [SerializeField]
    private Color colorClick;

    private Color colorInicial;

    private void Start()
    {
        colorInicial = boton.image.color;//guardo el color que se establece en el boton
    }
    
    public void OnPointerUp(PointerEventData eventData)//al levantar el click
    {
        boton.image.color = colorInicial;//lo pongo de su color inicial
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        
        boton.image.color = colorClick;//al bajar click le cambio el color al pasado por editor
    }
    
    // private void Update()
    // {
    //     //si la escala se pasa de tamaño tanto por arriba como por abajo la deja en escala 1
    //     if (transform.localScale.x < 2.84 || transform.localScale.x > 3.34)
    //     {
    //         transform.localScale = initialScale;
    //     }
    // }

    public void OnPointerEnter(PointerEventData eventData)
    {
        boton.image.color = colorHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        boton.image.color = colorInicial;
    }
    
    

}
