using UnityEngine;

public class BaseClickHandler : MonoBehaviour
{
    public TowerMenuManager menuManager; // Referencia al gestor del men�

    private void OnMouseDown()
    {
        // Llama al gestor del men� al hacer clic en la base
        menuManager.OpenMenu(gameObject);
    }
}