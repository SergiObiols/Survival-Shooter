  using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    public GameObject DropLootPrefab;
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public int scoreHitValue;
    public ParticleSystem deathParticles;
    public AudioClip deathClip;

    GameObject _dropLootTarget;
    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    void Start()
    {
        _dropLootTarget = GameObject.FindGameObjectWithTag("DropLootTracker");
    }

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, RaycastHit hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;

        //ScoreManager.score += scoreHitValue;
            
        hitParticles.transform.position = hitPoint.point;
        hitParticles.Play();

        if(FloatingTextPrefab && currentHealth > 0)
        {
            ShowFloatingText();
        }

        if (currentHealth <= 0)
        {
            Death();
        }
        else
        {
            transform.position -= hitPoint.normal.normalized / 2;
        }
    }

    void ShowFloatingText()
    {
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = currentHealth.ToString();
    }

    void Death ()
    {
        isDead = true;

        if (deathParticles != null) deathParticles.Play();
        else transform.position = new Vector3(0,0,0);

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();

        for (int i = 0; i < startingHealth / 10; i++)
        {
            var go = Instantiate(DropLootPrefab, transform.position + new Vector3(0, Random.Range(0, 2)), Quaternion.identity);

            go.GetComponent<Follow>().Target = _dropLootTarget.transform;
        }
    }


    public void StartSinking ()
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
