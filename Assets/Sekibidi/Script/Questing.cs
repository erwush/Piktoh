// File: QuestManager.cs
using UnityEngine;
using System.Collections.Generic;

public class Questing : MonoBehaviour
{
    // Daftar semua misi (diisi lewat Inspector Unity sesuai urutan)
    public List<Quest> daftarMisi = new List<Quest>();

    // Menandai urutan misi yang sedang berjalan (0 = misi pertama, 1 = misi kedua, dst)
    public int indeksMisiAktif = 0;

    void Start()
    {
        // Di awal game, pastikan semua misi di-set Locked, kecuali misi pertama
        InisialisasiMisi();
        TampilkanMisiAktif();
    }

    // Fungsi untuk mengatur keadaan awal semua misi
    void InisialisasiMisi()
    {
        for (int i = 0; i < daftarMisi.Count; i++)
        {
            if (i == 0)
            {
                daftarMisi[i].status = QuestStatus.Active; // Misi pertama langsung aktif
            }
            else
            {
                daftarMisi[i].status = QuestStatus.Locked; // Misi sisanya dikunci dulu
            }
        }
    }

    // Fungsi untuk menampilkan/melihat misi yang SEKARANG sedang berjalan
    public void TampilkanMisiAktif()
    {
        // Cek dulu apakah semua misi sudah habis diselesaikan
        if (indeksMisiAktif >= daftarMisi.Count)
        {
            Debug.Log("SEMUA MISI SUDAH SELESAI! Tidak ada misi baru.");
            return;
        }

        // Ambil data misi berdasarkan indeks yang aktif saat ini
        Quest misiSekarang = daftarMisi[indeksMisiAktif];

        // Debug.Log("=== MISI SAAT INI ===");
        // Debug.Log("Nama Misi: " + misiSekarang.questName);
        // Debug.Log("Deskripsi: " + misiSekarang.questDescription);
        // Debug.Log("Status: " + misiSekarang.status);
        // Debug.Log("=====================");
    }

    // Fungsi yang dipanggil saat player berhasil menyelesaikan misi aktif
    public void SelesaikanMisiAktif()
    {
        // Pengaman jika memanggil fungsi ini padahal game sudah tamat
        if (indeksMisiAktif >= daftarMisi.Count) return;

        // 1. Ubah status misi saat ini menjadi Completed (Selesai)
        daftarMisi[indeksMisiAktif].status = QuestStatus.Completed;
        // Debug.Log("Misi '" + daftarMisi[indeksMisiAktif].questName + "' UDAH SELESAI!");

        // 2. Naikkan indeks untuk lanjut ke misi berikutnya
        indeksMisiAktif++;

        // 3. Aktifkan misi berikutnya jika masih ada di dalam list
        if (indeksMisiAktif < daftarMisi.Count)
        {
            daftarMisi[indeksMisiAktif].status = QuestStatus.Active;
            Debug.Log("Misi baru terbuka!");

            // Tampilkan misi baru yang sekarang aktif
            TampilkanMisiAktif();
        }
        else
        {
            Debug.Log("Selamat! Kamu sudah menyelesaikan seluruh rangkaian misi di game ini!");
        }
    }
}

// using System.Collections;
// using TMPro;
// using UnityEngine;

// public class Questing : MonoBehaviour
// {
//     public QuestUI UI;
//     public Quest activeQuest;
//     public GameObject questNotif;
   
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {

//     }

//     public IEnumerator QuestChange()
//     {
//         Animator anim = questNotif.GetComponent<Animator>();
//         anim.speed = 1f;
//         anim.Play("Turun");
//         yield return new WaitForSeconds(0.5f);
//         anim.speed = 0f;
//     }

    
// }


