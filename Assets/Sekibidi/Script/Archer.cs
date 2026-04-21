using UnityEngine;

public class Archer : MonoBehaviour
{
    public Transform dest;
    public RectTransform arrow;
    public Camera cam;
    public Canvas canvas;

    public float smoothSpeed = 10f;
    public float rotSmooth = 10f;

    void Update()
    {
        // World → Screen
        Vector3 screenPos = cam.WorldToScreenPoint(dest.position);

        float margin = 50f;

        // Clamp target dulu
        screenPos.x = Mathf.Clamp(screenPos.x, margin, Screen.width - margin);
        screenPos.y = Mathf.Clamp(screenPos.y, margin, Screen.height - margin);

        // Screen → World (canvas)
        Vector3 targetWorldPos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvas.transform as RectTransform,
            screenPos,
            cam,
            out targetWorldPos
        );

        // 🔥 SMOOTH + CLAMP FINAL (ini pengganti arrow.position lama)
        Vector3 newPos = Vector3.Lerp(
            arrow.position,
            targetWorldPos,
            Time.deltaTime * smoothSpeed
        );

        // Clamp ulang biar gak keluar layar
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

        // 🔥 ROTASI SMOOTH
        Vector2 direction = dest.position - arrow.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRot = Quaternion.Euler(0, 0, targetAngle);

        arrow.rotation = Quaternion.Lerp(
            arrow.rotation,
            targetRot,
            Time.deltaTime * rotSmooth
        );
    }
}