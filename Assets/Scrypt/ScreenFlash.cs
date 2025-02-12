using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public static ScreenFlash instance;
    public Image flashImage;
    public float flashDuration = 0.1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Flash()
    {
        Debug.Log("Flash method called");
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        Debug.Log("FlashRoutine started");
        flashImage.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        flashImage.enabled = false;
        Debug.Log("FlashRoutine ended");
    }
}


