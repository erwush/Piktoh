using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public float health = 33550336;
    public float maxHealth = 33550336;
    public float energy = 100;
    public float maxEnergy = 100;

    public Image healthBar;
    public Image energyBar;
    public Image sumurBar;

    public Animator anim;
    public float sumur;

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
    public TextMeshProUGUI textWarning;
    public GameObject warnObj;
    public float textTimer;
    public bool isShowing, isShowedOnce;

    void Start()
    {
        health = maxHealth;
        energy = maxEnergy;
        sumur = 0f;

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
        if (Input.GetKey(KeyCode.Slash) && Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(DeathRoutine());
        }
        if (Input.GetKey(KeyCode.Slash) && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(DeathRoutine());
            SceneManager.LoadScene(0);
        }
        if (Input.GetKey(KeyCode.Slash) && Input.GetKeyDown(KeyCode.H))
        {
            health = maxHealth;
            energy = maxEnergy;
            sumur = 5f;
        }
        if (Input.GetKey(KeyCode.Slash) && Input.GetKeyDown(KeyCode.X))
        {
            Questing.Instance.LaporkanProgress(Questing.Instance.indeksMisiAktif, 1);
        }
        
        

        if (health < (maxHealth * 0.5f) || energy <= 0)
        {
            // isShowedOnce = true;
            warnObj.SetActive(true);
            textWarning.text = "Makan sesuatu untuk memulihkan diri dan beraktivitas kembali";
            // isShowing = true;
            // textTimer = 0f;
        }
        
        else if(health > (maxHealth*0.5f) && energy > 0)
        {
            warnObj.SetActive(false);
            // isShowing = false;
            // isShowedOnce = false;
        }
        // if (isShowing && textTimer < 3)
        // {
            // textTimer += Time.deltaTime;
        // }
        // if(textTimer >= 3f)
        // {
            // warnObj.SetActive(false);
            // isShowing = false;
        // }
        if (healthBar != null)
            healthBar.fillAmount = health / maxHealth;

        if (energyBar != null)
            energyBar.fillAmount = energy / maxEnergy;

        sumurBar.fillAmount = (float)sumur / 5f;

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
            Input.GetKeyDown(KeyCode.F) &&
            hotbar.activeSlot == 1)
        {
            nebangTimer = nebangSpd;
            Nebang();
        }

        // Slot 2 = Stone
        if (nambangTimer <= 0 &&
            Input.GetKeyDown(KeyCode.F) &&
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
        if(Questing.Instance.daftarMisi[3].status == QuestStatus.Completed || Questing.Instance.daftarMisi[3].status == QuestStatus.Active)
        {
            ApplyNambang();
        }
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

    Collider2D GetClosestTarget(LayerMask layer)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(
            atkPoint.position,
            atkRange,
            layer);

        Collider2D closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D target in targets)
        {
            float distance = Vector2.Distance(
                atkPoint.position,
                target.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = target;
            }
        }

        return closest;
    }

    void ApplyDamage()
    {
        if (energy < 5f)
            return;

        Collider2D enemy = GetClosestTarget(eLayer);

        if (enemy == null)
            return;

        

        StartCoroutine(AttackAnim());

        Kriper kriperScript = enemy.GetComponent<Kriper>();

        if (kriperScript != null)
        {
            kriperScript.ChangeHealth(-atk);
            ChangeEnergy(-5f);
        }
    }

    void ApplyNebang()
    {
        if (energy < 5f)
            return;

        Collider2D tree = GetClosestTarget(treeLayer);

        if (tree == null)
            return;

        

        StartCoroutine(NebangAnim());

        Pohon pohonScript = tree.GetComponent<Pohon>();

        if (pohonScript != null)
        {
            pohonScript.ChangeHealth(-1);
            ChangeEnergy(-5f);
        }
    }

    void ApplyNambang()
    {
        if (energy < 5f)
            return;

        Collider2D stone = GetClosestTarget(stoneLayer);

        if (stone == null)
            return;

        energy -= 5f;

        StartCoroutine(NambangAnim());

        Batu batuScript = stone.GetComponent<Batu>();

        if (batuScript != null)
        {
            batuScript.ChangeHealth(-1);
            ChangeEnergy(-5f);
        }
    }

    IEnumerator DeathRoutine()
    {
        isDead = true;

        while (blackScreen.color.a < 1f)
        {
            Color c = blackScreen.color;
            c.a += fadeSpeed * Time.deltaTime;
            blackScreen.color = c;

            yield return null;
        }

        Respawn();

        yield return new WaitForSeconds(0.5f);

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
        anim.Play("macul");

        yield return new WaitForSeconds(0.1f);
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