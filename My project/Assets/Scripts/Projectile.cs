using UnityEngine;


public enum projectileType
{
    lightning, arow, sword


};
public class Projectile : MonoBehaviour
{
    [SerializeField]
    int attackDamage;

    [SerializeField]
    projectileType pType;

    public int AttackDamage
    {

        get
        {
            return attackDamage;
        }
    }

    public projectileType PType
    {

        get
        {
            return pType;



        }
    }
    public void ScaleDamage(float multiplier)
    {
        attackDamage = Mathf.RoundToInt(attackDamage * multiplier);
    }

}
