using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

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
    public Player pleyer;
    public GameObject keybind;
    public TextMeshProUGUI keyText;
    public bool isStart;
    public GameObject reward;
    public UnityEvent onComplete;
    public Image progressImage;
    public GameObject progressObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // barController = GetComponent<BarController>();
        maxProgress[0] = 2f;
        maxProgress[1] = 5;
        progress[0] = 0;
        progress[1] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (inArea && !isStart && Input.GetKeyDown(KeyCode.E))
        {
            isStart = true;
        }
        if (isStart)
        {
            progressObject.SetActive(true);
        }
        else
        {
            progressObject.SetActive(false);
        }

        if (inArea)
        {
            keybind.SetActive(true);
        }
        else
        {
            keybind.SetActive(false);
        }

        if (inArea && Input.GetKey(KeyCode.Space) && !isNarik && progress[0] < maxProgress[0] && isStart)
        {
            progressObject.SetActive(true);
            progress[0] += Time.deltaTime;
            progressImage.fillAmount = progress[0] / maxProgress[0];
            progressImage.fillOrigin = (int)Image.OriginHorizontal.Right;
            // barController.slider.direction = Slider.Direction.RightToLeft;
            // barController.UpdateBar(progress[0], maxProgress[0]);
            if (progress[0] >= maxProgress[0])
            {
                pleyer.energy -= 2;
            }
        }
        else if (inArea && Input.GetKeyDown(KeyCode.Space) && isNarik && progress[1] < maxProgress[1] && isStart)
        {
            pleyer.energy -= 2;
            progressObject.SetActive(true);
            progressImage.fillAmount = progress[1] / maxProgress[1];
            progressImage.fillOrigin = (int)Image.OriginHorizontal.Left;
            // barController.UpdateBar(progress[1], maxProgress[1]);
            // barController.slider.direction = Slider.Direction.LeftToRight;
            progress[1] += 1;
        }

        if (progress[0] >= maxProgress[0])
        {
            isNarik = true;
            progressImage.fillAmount = progress[1] / maxProgress[1];
            progressImage.fillOrigin = (int)Image.OriginHorizontal.Left;

            // barController.UpdateBar(progress[1], maxProgress[1]);
        }
        if (progress[1] >= maxProgress[1])
        {
            isNarik = false;
        }
        if (progress[0] >= maxProgress[0] && progress[1] >= maxProgress[1])
        {
            progressObject.SetActive(false);
            progress[0] = 0;
            progress[1] = 0;
            isStart = false;
            isNarik = false;
            // Instantiate(reward);
            onComplete?.Invoke();
            sfx.Play();
        }

        if (isStart && !isNarik)
        {
            keybind.GetComponent<RectTransform>().sizeDelta = new Vector2(4f, 1.1f);
            keyText.text = "Tahan Spasi";
        } else if(isStart && isNarik)
        {
            keybind.GetComponent<RectTransform>().sizeDelta = new Vector2(4f, 1.1f);
            keyText.text = "Tahan Spasi";
        }
        else
        {
            keybind.GetComponent<RectTransform>().sizeDelta = new Vector2(1f, 1f);
            keyText.text = "E";
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inArea = true;
        }
        if (pleyer == null)
        {
            pleyer = collision.gameObject.GetComponent<Player>();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inArea = false;
        }
    }
}
