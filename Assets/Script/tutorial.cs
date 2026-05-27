using UnityEngine;
using TMPro; 
using System.Collections.Generic; 

public class tutorial : MonoBehaviour
{
    public enum TipeTutorial { SingleKey, WASD }

    [Header("Settings Prefab")]
    public GameObject prefabTemplate; 
    public Vector3 offset = new Vector3(0, 2.5f, 0); 

    [Header("Konfigurasi Di Inspector")]
    public TipeTutorial jenisTutorial; 
    public string idTutorialUnik; 
    public string isiDeskripsi; 
    public string hurufTombol = "E"; 

    private static HashSet<string> daftarTutorialSelesai = new HashSet<string>();
    private GameObject activeUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (daftarTutorialSelesai.Contains(idTutorialUnik)) return;

            activeUI = Instantiate(prefabTemplate, other.transform.position + offset, Quaternion.identity, other.transform);

            // MENCARI PANEL
            Transform panelSingle = activeUI.transform.Find("Panel_Single");
            Transform panelWASD = activeUI.transform.Find("Panel_WASD");

            // KODE DETEKSI OTOMATIS: Kalau nama di prefab salah, bakal muncul peringatan ini
            if (panelSingle == null || panelWASD == null)
            {
                Debug.LogError("Pemberitahuan: Nama 'Panel_Single' atau 'Panel_WASD' di dalam Prefab lu belum sama persis dengan script! Cek huruf besar-kecil atau spasinya sob.");
                return; // Gagalkan script biar gak crash
            }

            // LOGIKA AKTIF/NONAKTIFKAN PANEL
            if (jenisTutorial == TipeTutorial.SingleKey)
            {
                panelSingle.gameObject.SetActive(true);
                panelWASD.gameObject.SetActive(false);
                
                panelSingle.Find("Image_Tombol/Text_Huruf").GetComponent<TextMeshProUGUI>().text = hurufTombol;
                panelSingle.Find("Text_Deskripsi").GetComponent<TextMeshProUGUI>().text = isiDeskripsi;
            }
            else if (jenisTutorial == TipeTutorial.WASD)
            {
                panelSingle.gameObject.SetActive(false); // Matikan yang single
                panelWASD.gameObject.SetActive(true);   // Nyalakan yang WASD
                panelWASD.Find("Text_Deskripsi").GetComponent<TextMeshProUGUI>().text = isiDeskripsi;
            }
        }
    }

    private void Update()
    {
        if (activeUI != null)
        {
            if (jenisTutorial == TipeTutorial.WASD)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    SelesaikanTutorial();
                }
            }
            else if (jenisTutorial == TipeTutorial.SingleKey)
            {
                if (Input.GetKeyDown(hurufTombol.ToLower()))
                {
                    SelesaikanTutorial();
                }
            }
        }
    }

    private void SelesaikanTutorial()
    {
        daftarTutorialSelesai.Add(idTutorialUnik); 
        Destroy(activeUI);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && activeUI != null)
        {
            Destroy(activeUI);
        }
    }
}