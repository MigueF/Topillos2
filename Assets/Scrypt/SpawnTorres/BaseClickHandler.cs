using UnityEngine;

public class BaseClickHandler : MonoBehaviour
{
    public TowerMenuManager menuManager; // Referencia al script del menú

    private void OnMouseDown()
    {
        // Detecta el clic en esta base y abre el menú
        menuManager.OpenMenu(gameObject);
    }
}
