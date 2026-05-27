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

    [Header("Sistem Rantai (Urutan)")]
    public tutorial tutorialBerikutnya;

    private static HashSet<string> daftarTutorialSelesai = new HashSet<string>();
    private GameObject activeUI;
    private Transform savedPlayerTransform; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            savedPlayerTransform = other.transform;
            MulaiTutorial(savedPlayerTransform);
        }
    }

    public void MulaiTutorial(Transform player)
    {
        if (daftarTutorialSelesai.Contains(idTutorialUnik)) return;
        if (activeUI != null) return; 

        savedPlayerTransform = player;
        activeUI = Instantiate(prefabTemplate, player.position + offset, Quaternion.identity, player);

        Transform panelSingle = activeUI.transform.Find("Panel_Single");
        Transform panelWASD = activeUI.transform.Find("Panel_WASD");

        if (panelSingle == null || panelWASD == null)
        {
            Debug.LogError("Nama 'Panel_Single' atau 'Panel_WASD' di prefab belum sesuai!");
            return; 
        }

        if (jenisTutorial == TipeTutorial.SingleKey)
        {
            panelSingle.gameObject.SetActive(true);
            panelWASD.gameObject.SetActive(false);
            
            // SUDAH DISESUAIKAN: Mencari 'Image/Text (TMP)' dan 'deskripsi' sesuai gambar prefab lu
            panelSingle.Find("Image/Text (TMP)").GetComponent<TextMeshProUGUI>().text = hurufTombol;
            panelSingle.Find("deskripsi").GetComponent<TextMeshProUGUI>().text = isiDeskripsi;
        }
        else if (jenisTutorial == TipeTutorial.WASD)
        {
            panelSingle.gameObject.SetActive(false);
            panelWASD.gameObject.SetActive(true);

            // SUDAH DISESUAIKAN: Mencari 'deskripsi' di dalam Panel_WASD sesuai gambar prefab lu
            panelWASD.Find("deskripsi").GetComponent<TextMeshProUGUI>().text = isiDeskripsi;
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
                int slotGenggam = BatangPanas.instance != null ? BatangPanas.instance.activeSlot : -1;

                if (idTutorialUnik == "TutorTebang") 
                {
                    if (Input.GetKeyDown(KeyCode.E) && slotGenggam == 1) SelesaikanTutorial();
                }
                else if (idTutorialUnik == "TutorTanam" || idTutorialUnik == "TutorCangkul") 
                {
                    if (Input.GetKeyDown(KeyCode.E) && slotGenggam == 3) SelesaikanTutorial();
                }
                else if (idTutorialUnik == "TutorMancing") 
                {
                    if (Input.GetKeyDown(KeyCode.E) && slotGenggam == 4) SelesaikanTutorial();
                }
                else if (idTutorialUnik == "TutorSerangBabi") 
                {
                    if (Input.GetMouseButtonDown(0) && slotGenggam == 0) SelesaikanTutorial();
                }
                else if (idTutorialUnik == "TutorRumah") 
                {
                    if (Input.GetKeyDown(KeyCode.E)) SelesaikanTutorial();
                }
                else 
                {
                    if (Input.GetKeyDown(hurufTombol.ToLower())) SelesaikanTutorial();
                }
            }
        }
    }

    private void SelesaikanTutorial()
    {
        daftarTutorialSelesai.Add(idTutorialUnik); 
        Destroy(activeUI);

        if (tutorialBerikutnya != null && savedPlayerTransform != null)
        {
            tutorialBerikutnya.MulaiTutorial(savedPlayerTransform);
        }
    }

    public static void SelesaikanMekanikLuar(string idID)
    {
        if (!daftarTutorialSelesai.Contains(idID))
        {
            daftarTutorialSelesai.Add(idID);
            
            tutorial[] semuaTutorDiScene = FindObjectsByType<tutorial>(FindObjectsSortMode.None);
            foreach (tutorial t in semuaTutorDiScene)
            {
                if (t.idTutorialUnik == idID && t.activeUI != null)
                {
                    Destroy(t.activeUI);

                    if (t.tutorialBerikutnya != null && t.savedPlayerTransform != null)
                    {
                        t.tutorialBerikutnya.MulaiTutorial(t.savedPlayerTransform);
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && activeUI != null)
        {
            Destroy(activeUI); 
        }
    }
}