using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildController : MonoBehaviour
{
    public float gridMovementSize;
   
    public GameObject spawnedPrefab;
    private bool isDragging;
    void Update()
    {
        if (isDragging && spawnedPrefab != null)
        {
            HandleDrag(spawnedPrefab);
        }
    }

    public void DragStart(GameObject prefab)
    {
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab);
        }
        if (prefab != null)
        {
           // if (!EventSystem.current.IsPointerOverGameObject())
           // {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos = SnapToGrid(mousePos); // Snap to grid
                spawnedPrefab = Instantiate(prefab, mousePos, prefab.transform.rotation);
                isDragging = true;
           // }
        }
        
    }

    private void HandleDrag(GameObject prefab)
    {
        if (prefab != null)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos = SnapToGrid(mousePos); // Snap to grid
                prefab.transform.position = mousePos;

                if (Input.GetMouseButtonUp(0))
                {
                    DragEnd();
                }
            }
        }
    }

    public void DragEnd()
    {
        if (spawnedPrefab != null)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos = SnapToGrid(mousePos); // Snap to grid
                if (IsValidPosition(mousePos))
                {
                    Destroy(spawnedPrefab); // Destroy the prefab if placed on a specific tag or layer
                }
                else
                {
                    spawnedPrefab.layer = LayerMask.NameToLayer("Tower");
                }
                isDragging = false;
            }
            else
            {
                Destroy(spawnedPrefab);
            }
        }
    }

    private Vector2 SnapToGrid(Vector2 position)
    {
        // Define the size of each grid cell
        float gridSize = gridMovementSize;

        // Calculate the snapped position by rounding the coordinates to the nearest grid point
        float snappedX = Mathf.Round(position.x / gridSize) * gridSize;
        float snappedY = Mathf.Round(position.y / gridSize) * gridSize;

        // Create a new Vector2 with the snapped coordinates
        Vector2 snappedPosition = new Vector2(snappedX, snappedY);

        return snappedPosition;
    }

    private bool IsValidPosition(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f); // Change 0.1f to the appropriate radius
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Tower"))
            {
                return true; // Tower exists at the position, so the position is invalid
            }
        }
        return false; // No tower at the position, so the position is valid
    }
}
