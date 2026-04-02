using UnityEngine;

public class Archer : MonoBehaviour
{
    public GameObject dest;
    public GameObject arrow;

    void Update()
    {
        Vector2 direction = dest.transform.position - arrow.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        arrow.transform.rotation = Quaternion.Euler(0, 0, angle-90f);
    }
}