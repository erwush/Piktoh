using UnityEngine;

public static class GemYutil
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static void SetInactive(GameObject obj) => obj.SetActive(false);
    public static void SetActive(GameObject obj) => obj.SetActive(true);
}
