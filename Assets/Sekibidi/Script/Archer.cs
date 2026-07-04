using UnityEngine;

public class Archer : MonoBehaviour
{
    [Header("Quest Target Tags")]
    public string[] targetTags;

    [Header("References")]
    public RectTransform arrow;
    public Camera cam;
    public Canvas canvas;

    [Header("Settings")]
    public float smoothSpeed = 10f;
    public float rotSmooth = 10f;
    public float margin = 50f;

    [Header("Player")]
    public Transform player; // isi player di inspector

    void Update()
    {
        int questIndex = Questing.Instance.indeksMisiAktif;

        if (questIndex < 0 || questIndex >= targetTags.Length)
            return;

        string currentTag = targetTags[questIndex];

        if (string.IsNullOrEmpty(currentTag))
            return;

        GameObject[] targets = GameObject.FindGameObjectsWithTag(currentTag);

        if (targets.Length == 0)
            return;

        // Cari target terdekat
        Transform nearest = null;
        float nearestDistance = Mathf.Infinity;

        Vector3 origin = player != null ? player.position : transform.position;

        foreach (GameObject obj in targets)
        {
            if (obj == null)
                continue;

            float distance = (obj.transform.position - origin).sqrMagnitude;

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearest = obj.transform;
            }
        }

        if (nearest == null)
            return;

        // World -> Screen
        Vector3 screenPos = cam.WorldToScreenPoint(nearest.position);

        screenPos.x = Mathf.Clamp(screenPos.x, margin, Screen.width - margin);
        screenPos.y = Mathf.Clamp(screenPos.y, margin, Screen.height - margin);

        // Screen -> Canvas World
        Vector3 targetWorldPos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvas.transform as RectTransform,
            screenPos,
            cam,
            out targetWorldPos
        );

        // Smooth movement
        Vector3 newPos = Vector3.Lerp(
            arrow.position,
            targetWorldPos,
            Time.deltaTime * smoothSpeed
        );

        // Clamp lagi setelah smoothing
        Vector3 screenClamp = cam.WorldToScreenPoint(newPos);

        screenClamp.x = Mathf.Clamp(screenClamp.x, margin, Screen.width - margin);
        screenClamp.y = Mathf.Clamp(screenClamp.y, margin, Screen.height - margin);

        Vector3 finalWorldPos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvas.transform as RectTransform,
            screenClamp,
            cam,
            out finalWorldPos
        );

        arrow.position = finalWorldPos;

        // Rotasi arrow ke target
        Vector2 direction = nearest.position - arrow.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRot = Quaternion.Euler(0f, 0f, targetAngle);

        arrow.rotation = Quaternion.Lerp(
            arrow.rotation,
            targetRot,
            Time.deltaTime * rotSmooth
        );
    }
}

// using UnityEngine;

// public class Archer : MonoBehaviour
// {
//     public Transform[] dest;
//     public RectTransform arrow;
//     public Camera cam;
//     public Canvas canvas;
//     public float smoothSpeed = 10f;
//     public float rotSmooth = 10f;

//     void Update()
//     {
//         // World → Screen
//         Vector3 screenPos = cam.WorldToScreenPoint(dest[Questing.Instance.indeksMisiAktif].position);

//         float margin = 50f;

//         // Clamp target dulu
//         screenPos.x = Mathf.Clamp(screenPos.x, margin, Screen.width - margin);
//         screenPos.y = Mathf.Clamp(screenPos.y, margin, Screen.height - margin);

//         // Screen → World (canvas)
//         Vector3 targetWorldPos;
//         RectTransformUtility.ScreenPointToWorldPointInRectangle(
//             canvas.transform as RectTransform,
//             screenPos,
//             cam,
//             out targetWorldPos
//         );

//         // 🔥 SMOOTH + CLAMP FINAL (ini pengganti arrow.position lama)
//         Vector3 newPos = Vector3.Lerp(
//             arrow.position,
//             targetWorldPos,
//             Time.deltaTime * smoothSpeed
//         );

//         // Clamp ulang biar gak keluar layar
//         Vector3 screenClamp = cam.WorldToScreenPoint(newPos);

//         screenClamp.x = Mathf.Clamp(screenClamp.x, margin, Screen.width - margin);
//         screenClamp.y = Mathf.Clamp(screenClamp.y, margin, Screen.height - margin);

//         Vector3 finalWorldPos;
//         RectTransformUtility.ScreenPointToWorldPointInRectangle(
//             canvas.transform as RectTransform,
//             screenClamp,
//             cam,
//             out finalWorldPos
//         );

//         arrow.position = finalWorldPos;

//         // 🔥 ROTASI SMOOTH
//         Vector2 direction = dest[Questing.Instance.indeksMisiAktif].position - arrow.position;
//         float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

//         Quaternion targetRot = Quaternion.Euler(0, 0, targetAngle);

//         arrow.rotation = Quaternion.Lerp(
//             arrow.rotation,
//             targetRot,
//             Time.deltaTime * rotSmooth
//         );
//     }
// }