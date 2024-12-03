using UnityEngine;

public class BaseClickHandler : MonoBehaviour
{
    public TowerMenuManager menuManager; // Referencia al gestor del menú
    public bool isOccupied = false; // Indica si la base ya tiene una torre

    private void OnMouseDown()
    {
        // Solo abre el menú si la base no está ocupada
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
            // Desactiva el collider para evitar más interacciones
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            else
            {
                Debug.LogWarning("No se encontró un Collider2D en el objeto.");
            }
        }
    }
}