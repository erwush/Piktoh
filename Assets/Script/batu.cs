using UnityEngine;

// Membuat pilihan ukuran di Inspector
public enum UkuranBatu { Besar, Medium, Mini }

public class Batu : MonoBehaviour
{
    [Header("Stone Size Settings")]
    public UkuranBatu ukuranBatu;
    public float health = 5; 

    [Header("Drop Settings")]
    public GameObject itemHasilTambang; // Prefab item yang melayang di tanah
    public Item dataBatu;               // ScriptableObject data batunya
    public int jumlahDrop = 1;          // Jumlah item yang keluar

    [Header("Spawning Next Size")]
    // Jika batu BESAR hancur, masukkan prefab batu MEDIUM ke sini.
    // Jika batu MEDIUM hancur, masukkan prefab batu MINI ke sini.
    // Jika batu MINI hancur, kosongkan saja (isi null).
    public GameObject prefabUkuranBerikutnya; 
    public int jumlahPecahan = 2; // Berapa banyak batu lebih kecil yang muncul saat pecah

    public void ChangeHealth(float amount)
    {
        health += amount; 

        if (health <= 0)
        {
            Hancur();
        }
    }

    void Hancur()
    {
        // 1. Munculkan item drop (bisa diatur jumlahnya berdasarkan ukuran)
        if (itemHasilTambang != null)
        {
            for (int i = 0; i < jumlahDrop; i++)
            {
                // Beri sedikit random posisi agar item tidak menumpuk di satu titik
                Vector3 spawnPos = transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
                GameObject obj = Instantiate(itemHasilTambang, spawnPos, Quaternion.identity);
                
                if (obj.GetComponent<DroppedItem>() != null)
                {
                    obj.GetComponent<DroppedItem>().data = dataBatu;
                }
            }
        }

        // 2. Logika Pecah: Munculkan batu yang ukurannya lebih kecil
        if (prefabUkuranBerikutnya != null)
        {
            for (int i = 0; i < jumlahPecahan; i++)
            {
                Vector3 spawnPosBatu = transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
                Instantiate(prefabUkuranBerikutnya, spawnPosBatu, Quaternion.identity);
            }
        }

        // 3. Hancurkan objek batu ini sendiri
        Destroy(gameObject);
    }
}