using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public float health = 33550336;
    public float maxHealth = 33550336;
    public float energy = 100;
    public float maxEnergy = 100;

    public Image healthBar;
    public Image energyBar;

    public Animator anim;

    [Header("Combat Settings")]
    public float atkSpd = 0.5f;
    public float atk;
    public Transform atkPoint;
    public float atkRange;
    public LayerMask eLayer;
    private float atkTimer = 1.15f;

    [Header("Gathering Settings (Tree)")]
    public float nebangSpd = 0.5f;
    public LayerMask treeLayer;
    private float nebangTimer;

    [Header("Gathering Settings (Mining)")]
    public float nambangSpd = 0.5f;
    public LayerMask stoneLayer;
    private float nambangTimer;

    [Header("Death & Respawn")]
    public Image blackScreen;
    public float fadeSpeed = 2f;
    public Transform respawnPoint;

    private bool isDead = false;

    private BatangPanas hotbar;

    void Start()
    {
        health = maxHealth;
        energy = maxEnergy;

        hotbar = GetComponent<BatangPanas>();
        anim = GetComponent<Animator>();

        if (blackScreen != null)
        {
            Color c = blackScreen.color;
            c.a = 0f;
            blackScreen.color = c;
        }
    }

    void Update()
    {
        if (healthBar != null)
            healthBar.fillAmount = health / maxHealth;

        if (energyBar != null)
            energyBar.fillAmount = energy / maxEnergy;

        if (!isDead && health <= 0)
        {
            StartCoroutine(DeathRoutine());
        }

        if (isDead)
            return;

        if (atkTimer > 0)
            atkTimer -= Time.deltaTime;

        if (nebangTimer > 0)
            nebangTimer -= Time.deltaTime;

        if (nambangTimer > 0)
            nambangTimer -= Time.deltaTime;

        // Slot 0 = Attack
        if (atkTimer <= 0 &&
            Input.GetMouseButtonDown(0) &&
            hotbar.activeSlot == 0)
        {
            atkTimer = atkSpd;
            Attack();
        }

        // Slot 1 = Tree
        if (nebangTimer <= 0 &&
            Input.GetKeyDown(KeyCode.E) &&
            hotbar.activeSlot == 1)
        {
            nebangTimer = nebangSpd;
            Nebang();
        }

        // Slot 2 = Stone
        if (nambangTimer <= 0 &&
            Input.GetKeyDown(KeyCode.E) &&
            hotbar.activeSlot == 2)
        {
            nambangTimer = nambangSpd;
            Nambang();
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

    void Nambang()
    {
        ApplyNambang();
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
        Collider2D[] enemies =
            Physics2D.OverlapCircleAll(
                atkPoint.position,
                atkRange,
                eLayer);

        foreach (Collider2D enemy in enemies)
        {
            StartCoroutine(AttackAnim());

            Kriper kriperScript =
                enemy.GetComponent<Kriper>();

            if (kriperScript != null)
            {
                kriperScript.ChangeHealth(-atk);
            }
        }
    }

    void ApplyNebang()
    {
        Collider2D[] trees =
            Physics2D.OverlapCircleAll(
                atkPoint.position,
                atkRange,
                treeLayer);

        foreach (Collider2D tree in trees)
        {
            StartCoroutine(NebangAnim());

            Pohon pohonScript =
                tree.GetComponent<Pohon>();

            if (pohonScript != null)
            {
                pohonScript.ChangeHealth(-1);
                ChangeEnergy(-5f);
                break;
            }
        }
    }

    void ApplyNambang()
    {
        Collider2D[] stones =
            Physics2D.OverlapCircleAll(
                atkPoint.position,
                atkRange,
                stoneLayer);

        foreach (Collider2D stone in stones)
        {
            StartCoroutine(NambangAnim());

            Batu batuScript =
                stone.GetComponent<Batu>();

            if (batuScript != null)
            {
                batuScript.ChangeHealth(-1);
                ChangeEnergy(-5f);
                break;
            }
        }
    }

    IEnumerator DeathRoutine()
    {
        isDead = true;

        // Fade In
        while (blackScreen.color.a < 1f)
        {
            Color c = blackScreen.color;
            c.a += fadeSpeed * Time.deltaTime;
            blackScreen.color = c;

            yield return null;
        }

        Respawn();

        yield return new WaitForSeconds(0.5f);

        // Fade Out
        while (blackScreen.color.a > 0f)
        {
            Color c = blackScreen.color;
            c.a -= fadeSpeed * Time.deltaTime;
            blackScreen.color = c;

            yield return null;
        }

        Color finalColor = blackScreen.color;
        finalColor.a = 0f;
        blackScreen.color = finalColor;

        isDead = false;
    }

    void Respawn()
    {
        transform.position = respawnPoint.position;

        health = maxHealth;
        energy = maxEnergy;

        // Tambahkan kode respawn lain di sini jika perlu
    }

    public IEnumerator AttackAnim()
    {
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.5f);

        anim.SetBool("isAttack", false);
    }

    public IEnumerator NebangAnim()
    {
        anim.SetBool("isNebang", true);

        yield return new WaitForSeconds(0.5f);

        anim.SetBool("isNebang", false);
    }

    public IEnumerator NambangAnim()
    {
        anim.SetBool("isNambang", true);

        yield return new WaitForSeconds(0.5f);

        anim.SetBool("isNambang", false);
    }

    void OnDrawGizmosSelected()
    {
        if (atkPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(
                atkPoint.position,
                atkRange);
        }
    }
}