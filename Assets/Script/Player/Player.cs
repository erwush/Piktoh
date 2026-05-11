using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health = 33550336;
    public float maxHealth = 33550336;
    public float energy = 100;
    public float maxEnergy = 100;
    public Image healthBar;
    public Image energyBar;
    public float atkSpd;
    public float atk;
    public Transform atkPoint;
    public float atkRange;
    public LayerMask eLayer;
    private BatangPanas hotbar;
    private float atkTimer = 1.15f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        energy = maxEnergy;
        hotbar = GetComponent<BatangPanas>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / maxHealth;
        energyBar.fillAmount = energy / maxEnergy;
        if (atkTimer > 0)
        {
            atkTimer -= Time.deltaTime;
        }
        if (atkTimer <= 0 && Input.GetMouseButtonDown(0) && hotbar.activeSlot == 0)
        {
            atkTimer = atkSpd;
            Attack();
        }
    }

    void Attack()
    {
        ApplyDamage();

    }

    public void ChangeHealth(float amount)
    {
        health += maxHealth * amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health <= 0)
        {
            health = 0;
        }
    }
    
    public void ChangeEnergy(float amount)
    {
        energy += amount;
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }
        if (energy <= 0)
        {
            energy = 0;
        }
    }

    void ApplyDamage()
    {
        // Kita ambil semua collider tanpa filter layer dulu agar lebih pasti kena
        Collider2D[] objectsHit = Physics2D.OverlapCircleAll(atkPoint.position, atkRange);
        
        Debug.Log("<color=white>Player:</color> Menghantam " + objectsHit.Length + " objek.");

        foreach (Collider2D hit in objectsHit)
        {
            // JIKA YANG KENA ADALAH MUSUH
            if (hit.CompareTag("Enemy"))
            {
                Kriper enemyScript = hit.GetComponent<Kriper>();
                if (enemyScript != null)
                {
                    Debug.Log("<color=orange>Player:</color> Menyakiti Kriper!");
                    enemyScript.ChangeHealth(-atk);
                }
            }
            
            // JIKA YANG KENA ADALAH POHON
            if (hit.CompareTag("Tree"))
            {
                Pohon treeScript = hit.GetComponent<Pohon>();
                if (treeScript != null)
                {
                    Debug.Log("<color=cyan>Player:</color> Menebang Pohon!");
                    treeScript.ChangeHealth(-atk);
                    
                    // Opsional: kurangi energi kalau nebang pohon
                    ChangeEnergy(-5f); 
                }
            }
        }
    }

    // public void FinishAttack()
    // // {

    // //     if (atkTimer > 0 || !Input.GetMouseButtonDown(0))
    // //     {

    // //     }


    // // }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(atkPoint.position, atkRange);
    }

}
