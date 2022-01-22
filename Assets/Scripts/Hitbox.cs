using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hitbox : MonoBehaviour
{
    public float fDamage = 20;
    public Vector3 v3Knockback = new Vector3(0, 5, 15);

    public LayerMask layerMask;


    private void OnTriggerEnter(Collider other)
    {
        if (layerMask == (layerMask | (1 << other.transform.gameObject.layer)))
        {
            Hurtbox h = other.GetComponent<Hurtbox>();

            if (h != null)
            {
                OnHit(h);
            }
        }
    }

    protected virtual void OnHit(Hurtbox h)
    {
        h.health.fHealth -= fDamage;
        h.cc.v3MoveDirection = v3Knockback;
        Destroy(this.gameObject);
    }
}
