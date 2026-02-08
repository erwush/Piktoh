using UnityEngine;

public class Oronyx : MonoBehaviour
{
    public float progress;
    public GameObject progressBar;
    private bool inArea;
    private BarController barController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        barController = GetComponent<BarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inArea && Input.GetButton("Mine"))
        {
            progressBar.SetActive(true);
        }
    }
}
