using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHitBox : MonoBehaviour
{
    public PolygonCollider2D hitbox;

    public void Attack()
    {
        hitbox.enabled = true;
    }

    public void StopAttack()
    {
        hitbox.enabled = false;
    }


}
