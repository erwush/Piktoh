using UnityEngine;
using UnityEngine.UI;

public class ToggleVolumeIcon : MonoBehaviour
{
    public Sprite iconMute;      // Tarik gambar Mute ke sini
    private Sprite iconAwal;     // Untuk menyimpan gambar Speaker awal
    private Image imageKomponen; // Komponen Image pada objek ini
    private bool isMuted = false;

    void Start()
    {
        // Mengambil komponen Image dari objek itu sendiri
        imageKomponen = GetComponent<Image>();
        // Simpan gambar default agar bisa balik lagi
        iconAwal = imageKomponen.sprite;
    }

    public void GantiGambar()
    {
        isMuted = !isMuted; // Membalikkan status true/false

        if (isMuted) {
            imageKomponen.sprite = iconMute;
        } else {
            imageKomponen.sprite = iconAwal;
        }
    }
}