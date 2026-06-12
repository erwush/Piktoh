using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Questing : MonoBehaviour
{
    // Singleton: Supaya script lain bisa panggil lewat 'Questing.Instance'
    public static Questing Instance;
    public Animator notif;
    public bool isNotif;

    public List<Quest> daftarMisi = new List<Quest>(); 
    public int indeksMisiAktif = 0;
    public QuestUI questUI; 

    void Awake()
    {
        // Inisialisasi Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (daftarMisi == null || daftarMisi.Count == 0) return;
        InisialisasiMisi();
        TampilkanMisiAktif();
    }

    void InisialisasiMisi()
    {
        for (int i = 0; i < daftarMisi.Count; i++)
        {
            if (daftarMisi[i] != null)
                daftarMisi[i].status = (i == 0) ? QuestStatus.Active : QuestStatus.Locked;
        }
    }

    public void TampilkanMisiAktif()
    {
        if (questUI == null) return;

        if (indeksMisiAktif >= daftarMisi.Count)
        {
            questUI.UpdateTeksMisi("Tamat", "Semua misi telah diselesaikan!");
            return;
        }

        Quest misiSekarang = daftarMisi[indeksMisiAktif];
        if (misiSekarang != null)
        {
            questUI.UpdateTeksMisi(misiSekarang.displayName, misiSekarang.desc);
        }
    }

    // ========================================================
    // SATU-SATUNYA FUNGSI UNTUK MENERIMA LAPORAN DARI MANAPUN
    // ========================================================
    public void LaporkanProgress(int idAksi, int jumlah = 1)
{
    if (indeksMisiAktif >= daftarMisi.Count) return;

    Quest quest = daftarMisi[indeksMisiAktif];

    if (quest.questId != idAksi) return;

        quest.currentAmount += jumlah;

    if (quest.currentAmount >= quest.targetAmount)
    {
        SelesaikanMisiAktif();
    }
}

    private void SelesaikanMisiAktif()
    {
        daftarMisi[indeksMisiAktif].status = QuestStatus.Completed;
        indeksMisiAktif++;

        if (indeksMisiAktif < daftarMisi.Count)
        {
            daftarMisi[indeksMisiAktif].status = QuestStatus.Active;
            Debug.Log("Misi baru terbuka!");
        }
        else
        {
            Debug.Log("Selamat! Seluruh rangkaian misi selesai!");
        }
        StartCoroutine(AnimasiNotif());
        TampilkanMisiAktif();
    }
    
    private IEnumerator AnimasiNotif()
    {
        notif.Play("Turun");
        yield return new WaitForSeconds(0.5f);
        notif.speed = 0f;
        isNotif = true;
        if (questUI.isOpen)
        {
            yield return new WaitForSeconds(1.5f);
            notif.speed = 1f;
            notif.Play("Naik");
            yield return new WaitForSeconds(0.5f);
            notif.speed = 0f;
            isNotif = false;
        }
    }
}


// Contoh tambahan untuk misi mengumpulkan kayu
// public int jumlahKayu = 0;

// public void TambahKayu()
// {
//     jumlahKayu++;
//     if (jumlahKayu >= 5) 
//     {
//         // Lapor kalau misi mengumpulkan kayu sudah beres syaratnya
//         Questing.Instance.LaporkanProgress("KUMPULKAN_KAYU_CUKUP");
//     }
// }