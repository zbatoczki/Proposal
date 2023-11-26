using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Tooltip("The max damge the attack can go to from the range 1 to max damage.")]
    public int maxAttackDamage = 10;

    public Vector2 knockback = Vector2.zero;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if(damageable != null)
        {
            Transform t = transform.parent ?? transform;
            Vector2 deliverableKnockback = t.localScale.x > 0 ? knockback: new Vector2(-knockback.x, knockback.y);
            int finalAttackDamage = Mathf.CeilToInt(Random.Range(1, maxAttackDamage));
            bool hitSuccessful = damageable.Hit(finalAttackDamage, deliverableKnockback);
            if(hitSuccessful)
                print(collision.name + " hit for " + maxAttackDamage);
        }
    }
}
