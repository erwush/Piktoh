using UnityEngine;

public class Turu : MonoBehaviour
{
    public Cycle cycle;
    public GameObject keybindUI;

    private bool playerInside;

    private void Start()
    {
        if (keybindUI != null)
            keybindUI.SetActive(false);
    }

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E) && cycle.hour >= 22)
        {
            Sleep();
        }
    }

    private void Sleep()
    {
        // Tambah hari
        cycle.day++;

        // Set ke jam 05:00 pagi
        cycle.time = cycle.duration * (5f / 24f);

        // Update status waktu
        cycle.hour = 5;
        cycle.min = 0;

        Debug.Log("Player tidur, hari berikutnya dimulai.");

        // Paksa ganti ke pagi
        if (cycle.cor == null)
        {
            cycle.cor = cycle.StartCoroutine(
                cycle.GantiWaktu(Waktu.Pagi)
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            if (keybindUI != null && cycle.hour >= 22)
                keybindUI.SetActive(true);
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