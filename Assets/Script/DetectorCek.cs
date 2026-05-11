using UnityEngine;
using System.Collections;
using TMPro; // Wajib untuk mengontrol Text (TMP)

public class DetectorCek : MonoBehaviour
{
    [Header("Pengaturan Dasar")]
    public bool full = false;
    public int kayuDibutuhkan = 10;
    public int jumlahKayuYangSudahMasuk = 0; 
    public float intervalInput = 0.1f; 

    [Header("Referensi Objek")]
    public SpriteRenderer visualRumah; // Tarik objek 'Rumah' (Blok Cokelat) ke sini
    public Sprite gambarRumahJadi;    // Tarik file gambar rumah jadi ke sini
    public TextMeshPro teksProgres;   // Tarik objek 'Text (TMP)' ke sini

    private Coroutine prosesInput; 

    private void Start()
    {
        // Set tulisan awal agar tidak kosong saat game mulai
        if (teksProgres != null)
        {
            teksProgres.text = jumlahKayuYangSudahMasuk + " / " + kayuDibutuhkan + " Kayu";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah yang masuk adalah Player dan pembangunan belum selesai
        if (other.CompareTag("Player") && !full)
        {
            KarakterController karakter = other.GetComponent<KarakterController>();
            if (karakter != null && prosesInput == null)
            {
                prosesInput = StartCoroutine(AmbilKayuSatuPerSatu(karakter));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Hentikan proses jika Player keluar area
        if (other.CompareTag("Player")) 
        {
            StopInput();
        }
    }

    private void StopInput()
    {
        if (prosesInput != null)
        {
            StopCoroutine(prosesInput);
            prosesInput = null; 
            Debug.Log("Input Berhenti: Karakter menjauh.");
        }
    }

    IEnumerator AmbilKayuSatuPerSatu(KarakterController karakter)
    {
        while (jumlahKayuYangSudahMasuk < kayuDibutuhkan)
        {
            if (karakter.jumlahKayu > 0)
            {
                yield return new WaitForSeconds(intervalInput);

                // Kurangi kayu karakter, masukkan ke rumah
                karakter.jumlahKayu -= 1;
                jumlahKayuYangSudahMasuk += 1;

                // Update angka pada teks secara real-time
                if (teksProgres != null)
                {
                    teksProgres.text = jumlahKayuYangSudahMasuk + " / " + kayuDibutuhkan + " Kayu";
                }

                // Cek jika sudah penuh
                if (jumlahKayuYangSudahMasuk >= kayuDibutuhkan)
                {
                    SelesaikanPembangunan();
                    yield break;
                }
            }
            else 
            { 
                Debug.Log("Kayu di karakter habis!");
                prosesInput = null; 
                yield break; 
            }
        }
    }

    void SelesaikanPembangunan()
    {
        full = true;

        if (visualRumah != null && gambarRumahJadi != null)
        {
            // 1. Ganti gambarnya
            visualRumah.sprite = gambarRumahJadi;

            // 2. Reset scale ke 1 supaya ukurannya sesuai foto asli (tidak melar)
            visualRumah.transform.localScale = new Vector3(1f, 1f, 1f);

            // 3. Kembalikan warna ke putih agar gambar tidak terlihat cokelat/gelap
            visualRumah.color = Color.white;

            // 4. Pastikan mode gambarnya simple (tidak memotong)
            visualRumah.drawMode = SpriteDrawMode.Simple;
        }

        // Matikan teks agar tidak melayang di atas rumah yang sudah jadi
        if (teksProgres != null)
        {
            teksProgres.gameObject.SetActive(false);
        }

        // Matikan detektor biru (objek ini)
        Debug.Log("Pembangunan Selesai Jozz!");
        this.gameObject.SetActive(false); 
    }
}
