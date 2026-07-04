using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Turu : MonoBehaviour
{
    public Cycle cycle;
    public GameObject keybindUI;
    public Image blackScreen;

    [Header("Fade")]
    public float fadeSpeed = 2f;

    private bool playerInside;
    private bool isSleeping;

    private void Start()
    {
        if (keybindUI != null)
            keybindUI.SetActive(false);

        if (blackScreen != null)
        {
            Color c = blackScreen.color;
            c.a = 0f;
            blackScreen.color = c;
        }
    }

    private void Update()
    {
        bool canSleep =
            (cycle.hour >= 7 && cycle.hour < 19) ||
            cycle.hour >= 22 ||
            (cycle.hour < 5);

        if (playerInside &&
            !isSleeping &&
            canSleep &&
            Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(SleepRoutine());
        }

        if (playerInside && keybindUI != null)
        {
            keybindUI.SetActive(canSleep);
        }
    }

    IEnumerator SleepRoutine()
    {
        isSleeping = true;

        // Fade In
        while (blackScreen.color.a < 1f)
        {
            Color c = blackScreen.color;
            c.a += fadeSpeed * Time.deltaTime;
            blackScreen.color = c;

            yield return null;
        }

        Color fullBlack = blackScreen.color;
        fullBlack.a = 1f;
        blackScreen.color = fullBlack;

        // Tidur siang -> malam
        if (cycle.hour >= 7 && cycle.hour < 19)
        {
            cycle.time = cycle.duration * (19f / 24f);
            cycle.hour = 19;
            cycle.min = 0;

            if (cycle.cor == null)
            {
                cycle.cor = cycle.StartCoroutine(
                    cycle.GantiWaktu(Waktu.Malam)
                );
            }
        }
        // Tidur malam/dini hari -> pagi
        else
        {
            cycle.time = cycle.duration * (5f / 24f);
            cycle.hour = 5;
            cycle.min = 0;

            if (cycle.cor == null)
            {
                cycle.cor = cycle.StartCoroutine(
                    cycle.GantiWaktu(Waktu.Pagi)
                );
            }
        }

        yield return new WaitForSeconds(1f);

        // Fade Out
        while (blackScreen.color.a > 0f)
        {
            Color c = blackScreen.color;
            c.a -= fadeSpeed * Time.deltaTime;
            blackScreen.color = c;

            yield return null;
        }

        Color clear = blackScreen.color;
        clear.a = 0f;
        blackScreen.color = clear;

        isSleeping = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            bool canSleep =
                (cycle.hour >= 7 && cycle.hour < 19) ||
                cycle.hour >= 22 ||
                (cycle.hour < 5);

            if (keybindUI != null)
                keybindUI.SetActive(canSleep);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            if (keybindUI != null)
                keybindUI.SetActive(false);
        }
    }
}