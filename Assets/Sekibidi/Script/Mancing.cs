using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
// using UnityEngine.UIElements;

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
    public Image progress;
    public bool canStrike;
    // public RectTransform rect;
    public TextMeshProUGUI[] text;
    public GameObject[] failIndicator;
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
            UpdateIndicator();
            strikeCounter++;
            progress.fillAmount = (float)strikeCounter / 3f;
            text[0].text = "Mantap";
            mancingAudio.Play();
            if (strikeCounter == 3)
            {
                mancingUI.SetActive(false);
                strikeCounter = 0;
                progress.fillAmount = (float)strikeCounter / 3f;
                isFishing = false;
                anim.speed = 1f;
                pleyer.GetComponent<Movement>().canMove = true;
                if(Questing.Instance.daftarMisi[8].status == QuestStatus.Active) Questing.Instance.LaporkanProgress(8, 1);
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
            UpdateIndicator();
            if (failCounter == 3)
            {
                failCounter = 0;
                UpdateIndicator();
                anim.speed = 1f;
                // mancingUI.SetActive(false);
                isFishing = false;
            }
        }

    }


    public void StartFishing()
    {
        pleyer.GetComponent<Movement>().StopMove();
        pleyer.GetComponent<Movement>().canMove = false;
        failCounter = 0;
        UpdateIndicator();
        strikeCounter = 0;
        progress.fillAmount = (float)strikeCounter / 3f;
        text[0].text = "Mancing Mania?";
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

    void UpdateIndicator()
    {
        for (int i = 0; i < failIndicator.Length; i++)
        {
            // aktif kalau index masih di bawah failCounter
            failIndicator[i].SetActive(i < failCounter);
        }
    }




}
