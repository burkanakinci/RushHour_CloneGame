using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCollided : ICollided
{
    public void Damage()
    {
        CarManager.Instance.DamageCar( CarManager.Instance.GetDurability());
    }
    public void PlayEffect(Vector3 _effectPos,ref ParticleSystem _particle)
    {
        _particle.transform.position = _effectPos;
        _particle.Play();
    }
}
