using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float attackRadius;
    [SerializeField] Projectile projectile;

    private Enemy targetEnemy = null;
    private float attackCounter;
    private bool isAttacking = false;

    void Update()
    {
        attackCounter -= Time.deltaTime;


        // Проверка цели
        if (targetEnemy == null || targetEnemy.gameObject == null || targetEnemy.IsDead || Vector2.Distance(transform.position, targetEnemy.transform.position) > attackRadius)
        {
            targetEnemy = GetNearestEnemy();
        }


        // Если цель есть и время пришло — начинаем атаку
        if (targetEnemy != null && attackCounter <= 0f)
        {
            isAttacking = true;
            attackCounter = timeBetweenAttacks;
        }
    }

    void FixedUpdate()
    {
        if (isAttacking)
        {
            Attack();
        }
    }

    void Attack()
    {
        isAttacking = false;

        if (targetEnemy == null) return;

        Projectile newProjectile = Instantiate(projectile);
        newProjectile.transform.position = transform.position;

        float damageMultiplier = Mathf.Pow(1.2f, Manager.Instance.WaveNumber - 1);
        newProjectile.ScaleDamage(damageMultiplier);

        if (newProjectile != null && targetEnemy != null)
        {
            Debug.Log("Атака! Башня стреляет по " + targetEnemy.name);
            StartCoroutine(MoveProjectile(newProjectile));
        }
    }

    IEnumerator MoveProjectile(Projectile projectile)
    {
        while (
            projectile != null &&
            targetEnemy != null &&
            !targetEnemy.IsDead &&
            Vector2.Distance(projectile.transform.position, targetEnemy.transform.position) > 0.2f &&
            Vector2.Distance(transform.position, targetEnemy.transform.position) <= attackRadius // <- добавлено
        )
        {
            Vector2 direction = targetEnemy.transform.position - projectile.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            projectile.transform.position = Vector2.MoveTowards(projectile.transform.position, targetEnemy.transform.position, 5f * Time.deltaTime);
            yield return null;
        }

        // Если враг умер или вышел из зоны — удалить снаряд
        if (projectile != null)
        {
            Destroy(projectile.gameObject);
        }
    }


    Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Enemy enemy in Manager.Instance.EnemyList)
        {
            if (enemy == null) continue;

            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance <= attackRadius && distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            Debug.Log("Новая цель: " + nearestEnemy.name);
        }

        return nearestEnemy;
    }
}