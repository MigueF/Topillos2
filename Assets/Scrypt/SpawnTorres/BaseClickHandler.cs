using UnityEngine;

public class BaseClickHandler : MonoBehaviour
{
    public TowerMenuManager towerMenuManager; // Referencia al TowerMenuManager
    public GameObject currentTower; // Torre colocada en esta base
    public string baseColor; // Color de la base (rojo, azul, etc.)
    private bool isOccupied = false; // Estado de ocupación de la base

    void Start()
    {
        if (towerMenuManager == null)
        {
            towerMenuManager = FindObjectOfType<TowerMenuManager>();
        }
    }

    void OnMouseDown()
    {
        if (!isOccupied)
        {
            towerMenuManager.OpenMenu(gameObject);
        }
        else
        {
            Debug.Log("La base ya está ocupada.");
        }
    }

    public void SetOccupied(bool value, GameObject tower = null)
    {
        isOccupied = value;
        currentTower = tower;

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = !value; // Desactivar el collider si está ocupada
        }
    }

    public void ClearBase()
    {
        SetOccupied(false);
        Debug.Log("La base ha sido liberada.");
    }

    // Devuelve la torre colocada en la base
    public GameObject GetTower()
    {
        return currentTower;
    }
}
