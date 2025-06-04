using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform exit;
    public float speed = 2f;
    [SerializeField]
    Transform[] wayPoints;
    [SerializeField]
    int health;
    [SerializeField]
    int rewardAmount;

    private int target = 0;
    Collider2D enemyCollider;
    Transform enemy;
    bool isDead = false;

    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }


    void Start()
    {
        enemy = GetComponent<Transform>();
        enemyCollider = GetComponent<Collider2D>();
        Manager.Instance.RegisterEnemy(this);

        // Находим все точки пути с тегом
        GameObject[] waypointsGO = GameObject.FindGameObjectsWithTag("MoveingPoint");
        wayPoints = new Transform[waypointsGO.Length];

        // Сортировка по индексу WaypointIndex
        System.Array.Sort(waypointsGO, (a, b) =>
            a.GetComponent<WaypointIndex>().index.CompareTo(
            b.GetComponent<WaypointIndex>().index)
        );

        for (int i = 0; i < waypointsGO.Length; i++)
        {
            wayPoints[i] = waypointsGO[i].transform;
        }
    }

    void Update()
    {
        if (isDead || wayPoints == null || wayPoints.Length == 0)
            return;

        if (target < wayPoints.Length)
        {
            MoveTo(wayPoints[target]);
            if (Vector2.Distance(enemy.position, wayPoints[target].position) < 0.1f)
            {
                target++;
            }
        }
        else
        {
            MoveTo(exit);
            if (Vector2.Distance(enemy.position, exit.position) < 0.1f)
            {
                Manager.Instance.minusHeart(1);
                Manager.Instance.RoundEscaped += 1;
                Manager.Instance.TotalEscaped += 1;
                Manager.Instance.UnregisterEnemy(this);
                Manager.Instance.IsWaveOver();
                Destroy(gameObject);
            }
        }
    }


    void MoveTo(Transform destination)
    {
        enemy.position = Vector2.MoveTowards(enemy.position, destination.position, speed * Time.deltaTime);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            var projectileComponent = collision.GetComponent<Projectile>();

            if (projectileComponent != null)
            {
                Debug.Log($"Hit by projectile: {collision.name} with damage {projectileComponent.AttackDamage}");
                EnemyHit(projectileComponent.AttackDamage);
                Destroy(collision.gameObject);
            }
        }
    }




    public void EnemyHit(int hitPoints)
    {
        if (health - hitPoints > 0)
        {
            health -= hitPoints;
            //hurt

        }
        else
        {
            //die
            Die();


        }

    }

    public void Die()
    {

        isDead = true;
        Manager.Instance.TotalKilled += 1;
        Manager.Instance.IsWaveOver();
        Manager.Instance.addMoney(rewardAmount);
        Manager.Instance.UnregisterEnemy(this); // убрать из списка
        Destroy(gameObject); // полностью удалить из сцены (если нужно)
    }


    public void ScaleHealth(float multiplier)
    {
        health = Mathf.CeilToInt(health * multiplier);
    }
}