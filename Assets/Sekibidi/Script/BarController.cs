using UnityEngine;
using UnityEngine.UI;
public class BarController : MonoBehaviour
{
    [SerializeField] private string barName;
    [SerializeField] private float value;
    [SerializeField] public Slider slider;
    [SerializeField] private Image fillImage;
    [SerializeField] private RectTransform fillArea;

    public void UpdateBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;

    }

    void Update()
    {
        // slider.transform.localScale = new Vector3 (0.02, 0.02, ;
    }





    // Start is called once before the first execution of Update after the MonoBehaviour is create
}
