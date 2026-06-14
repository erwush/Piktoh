using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Archer2 : MonoBehaviour
{
    [Header("Targets")]
    public List<Transform> targets = new();

    [Header("UI")]
    public RectTransform arrow;
    public Camera cam;
    public Canvas canvas;

    [Header("Settings")]
    public float smoothSpeed = 10f;
    public float rotSmooth = 10f;
    public float margin = 50f;

    private Image arrowImage;

    void Start()
    {
        arrowImage = arrow.GetComponent<Image>();
    }

    void Update()
    {
        // Hapus target yang sudah di-destroy
        targets.RemoveAll(t => t == null);

        Transform target = GetClosestTarget();

        if (target == null)
        {
            if (arrowImage != null)
                arrowImage.color = new Color(1f, 1f, 1f, 0f);

            return;
        }

        if (arrowImage != null)
            arrowImage.color = Color.white;

        // World -> Screen
        Vector3 screenPos = cam.WorldToScreenPoint(target.position);

        // Clamp ke layar
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

        // Rotasi panah ke target
        Vector2 direction = target.position - arrow.position;

        float targetAngle =
            Mathf.Atan2(direction.y, direction.x) *
            Mathf.Rad2Deg - 90f;

        Quaternion targetRot =
            Quaternion.Euler(0, 0, targetAngle);

        arrow.rotation = Quaternion.Lerp(
            arrow.rotation,
            targetRot,
            Time.deltaTime * rotSmooth
        );
    }

    Transform GetClosestTarget()
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform t in targets)
        {
            if (t == null)
                continue;

            float distance = Vector2.Distance(
                transform.position,
                t.position
            );

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = t;
            }
        }

        return closest;
    }

    public void AddTarget(Transform target)
    {
        if (target != null && !targets.Contains(target))
            targets.Add(target);
    }

    public void RemoveTarget(Transform target)
    {
        if (target != null)
            targets.Remove(target);
    }
}