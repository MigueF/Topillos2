using UnityEngine;

public class BaseClickHandler : MonoBehaviour
{
    public TowerMenuManager menuManager; // Referencia al gestor del men�
    public bool isOccupied = false; // Indica si la base ya tiene una torre

    private void OnMouseDown()
    {
        // Solo abre el men� si la base no est� ocupada
        if (!isOccupied)
        {
            menuManager.OpenMenu(gameObject);
        }
    }

    public void SetOccupied(bool value)
    {
        isOccupied = value;

        if (isOccupied)
        {
            // Desactiva el collider para evitar m�s interacciones
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            else
            {
                Debug.LogWarning("No se encontr� un Collider2D en el objeto.");
            }
        }
    }
}