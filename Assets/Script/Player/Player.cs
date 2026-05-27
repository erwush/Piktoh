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
    
    [Header("Combat Settings")]
    public float atkSpd = 0.5f;
    public float atk;
    public Transform atkPoint;
    public float atkRange;
    public LayerMask eLayer;
    private float atkTimer = 1.15f;

    [Header("Gathering Settings")]
    public float nebangSpd = 0.5f;
    public LayerMask treeLayer;
    private float nebangTimer;

    private BatangPanas hotbar;
    
    void Start()
    {
        health = maxHealth;
        energy = maxEnergy;
        hotbar = GetComponent<BatangPanas>();
    }

    void Update()
    {
        if (healthBar != null) healthBar.fillAmount = health / maxHealth;
        if (energyBar != null) energyBar.fillAmount = energy / maxEnergy;

        if (atkTimer > 0) atkTimer -= Time.deltaTime;
        if (nebangTimer > 0) nebangTimer -= Time.deltaTime;

        if (atkTimer <= 0 && Input.GetMouseButtonDown(0) && hotbar.activeSlot == 0)
        {
            atkTimer = atkSpd;
            Attack();
        } 

        if (nebangTimer <= 0 && Input.GetKeyDown(KeyCode.E) && hotbar.activeSlot == 1)
        {
            nebangTimer = nebangSpd;
            Nebang();
        }
    }

    void Attack()
    {
        ApplyDamage();
    }

    void Nebang()
    {
        ApplyNebang();
    }

    public void ChangeHealth(float amount)
    {
        health += maxHealth * amount;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void ChangeEnergy(float amount)
    {
        energy += amount;
        energy = Mathf.Clamp(energy, 0, maxEnergy);
    }

    void ApplyDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(atkPoint.position, atkRange, eLayer);
        
        foreach (Collider2D enemy in enemies)
        {
            Kriper kriperScript = enemy.GetComponent<Kriper>();
            if (kriperScript != null)
            {
                kriperScript.ChangeHealth(-atk);
            }
        }
    }
    
    void ApplyNebang()
    {
        Collider2D[] trees = Physics2D.OverlapCircleAll(atkPoint.position, atkRange, treeLayer);
        
        foreach (Collider2D tree in trees)
        {
            Pohon pohonScript = tree.GetComponent<Pohon>();
            if (pohonScript != null)
            {
                pohonScript.ChangeHealth(-1);
                ChangeEnergy(-5f);
                break;
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        if (atkPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(atkPoint.position, atkRange);
        }
    }
}