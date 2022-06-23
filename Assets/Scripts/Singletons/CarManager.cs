using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public static CarManager Instance { get; private set; }

    [SerializeField] private CarMovement carMovement;

    [SerializeField] private float durabilityValue=100f;

    public ParticleSystem frontCollidedParticle;
    public ParticleSystem speedUpParticle;

    private void Awake()
    {
        Instance = this;
    }
    public CarMovement GetCarMovement()
    {
        return carMovement;
    }

    public void DamageCar( float _damageValue)
    {
        durabilityValue -= _damageValue;
        UIManager.Instance.SetDamageImage(ref durabilityValue);
    }
    public void CheckDurability()
    {
        if(durabilityValue<=0.0f)
        {
            GameManager.Instance.GetGameStateMachine().ChangeState(GameManager.Instance.GetGameStateMachine().failState);
        }
    }

    public float GetDurability()
    {
        return durabilityValue;
    }

    public void ResetCarValues()
    {
        speedUpParticle.Stop();

        carMovement.ResetCarMovement();
    }
}
