using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public GameObject UI;
    public GameObject[] Scroll;
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
        RectTransform rect = UI.GetComponent<RectTransform>();

        if (!isOpen && !YuAi.isOpen)
        {
            isOpen = true;
            YuAi.isOpen = true;
            while (rect.anchoredPosition.x > 770)
            {
                rect.anchoredPosition = new Vector2(
                    rect.anchoredPosition.x - 10,
                    rect.anchoredPosition.y
                );

                yield return null;
            }

            Scroll[0].GetComponent<Animator>().speed = 1;
            Scroll[0].GetComponent<Animator>().Play("ScrollOpen");
            Scroll[1].GetComponent<Animator>().speed = 1;
            Scroll[1].GetComponent<Animator>().Play("ScrollOpen");
            
            yield return new WaitForSeconds(0.75f);
            Scroll[1].GetComponent<Animator>().speed = 0;
            Scroll[0].GetComponent<Animator>().speed = 0;

        }
        else if (isOpen)
        {
            isOpen = false;
            YuAi.isOpen = false;
            Scroll[0].GetComponent<Animator>().speed = 1;
            Scroll[0].GetComponent<Animator>().Play("ScrollClose");
            Scroll[1].GetComponent<Animator>().speed = 1;
            Scroll[1].GetComponent<Animator>().Play("ScrollClose");
            yield return new WaitForSeconds(0.75f);
            Scroll[1].GetComponent<Animator>().speed = 0;
            Scroll[0].GetComponent<Animator>().speed = 0;

            while (rect.anchoredPosition.x < 1150)
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
