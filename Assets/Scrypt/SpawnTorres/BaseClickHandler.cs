using UnityEngine;

public class BaseClickHandler : MonoBehaviour
{
    public TowerMenuManager menuManager; // Referencia al gestor del menú

    private void OnMouseDown()
    {
        // Llama al gestor del menú al hacer clic en la base
        menuManager.OpenMenu(gameObject);
    }
}