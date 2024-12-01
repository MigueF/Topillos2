using UnityEngine;

public class BaseClickHandler : MonoBehaviour
{
    public TowerMenuManager menuManager; // Referencia al script del men�

    private void OnMouseDown()
    {
        // Detecta el clic en esta base y abre el men�
        menuManager.OpenMenu(gameObject);
    }
}
