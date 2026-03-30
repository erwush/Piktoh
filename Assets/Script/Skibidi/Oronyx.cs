using UnityEngine;
using UnityEngine.UI;

public class Oronyx : MonoBehaviour
{
    //*THIS IS SUMUR
    public float[] progress;
    public float[] maxProgress;
    public GameObject progressBar;
    private bool inArea;
    private BarController barController;
    public bool isNarik;
    public AudioSource sfx;
    public GameObject keybind;
    public bool isStart;
    public GameObject reward;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        barController = GetComponent<BarController>();
        maxProgress[0] = 2f;
        maxProgress[1] = 5;
        progress[0] = 0;
        progress[1] = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        if(inArea && !isStart && Input.GetKeyDown(KeyCode.E))
        {
            isStart = true;
        }
        if(isStart){
            progressBar.SetActive(true);
        } else
        {
            progressBar.SetActive(false);
        }

        if(inArea){
            keybind.SetActive(true);
        } else
        {
            keybind.SetActive(false);
        }

        if (inArea && Input.GetKey(KeyCode.Space) && !isNarik && progress[0] < maxProgress[0] && isStart)
        {
            progressBar.SetActive(true);
            progress[0] += Time.deltaTime;
            barController.slider.direction = Slider.Direction.RightToLeft;
            barController.UpdateBar(progress[0], maxProgress[0]);
        }
        else if (inArea && Input.GetKeyDown(KeyCode.Space) && isNarik && progress[1] < maxProgress[1] && isStart)
        {
            progressBar.SetActive(true);
            barController.UpdateBar(progress[1], maxProgress[1]);
            barController.slider.direction = Slider.Direction.LeftToRight;
            progress[1] += 1;
        }

        if(progress[0] >= maxProgress[0]){
            isNarik = true;
            barController.UpdateBar(progress[1], maxProgress[1]);
        }
        if (progress[1] >= maxProgress[1])
        {
            isNarik = false;
        }
        if(progress[0] >= maxProgress[0] && progress[1] >= maxProgress[1]){
            progressBar.SetActive(false);
            progress[0] = 0;
            progress[1] = 0;
            isStart = false;
            isNarik = false;
            // Instantiate(reward);
            reward.SetActive(true);
            sfx.Play();
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
            inArea = false;
        }
    }
}
