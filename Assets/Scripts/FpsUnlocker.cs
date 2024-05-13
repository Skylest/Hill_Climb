using System.Collections;
using UnityEngine;

/// <summary>
/// Класс реализовывающий разблокировку максимального FPS
/// </summary>
public class FpsUnlocker : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(FpsUnlock());
    }

    /// <summary>
    /// Убирает ограничение в 30 кадров
    /// </summary> 
    IEnumerator FpsUnlock()
    {
        while (true)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 9999;
            yield return null;
        }
    }
}

