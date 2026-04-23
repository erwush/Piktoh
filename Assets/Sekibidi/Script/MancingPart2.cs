using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MancingPart2 : MonoBehaviour
{
    public float xLimit = 310f;
    public Image strike;
    public Image bait;
    public float strikePos;
    public float baitSpd;
    public float strikeSpd;
    public bool isFishing;
    public int movingState; //0 = toPositive, 1 = toNegative
    public int failCounter;
    public bool canStrike;
    public RectTransform rect;
    public TextMeshProUGUI[] text;
    public float distanceTolerance;
    public int strikeCounter;
    public AudioSource audio;
    private int[] number;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = bait.GetComponent<RectTransform>();
        number = new int[2] { -1, 1 };
    }

    // Update is called once per frame
    void Update()
    {
        int randomIndex = Random.Range(0, number.Length);

        // 3. Access the value at that index
        int randomX = number[randomIndex];
        if (isFishing)
        {
            strike.rectTransform.anchoredPosition = new Vector3(strike.rectTransform.anchoredPosition.x + (strikeSpd * randomX), strike.rectTransform.anchoredPosition.y);
        }
        if (isFishing && Vector3.Distance(bait.rectTransform.anchoredPosition, strike.rectTransform.anchoredPosition) < distanceTolerance)
        {
            canStrike = true;
        }
        else
        {
            canStrike = false;
        }
        if (isFishing && Input.GetKey(KeyCode.Space) && bait.rectTransform.anchoredPosition.x < xLimit && bait.rectTransform.anchoredPosition.x > -xLimit)
        {
            bait.rectTransform.anchoredPosition = new Vector3(bait.rectTransform.anchoredPosition.x + baitSpd, bait.rectTransform.anchoredPosition.y);
        }
        else if(isFishing && bait.rectTransform.anchoredPosition.x >= xLimit || isFishing && bait.rectTransform.anchoredPosition.x <= -xLimit)
        {
            bait.rectTransform.anchoredPosition = new Vector3(bait.rectTransform.anchoredPosition.x - baitSpd, bait.rectTransform.anchoredPosition.y);
        }


        // if (Input.GetKeyDown(KeyCode.Space) && canStrike && isFishing)
        // {
        //     failCounter = 0;
        //     strikeCounter++;
        //     text[0].text = "Mantap";
        //     text[1].text = "Gagal: " + failCounter;
        //     text[2].text = "Strike: " + strikeCounter;
        //     audio.Play();
        //     if (strikeCounter == 3)
        //     {
        //         text[2].text = "Strike: " + strikeCounter;
        //         strikeCounter = 0;
        //         isFishing = false;
        //     }
        //     else if (strikeCounter < 3)
        //     {
        //         ChangeStrike();
        //     }
        // }
        // else if (Input.GetKeyDown(KeyCode.Space) && !canStrike && isFishing)
        // {
        //     text[0].text = "Gk Mantap";
        //     failCounter++;
        //     text[1].text = "Gagal: " + failCounter;
        //     if (failCounter == 3)
        //     {
        //         text[1].text = "Gagal: " + failCounter;
        //         failCounter = 0;
        //         isFishing = false;
        //     }
        // }

    }

    public void StartFishing()
    {
        failCounter = 0;
        strikeCounter = 0;
        text[0].text = "Mancing Mania?";
        text[1].text = "Gagal: " + failCounter;
        text[2].text = "Strike: " + strikeCounter;
        strikePos = Random.Range(-xLimit, xLimit);
        strike.rectTransform.anchoredPosition = new Vector3(strikePos, strike.rectTransform.anchoredPosition.y);
        isFishing = true;
    }

    public void ChangeStrike()
    {
        strikePos = Random.Range(-xLimit, xLimit);
        strike.rectTransform.anchoredPosition = new Vector3(strikePos, strike.rectTransform.anchoredPosition.y);
    }




}
