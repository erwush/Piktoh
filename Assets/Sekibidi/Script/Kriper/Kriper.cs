using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Kriper : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float[] detectRange;
    public Rigidbody2D rb;
    public GameObject itemDrop;
    public Item itemData;
    public Material hitFlash;
    public Transform detectPoint;
    public Transform target;
    private SpriteRenderer selfSprite;
    public LayerMask pLayer;
    public float atkRange;
    public float atk;
    public float spd;
    public float atkSpd;
    private KriperCombat combat;
    public Image healthBar;
    private KriperMovement mvm;    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        combat = GetComponent<KriperCombat>();
        mvm = GetComponent<KriperMovement>();
        selfSprite = GetComponent<SpriteRenderer>();
        FindFirstObjectByType<Archer2>().AddTarget(transform);
    }

    // Update is called once per frame
    void Update()
    {
 if (healthBar != null) healthBar.fillAmount = health / maxHealth;
    }

    public void ChangeHealth(float amount)
    {
        health += amount;
        if (amount < 0)
        {
            StartCoroutine(HurtAnim());
        }
        if (health > maxHealth)
        { health = maxHealth; }
        if (health <= 0)
        {
            GameObject drop = Instantiate(itemDrop, transform.position, Quaternion.identity);
            drop.GetComponent<DroppedItem>().data = itemData;
            if (Questing.Instance.daftarMisi[5].status == QuestStatus.Active) Questing.Instance.LaporkanProgress(5, 1);
            FindFirstObjectByType<Archer2>().RemoveTarget(transform);
            Destroy(gameObject);
        }
        if(amount < 0)
        {
            mvm.target = mvm.player;
            mvm.isTargetingPlayer = true;
            combat.isTargetingPlayer = true;
            mvm.ChangeState(EnemyState.Idle);
            combat.targetLayer = combat.layer[1];
        }

    }

    public IEnumerator HurtAnim()
    {
        Material defaultMaterial = selfSprite.material;
        selfSprite.material = hitFlash;
        yield return new WaitForSeconds(0.2f);
        selfSprite.material = defaultMaterial;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().ChangeHealth(-0.2f);

        }
    }

    
}
