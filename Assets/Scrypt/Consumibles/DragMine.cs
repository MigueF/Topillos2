using UnityEngine;
using UnityEngine.EventSystems;

public class DragMine : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject minePrefab; // Asigna el prefab de la mina desde el inspector
    private GameObject mineInstance;
    private Canvas canvas;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>(); // Encuentra el canvas en la escena
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnDragStart(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Actualiza la posición de la mina mientras se arrastra
        if (mineInstance != null)
        {
            mineInstance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDragEnd(eventData);
    }

    public void OnDragStart(PointerEventData eventData)
    {
        // Instancia la mina cuando comienza el arrastre
        mineInstance = Instantiate(minePrefab, canvas.transform);
        mineInstance.transform.position = eventData.position;
    }

    public void OnDragEnd(PointerEventData eventData)
    {
        // Finaliza el arrastre y coloca la mina en la posición final
        if (mineInstance != null)
        {
            mineInstance.transform.position = eventData.position;
            // Aquí puedes agregar lógica adicional si es necesario
        }
    }
}
