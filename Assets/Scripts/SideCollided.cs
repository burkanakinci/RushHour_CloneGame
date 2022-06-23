using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SideCollided:ICollided
{
    public float damage = 15f;
    public SideCollided(float _damage)
    {
        damage = _damage;
    }
    public void Damage()
    {
        CarManager.Instance.DamageCar( damage);
    }
    public void PlayEffect(Vector3 _effectPos,ref ParticleSystem _particle)
    {
        _particle.transform.position = _effectPos;
        _particle.Play();
    }
}
