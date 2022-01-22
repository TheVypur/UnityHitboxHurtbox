using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Hitbox
{
    public Vector3 v3Direction;
    public float fSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        this.transform.position += v3Direction * fSpeed * Time.deltaTime;
    }

    public void Shoot(Vector3 v3Direction, float fSpeed)
    {
        this.fSpeed = fSpeed;
        this.v3Direction = v3Direction;
    }

    protected override void OnHit(Hurtbox h)
    {
        h.health.fHealth -= fDamage;

        h.cc.v3MoveDirection = v3Direction * fSpeed;
    }
}
