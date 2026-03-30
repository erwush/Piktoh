using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public string sliderName;
    public Slider slider;
    public Image fillImage;
    public RectTransform fillArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void ChangeValue(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
}
