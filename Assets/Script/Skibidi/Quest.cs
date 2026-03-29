using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public Image UI;
    public GameObject UII;
    public bool isOpen;
    private bool isMoving;
    private Coroutine cor;
    private YuAi YuAi;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        YuAi = GameObject.Find("Mekanik").GetComponent<YuAi>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q) && cor == null)
        {
            cor = StartCoroutine(MoveUI());
        }
    }

    IEnumerator MoveUI()
    {
        RectTransform rect = UI.rectTransform;

        if (!isOpen && !YuAi.isOpen)
        {
            isOpen = true;
            YuAi.isOpen = true;
            while (rect.anchoredPosition.x > 720)
            {
                rect.anchoredPosition = new Vector2(
                    rect.anchoredPosition.x - 10,
                    rect.anchoredPosition.y
                );

                yield return null;
            }
            UII.SetActive(true);
            UII.GetComponent<Animator>().speed = 1;
            UII.GetComponent<Animator>().Play("Questest");
            yield return new WaitForSeconds(1.0f);
            UII.GetComponent<Animator>().speed = 0;

        }
        else if (isOpen)
        {
            isOpen = false;
            YuAi.isOpen = false;
            UII.GetComponent<Animator>().speed = 1;
            UII.GetComponent<Animator>().Play("Questest2");
            yield return new WaitForSeconds(1.0f);
            UII.GetComponent<Animator>().speed = 0;
            UII.SetActive(false);
            while (rect.anchoredPosition.x < 1200)
            {
                rect.anchoredPosition = new Vector2(
                    rect.anchoredPosition.x + 10,
                    rect.anchoredPosition.y
                );

                yield return null;
            }



        }

        cor = null;
    }

}
