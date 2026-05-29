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

    // TAMBAHKAN INI: Fitur cek item dinamis dari Inspector
    [Header("Kondisi Pegang Item")]
    public bool butuhItemSpesifik = false;
    [Tooltip("0=Pedang, 1=Kapak, 2=Beliung, 3=Cangkul, 4=Pancingan")]
    public int idSlotHarusDipegang = 0;

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
            
            panelSingle.Find("Image/Text (TMP)").GetComponent<TextMeshProUGUI>().text = hurufTombol;
            panelSingle.Find("deskripsi").GetComponent<TextMeshProUGUI>().text = isiDeskripsi;
        }
        else if (jenisTutorial == TipeTutorial.WASD)
        {
            panelSingle.gameObject.SetActive(false);
            panelWASD.gameObject.SetActive(true);

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
                // 1. Ambil slot aktif dari BatangPanas yang sudah diperbaiki
                int slotGenggam = BatangPanas.instance != null ? BatangPanas.instance.activeSlot : -1;

                // 2. LOGIKA BARU: Jika butuh item tapi slot player tidak pas, KODE DI BAWAHNYA DIABAIKAN (Tutorial Gak Bakal Ilang!)
                if (butuhItemSpesifik && slotGenggam != idSlotHarusDipegang)
                {
                    return; 
                }

                // 3. Jika slot sudah sesuai (atau tidak butuh item), cek tombol eksekusi
                if (idTutorialUnik == "TutorSerangBabi") 
                {
                    if (Input.GetMouseButtonDown(0)) SelesaikanTutorial();
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