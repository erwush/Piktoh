using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class Cycle : MonoBehaviour
{
    public float duration;
    public float time;
    public float day;
    public int hour;
    public int min;
    public TextMeshProUGUI timeText;
    public Waktu waktu;
    public Sprite[] clockSprite;
    public Image clockImg;
    public Image clockHand;
    public float clockSpd;
    public GameObject[] clockObj;
    public Coroutine cor;
    public GameObject[] clockBg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        duration = 720f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * clockSpd;
        if (time > duration)
        {
            time = 0;
            day++;
            hour = 0;
            min = 0;
        }
        float hourTime = (time / duration) * 24f;
        hour = (int)hourTime;
        min = (int)((hourTime - hour) * 60f);
        // timeText.text = "Day: " + day.ToString() + "<br>" + hour.ToString("D2") + ":" + min.ToString("D2");
        if (hour == 5)
        {
            if(cor == null)
            {
                cor = StartCoroutine(GantiWaktu(Waktu.Pagi));
            }
        }
        else if (hour == 11)
        {
            if (cor == null)
            {
                cor = StartCoroutine(GantiWaktu(Waktu.Siang));
            }
        }
        else if (hour == 15)
        {
            if (cor == null)
            {
                cor = StartCoroutine(GantiWaktu(Waktu.Sore));
            }
        }
        else if(hour == 18)
        {
            if (cor == null)
            {
                cor = StartCoroutine(GantiWaktu(Waktu.Malam));
            }
        }
        else
        {
            clockImg.sprite = clockSprite[1];
        }
        float hourAngle = (hour + min / 60f) * 30f;
        clockHand.rectTransform.localRotation = Quaternion.Euler(0, 0, 90f - hourAngle);


    }

    public IEnumerator GantiWaktu(Waktu newTime)
    {
        Debug.Log("duar");
        waktu = newTime;
        if (newTime == Waktu.Pagi)
        {
            clockBg[0].SetActive(true);
            clockBg[1].SetActive(false);
            clockObj[1].GetComponent<Animator>().Play("MtS");
            clockObj[1].GetComponent<Animator>().SetBool("isMalam", false);
            yield return new WaitForSeconds(0.5f);
            clockObj[0].SetActive(true);
            clockObj[0].GetComponent<Animator>().Play("MtS");
            yield return new WaitForSeconds(0.9f);
            clockObj[0].GetComponent<Animator>().SetBool("isSiang", true);


        }
        else if (newTime == Waktu.Sore)
        {
            clockBg[0].SetActive(false);
            clockBg[1].SetActive(true);
            clockObj[0].GetComponent<Animator>().Play("StM");
            clockObj[0].GetComponent<Animator>().SetBool("isSiang", false);
            
            yield return new WaitForSeconds(0.5f);
            clockObj[1].SetActive(true);
            clockObj[1].GetComponent<Animator>().Play("StM");
            yield return new WaitForSeconds(0.9f);
            clockObj[1].GetComponent<Animator>().SetBool("isMalam", true);
        }
        yield return new WaitForSeconds(5f);
        cor = null;
    }
}



public enum Waktu
{
    Pagi,
    Siang,
    Sore,
    Malam
}
