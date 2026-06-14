using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class Cycle : MonoBehaviour
{
    public float duration;
    public float time;
    public float day;

    public GameObject babiSpawn;
    public GameObject[] lokasiBabi;
    private bool babiSpawnedToday;

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

    [Header("Global Light 2D")]
    public Light2D globalLight;

    [Header("Light Colors")]
    public Color pagiColor = new Color(1f, 0.95f, 0.85f);
    public Color siangColor = Color.white;
    public Color soreColor = new Color(1f, 0.75f, 0.5f);
    public Color malamColor = new Color(0.4f, 0.5f, 1f);

    [Header("Light Intensities")]
    public float pagiIntensity = 0.8f;
    public float siangIntensity = 1.2f;
    public float soreIntensity = 0.7f;
    public float malamIntensity = 0.3f;

    public float lightTransitionDuration = 2f;

    void Start()
    {
        duration = 720f;
    }

    void Update()
    {
        time += Time.deltaTime * clockSpd;

        if (time > duration)
        {
            time = 0;
            day++;
            hour = 0;
            min = 0;

            babiSpawnedToday = false;
        }

        float hourTime = (time / duration) * 24f;
        hour = (int)hourTime;
        min = (int)((hourTime - hour) * 60f);

        // Spawn 3 babi jam 21:00 (sekali per hari)
        if (hour >= 21 && !babiSpawnedToday && Questing.Instance.daftarMisi[2].status == QuestStatus.Completed)
        {
            SpawnBabi();
            babiSpawnedToday = true;
        }

        //timeText.text = "Day: " + day + "<br>" + hour.ToString("D2") + ":" + min.ToString("D2");

        if (hour == 5)
        {
            if (cor == null)
                cor = StartCoroutine(GantiWaktu(Waktu.Pagi));
        }
        else if (hour == 11)
        {
            if (cor == null)
                cor = StartCoroutine(GantiWaktu(Waktu.Siang));
        }
        else if (hour == 15)
        {
            if (cor == null)
                cor = StartCoroutine(GantiWaktu(Waktu.Sore));
        }
        else if (hour == 18)
        {
            if (cor == null)
                cor = StartCoroutine(GantiWaktu(Waktu.Malam));
        }
        else
        {
            clockImg.sprite = clockSprite[1];
        }

        float hourAngle = (hour + min / 60f) * 30f;
        clockHand.rectTransform.localRotation =
            Quaternion.Euler(0, 0, 90f - hourAngle);
    }

    void SpawnBabi()
    {
        for (int i = 0; i < 3; i++)
        {
            int r = Random.Range(0, lokasiBabi.Length);
            int x = Random.Range(0, 3);
            int y = Random.Range(0, 3);
            Vector3 pos = lokasiBabi[r].transform.position + new Vector3(x, y, 0);
            Instantiate(babiSpawn, pos, Quaternion.identity);
        }

    }

    IEnumerator LerpLight(Color targetColor, float targetIntensity)
    {
        if (globalLight == null)
            yield break;

        Color startColor = globalLight.color;
        float startIntensity = globalLight.intensity;

        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime / lightTransitionDuration;

            globalLight.color = Color.Lerp(startColor, targetColor, t);
            globalLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);

            yield return null;
        }

        globalLight.color = targetColor;
        globalLight.intensity = targetIntensity;
    }

    public IEnumerator GantiWaktu(Waktu newTime)
    {
        Debug.Log("duar");

        waktu = newTime;

        if (newTime == Waktu.Pagi)
        {
            StartCoroutine(LerpLight(pagiColor, pagiIntensity));

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
        else if (newTime == Waktu.Siang)
        {
            StartCoroutine(LerpLight(siangColor, siangIntensity));
        }
        else if (newTime == Waktu.Sore)
        {
            StartCoroutine(LerpLight(soreColor, soreIntensity));

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
        else if (newTime == Waktu.Malam)
        {
            StartCoroutine(LerpLight(malamColor, malamIntensity));
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

// using UnityEngine;
// using TMPro;
// using System.Collections;
// using UnityEngine.UI;

// public class Cycle : MonoBehaviour
// {
//     public float duration;
//     public float time;
//     public float day;
//     public int hour;
//     public int min;
//     public TextMeshProUGUI timeText;
//     public Waktu waktu;
//     public Sprite[] clockSprite;
//     public Image clockImg;
//     public Image clockHand;
//     public float clockSpd;
//     public GameObject[] clockObj;
//     public Coroutine cor;
//     public GameObject[] clockBg;
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         duration = 720f;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         time += Time.deltaTime * clockSpd;
//         if (time > duration)
//         {
//             time = 0;
//             day++;
//             hour = 0;
//             min = 0;
//         }
//         float hourTime = (time / duration) * 24f;
//         hour = (int)hourTime;
//         min = (int)((hourTime - hour) * 60f);
//         // timeText.text = "Day: " + day.ToString() + "<br>" + hour.ToString("D2") + ":" + min.ToString("D2");
//         if (hour == 5)
//         {
//             if(cor == null)
//             {
//                 cor = StartCoroutine(GantiWaktu(Waktu.Pagi));
//             }
//         }
//         else if (hour == 11)
//         {
//             if (cor == null)
//             {
//                 cor = StartCoroutine(GantiWaktu(Waktu.Siang));
//             }
//         }
//         else if (hour == 15)
//         {
//             if (cor == null)
//             {
//                 cor = StartCoroutine(GantiWaktu(Waktu.Sore));
//             }
//         }
//         else if(hour == 18)
//         {
//             if (cor == null)
//             {
//                 cor = StartCoroutine(GantiWaktu(Waktu.Malam));
//             }
//         }
//         else
//         {
//             clockImg.sprite = clockSprite[1];
//         }
//         float hourAngle = (hour + min / 60f) * 30f;
//         clockHand.rectTransform.localRotation = Quaternion.Euler(0, 0, 90f - hourAngle);


//     }

//     public IEnumerator GantiWaktu(Waktu newTime)
//     {
//         Debug.Log("duar");
//         waktu = newTime;
//         if (newTime == Waktu.Pagi)
//         {
//             clockBg[0].SetActive(true);
//             clockBg[1].SetActive(false);
//             clockObj[1].GetComponent<Animator>().Play("MtS");
//             clockObj[1].GetComponent<Animator>().SetBool("isMalam", false);
//             yield return new WaitForSeconds(0.5f);
//             clockObj[0].SetActive(true);
//             clockObj[0].GetComponent<Animator>().Play("MtS");
//             yield return new WaitForSeconds(0.9f);
//             clockObj[0].GetComponent<Animator>().SetBool("isSiang", true);


//         }
//         else if (newTime == Waktu.Sore)
//         {
//             clockBg[0].SetActive(false);
//             clockBg[1].SetActive(true);
//             clockObj[0].GetComponent<Animator>().Play("StM");
//             clockObj[0].GetComponent<Animator>().SetBool("isSiang", false);
            
//             yield return new WaitForSeconds(0.5f);
//             clockObj[1].SetActive(true);
//             clockObj[1].GetComponent<Animator>().Play("StM");
//             yield return new WaitForSeconds(0.9f);
//             clockObj[1].GetComponent<Animator>().SetBool("isMalam", true);
//         }
//         yield return new WaitForSeconds(5f);
//         cor = null;
//     }
// }



// public enum Waktu
// {
//     Pagi,
//     Siang,
//     Sore,
//     Malam
// }
