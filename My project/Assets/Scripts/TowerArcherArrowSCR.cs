/*using UnityEngine;

public class TowerArcherArrowSCR : MonoBehaviour
{

    public Transform target;
    float speed = 10f; 
    int damage = 10; 

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Без цели стрела уничтожается
            return;
        }
        Move();
    }

    public void SetTarget(Transform enemy)
    {
        target = enemy;
    }

    private void Move()
    {
        
        if (target != null)
        {
            if(Vector2.Distance(transform.position, target.position)< .1f)
            {
                target.GetComponent<EnemyScr>().TakeDamage(damage); // Assuming the enemy has a method to take damage
                Destroy(gameObject); // Destroy the arrow 
            }
            else
            {
                Vector2 dir = target.position - transform.position;
                transform.Translate(dir.normalized * Time.deltaTime * speed); // Adjust speed as needed
            }
                
        }
        else
        {
            Destroy(gameObject); // Destroy the arrow if no target is set
        }
    }

}
*/