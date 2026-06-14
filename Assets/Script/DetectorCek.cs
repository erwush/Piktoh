using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class RequiredItem
{
    public Item item;
    public int requiredAmount;

    [HideInInspector] public int currentAmount;
}

public class DetectorCek : MonoBehaviour
{
    [Header("Requirement")]
    public List<RequiredItem> requiredItems = new();

    [Header("Tree Check")]
    public Transform treeParent;

    [Header("Setting")]
    public bool full = false;
    public float intervalInput = 0.1f;

    [Header("Reference")]
    public Inpentori inventory;
    public SpriteRenderer visualRumah;
    public Sprite gambarRumahJadi;
    public TextMeshPro teksProgres;
    public GameObject npc, rumah;

    private Coroutine prosesInput;

    void Start()
    {
        UpdateText();
        rumah.GetComponent<SpriteRenderer>().color=  new Color(1f, 1f, 1f, 0f);
    }

    bool SemuaPohonHilang()
    {
        if (treeParent == null)
            return true;

        return treeParent.childCount <= 0;
    }

    void UpdateText()
    {
        if (teksProgres == null)
            return;

        if (!SemuaPohonHilang())
        {
            teksProgres.text = "";
            rumah.GetComponent<Collider2D>().isTrigger = true;
            return;
        } else
        {
            rumah.GetComponent<Collider2D>().isTrigger = false;
        }

        System.Text.StringBuilder sb = new();

        for (int i = 0; i < requiredItems.Count; i++)
        {
            RequiredItem req = requiredItems[i];

            sb.Append(req.currentAmount);
            sb.Append("/");
            sb.Append(req.requiredAmount);
            sb.Append(" ");

            if (req.item != null)
                sb.Append(req.item.itemName);
            else
                sb.Append("Item");

            if (i < requiredItems.Count - 1)
                sb.AppendLine();
        }

        teksProgres.text = sb.ToString();
    }

    private void Update()
    {
        if (teksProgres != null)
            teksProgres.gameObject.SetActive(SemuaPohonHilang());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!SemuaPohonHilang())
            return;

        if (other.CompareTag("Player") && !full)
        {
            if (inventory != null && prosesInput == null)
            {
                prosesInput = StartCoroutine(InputItem());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopInput();
        }
    }

    void StopInput()
    {
        if (prosesInput != null)
        {
            StopCoroutine(prosesInput);
            prosesInput = null;
        }
    }

    IEnumerator InputItem()
    {
        while (!full)
        {
            bool adaProgress = false;

            foreach (var req in requiredItems)
            {
                if (req.currentAmount >= req.requiredAmount)
                    continue;

                int jumlahItem = inventory.GetItemCount(req.item);

                if (jumlahItem > 0)
                {
                    yield return new WaitForSeconds(intervalInput);

                    inventory.ReduceItem(req.item, 1);
                    req.currentAmount++;

                    adaProgress = true;

                    UpdateText();

                    if (SemuaRequirementTerpenuhi())
                    {
                        SelesaikanPembangunan();
                        yield break;
                    }

                    break;
                }
            }

            if (!adaProgress)
            {
                prosesInput = null;
                yield break;
            }
        }
    }

    bool SemuaRequirementTerpenuhi()
    {
        foreach (var req in requiredItems)
        {
            if (req.currentAmount < req.requiredAmount)
                return false;
        }

        return true;
    }

    void SelesaikanPembangunan()
    {
        full = true;




        if (teksProgres != null)
            teksProgres.gameObject.SetActive(false);

        if (Questing.Instance.daftarMisi[2].status == QuestStatus.Active)
            Questing.Instance.LaporkanProgress(2, 1);
        else if (Questing.Instance.daftarMisi[4].status == QuestStatus.Active)
            Questing.Instance.LaporkanProgress(4, 1);
        else if (Questing.Instance.daftarMisi[6].status == QuestStatus.Active)
            Questing.Instance.LaporkanProgress(6, 1);
        else if (Questing.Instance.daftarMisi[10].status == QuestStatus.Active)
            Questing.Instance.LaporkanProgress(10, 1);
        npc.SetActive(true);
        rumah.GetComponent<SpriteRenderer>().color=  new Color(1f, 1f, 1f, 1f);
        gameObject.SetActive(false);
    }
}


// using UnityEngine;
// using System.Collections;
// using TMPro; // Wajib untuk mengontrol Text (TMP)

