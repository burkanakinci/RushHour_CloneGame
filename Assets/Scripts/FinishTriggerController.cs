using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTriggerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] finishParticles;

    private void OnEnable()
    {
        this.gameObject.layer = LayerMask.NameToLayer("FinishTrigger");

        for (int i= finishParticles.Length - 1;i>=0;i--)
        {
            finishParticles[i].Stop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCar"))
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");

            GameManager.Instance.GetGameStateMachine().ChangeState(GameManager.Instance.GetGameStateMachine().finishState);

            for (int i = finishParticles.Length - 1; i >= 0; i--)
            {
                finishParticles[i].Play();
            }
        }
    }
}
