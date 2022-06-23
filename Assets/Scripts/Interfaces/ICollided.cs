using UnityEngine;

public interface ICollided
{
    void Damage();
    void PlayEffect(Vector3 _effectPos,ref ParticleSystem _particle);
}
