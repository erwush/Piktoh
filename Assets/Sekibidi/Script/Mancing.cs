using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Mancing : MonoBehaviour
{
    public float xLimit = 310f;
    public Image strike;
    public Image bait;
    public float strikePos;
    public float baitSpd;
    public bool isFishing;
    public int movingState; //0 = toPositive, 1 = toNegative
    public int failCounter;
    public bool canStrike;
    // public RectTransform rect;
    public TextMeshProUGUI[] text;
    public float distanceTolerance;
    public int strikeCounter;
    public GameObject mancingUI;
    public GameObject pleyer;
    public AudioSource mancingAudio;
    Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // rect = bait.GetComponent<RectTransform>();
        pleyer.GetComponent<Animator>();
        anim = pleyer.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFishing) anim.SetBool("isMancing", true);
        else anim.SetBool("isMancing", false);
        
        if (isFishing && movingState == 0)
        {
            bait.rectTransform.anchoredPosition = Vector3.MoveTowards(
                bait.rectTransform.anchoredPosition,
                new Vector3(xLimit, bait.rectTransform.anchoredPosition.y),
                (baitSpd * 100) * Time.deltaTime
            );

            if (bait.rectTransform.anchoredPosition.x >= xLimit)
            {
                movingState = 1;
            }

        }

        if (isFishing && movingState == 1)
        {
            bait.rectTransform.anchoredPosition = Vector3.MoveTowards(
                bait.rectTransform.anchoredPosition,
                new Vector3(-xLimit, bait.rectTransform.anchoredPosition.y),
                (baitSpd * 100) * Time.deltaTime
            );

            if (bait.rectTransform.anchoredPosition.x <= -xLimit)
            {
                movingState = 0;
            }
        }
        if (isFishing && Vector3.Distance(bait.rectTransform.anchoredPosition, strike.rectTransform.anchoredPosition) < distanceTolerance)
        {
            canStrike = true;
        }
        else
        {
            canStrike = false;
        }


        if (Input.GetKeyDown(KeyCode.Space) && canStrike && isFishing)
        {
            failCounter = 0;
            strikeCounter++;
            text[0].text = "Mantap";
            text[1].text = "Gagal: " + failCounter;
            text[2].text = "Strike: " + strikeCounter;
            mancingAudio.Play();
            if (strikeCounter == 3)
            {
                mancingUI.SetActive(false);
                text[2].text = "Strike: " + strikeCounter;
                strikeCounter = 0;
                isFishing = false;
                anim.speed = 1f;    
            }
            else if (strikeCounter < 3)
            {
                ChangeStrike();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !canStrike && isFishing)
        {
            text[0].text = "Gk Mantap";
            failCounter++;
            text[1].text = "Gagal: " + failCounter;
            if (failCounter == 3)
            {
                text[1].text = "Gagal: " + failCounter;
                failCounter = 0;
                anim.speed = 1f;
                mancingUI.SetActive(false);
                isFishing = false;
            }
        }

    }


    public void StartFishing()
    {
        failCounter = 0;
        strikeCounter = 0;
        text[0].text = "Mancing Mania?";
        text[1].text = "Gagal: " + failCounter;
        text[2].text = "Strike: " + strikeCounter;
        ChangeStrike(); 
        isFishing = true;
        

    }
    
    public IEnumerator PlayMancing()
    {
        anim.speed = 1f;

        yield return new WaitForSeconds(0.125f);
        anim.Play("idle");
        yield return new WaitForSeconds(0.001f);
        anim.Play("mancing");
        yield return new WaitForSeconds(0.3f);
        anim.speed = 0f;
    }

    public void ChangeStrike()
    {
        StartCoroutine(PlayMancing());
        strikePos = Random.Range(-xLimit, xLimit);
        strike.rectTransform.anchoredPosition = new Vector3(strikePos, strike.rectTransform.anchoredPosition.y);
    }




}