// public class DetectorCek : MonoBehaviour
// {
//     [Header("Pengaturan Dasar")]
//     public bool full = false;
//     public int kayuDibutuhkan = 10;
//     public int jumlahKayuYangSudahMasuk = 0; 
//     public float intervalInput = 0.1f; 

//     [Header("Referensi Objek")]
//     public SpriteRenderer visualRumah; // Tarik objek 'Rumah' (Blok Cokelat) ke sini
//     public Sprite gambarRumahJadi;    // Tarik file gambar rumah jadi ke sini
//     public TextMeshPro teksProgres;   // Tarik objek 'Text (TMP)' ke sini

//     private Coroutine prosesInput; 

//     private void Start()
//     {
//         // Set tulisan awal agar tidak kosong saat game mulai
//         if (teksProgres != null)
//         {
//             teksProgres.text = jumlahKayuYangSudahMasuk + " / " + kayuDibutuhkan + " Kayu";
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         // Cek apakah yang masuk adalah Player dan pembangunan belum selesai
//         if (other.CompareTag("Player") && !full)
//         {
//             KarakterController karakter = other.GetComponent<KarakterController>();
//             if (karakter != null && prosesInput == null)
//             {
//                 prosesInput = StartCoroutine(AmbilKayuSatuPerSatu(karakter));
//             }
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         // Hentikan proses jika Player keluar area
//         if (other.CompareTag("Player")) 
//         {
//             StopInput();
//         }
//     }

//     private void StopInput()
//     {
//         if (prosesInput != null)
//         {
//             StopCoroutine(prosesInput);
//             prosesInput = null; 
//             Debug.Log("Input Berhenti: Karakter menjauh.");
//         }
//     }

//     IEnumerator AmbilKayuSatuPerSatu(KarakterController karakter)
//     {
//         while (jumlahKayuYangSudahMasuk < kayuDibutuhkan)
//         {
//             if (karakter.jumlahKayu > 0)
//             {
//                 yield return new WaitForSeconds(intervalInput);

//                 // Kurangi kayu karakter, masukkan ke rumah
//                 karakter.jumlahKayu -= 1;
//                 jumlahKayuYangSudahMasuk += 1;

//                 // Update angka pada teks secara real-time
//                 if (teksProgres != null)
//                 {
//                     teksProgres.text = jumlahKayuYangSudahMasuk + " / " + kayuDibutuhkan + " Kayu";
//                 }

//                 // Cek jika sudah penuh
//                 if (jumlahKayuYangSudahMasuk >= kayuDibutuhkan)
//                 {
//                     SelesaikanPembangunan();
//                     yield break;
//                 }
//             }
//             else 
//             { 
//                 Debug.Log("Kayu di karakter habis!");
//                 prosesInput = null; 
//                 yield break; 
//             }
//         }
//     }

//     void SelesaikanPembangunan()
//     {
//         full = true;

//         if (visualRumah != null && gambarRumahJadi != null)
//         {
//             // 1. Ganti gambarnya
//             visualRumah.sprite = gambarRumahJadi;

//             // 2. Reset scale ke 1 supaya ukurannya sesuai foto asli (tidak melar)
//             visualRumah.transform.localScale = new Vector3(1f, 1f, 1f);

//             // 3. Kembalikan warna ke putih agar gambar tidak terlihat cokelat/gelap
//             visualRumah.color = Color.white;

//             // 4. Pastikan mode gambarnya simple (tidak memotong)
//             visualRumah.drawMode = SpriteDrawMode.Simple;

//         }

//         // Matikan teks agar tidak melayang di atas rumah yang sudah jadi
//         if (teksProgres != null)
//         {
//             teksProgres.gameObject.SetActive(false);
//         }

//         // Matikan detektor biru (objek ini)
//         Debug.Log("Pembangunan Selesai Jozz!");
//         if (Questing.Instance.daftarMisi[2].status == QuestStatus.Active) Questing.Instance.LaporkanProgress(2, 1);
//         else if (Questing.Instance.daftarMisi[4].status == QuestStatus.Active) Questing.Instance.LaporkanProgress(4, 1);
//         else if (Questing.Instance.daftarMisi[6].status == QuestStatus.Active) Questing.Instance.LaporkanProgress(6, 1);
//         else if(Questing.Instance.daftarMisi[10].status == QuestStatus.Active) Questing.Instance.LaporkanProgress(10, 1);
//         this.gameObject.SetActive(false); 
//     }
// }
