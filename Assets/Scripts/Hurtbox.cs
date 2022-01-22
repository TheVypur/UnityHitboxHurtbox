using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public PlayerHealth health;
    public CharacterControl cc;

    private void Start()
    {
        health = transform.GetComponentInParent<PlayerHealth>();
        cc = transform.GetComponentInParent<CharacterControl>();
    }


}
