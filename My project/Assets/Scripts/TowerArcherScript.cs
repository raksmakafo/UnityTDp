using UnityEngine;

public class TowerArcherScript : MonoBehaviour
{

    public float Range = 10f;
    public float CurrCooldown, Cooldown;

    public GameObject Projectile; 
    private void Update()
    {
        if (CanShoot())
        {
            SearchTarget();
        }

        if (CurrCooldown > 0)
        {
            CurrCooldown -= Time.deltaTime;
        }
        

     }
    bool CanShoot()
    {
        if(CurrCooldown <= 0f)
        {
            return true;
        }   return false;
    }

    void SearchTarget()
    {
        Transform nearestEnemy = null;
        float nearestEnemyDistance = Mathf.Infinity;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float CurrDistance = Vector2.Distance(transform.position, enemy.transform.position);

            if (CurrDistance < nearestEnemyDistance && CurrDistance <= Range)
            {
                nearestEnemy = enemy.transform;
                nearestEnemyDistance = CurrDistance;
                
            }
        }
        if (nearestEnemy != null )
        {
            Shoot(nearestEnemy);
        }
    }

    void Shoot(Transform enemy)
    {
        CurrCooldown = Cooldown; // Reset the cooldown after shooting
        GameObject proj = Instantiate(Projectile);
        proj.transform.position = transform.position;
        //proj.GetComponent<TowerArcherArrowSCR>().SetTarget(enemy); // Assuming ProjectileScript has a Target property
        proj.transform.SetParent(null);
        proj.GetComponent<TowerArcherArrowSCR>().target = enemy;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
