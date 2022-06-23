using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CarMovement : MonoBehaviour
{ 
    private Vector3 targetRotationPoint, targetRotationStart, targetRotationEnd;
    
    private float rotationPointMovementTimer = 0.0f;

    [SerializeField] private Rigidbody carRB;
    [SerializeField] private CarMovementData carMovementData;

    private float velocityMultiplier;
    private Vector3 targetVelocity;

    private Vector3 lookPos;
    private Quaternion rotation;

    private bool slowlyTrigger;
    private Coroutine setVelocityCoroutine;

    private void Awake()
    {
        carRB.centerOfMass = Vector3.forward * 5f;
    }

    public void ResetCarMovement()
    {
        slowlyTrigger = false;

        this.gameObject.layer = LayerMask.NameToLayer("PlayerCar");

        transform.position = Vector3.zero;

        SetTargetRotationPoint();
        ResetCarVelocity();
    }

    public void UpdateTempVelocity()
    {
        if (Input.GetMouseButtonDown(0) || (Input.GetMouseButtonUp(0)&&!slowlyTrigger))
        {
            if(slowlyTrigger==true)
            {
                slowlyTrigger = false;
            }

            if(setVelocityCoroutine!=null)
            {
                StopCoroutine(setVelocityCoroutine);
            }

            SetTargetRotationPoint();

            BreakSpeed();
            setVelocityCoroutine = StartCoroutine(SetVelocity());

            if(Input.GetMouseButtonDown(0))
            {
                CarManager.Instance.speedUpParticle.Play();
            }
            else
            {
                CarManager.Instance.speedUpParticle.Stop();
            }
            

        }

        targetRotationEnd.z = transform.position.z + carMovementData.rotateZDistance;
        targetRotationPoint = LerpOverTime(ref targetRotationStart,
                                            ref targetRotationEnd,
                                            ref rotationPointMovementTimer,
                                            ref carMovementData.targetRotationDuration);


        targetVelocity = transform.forward * velocityMultiplier;
    }
    private void SetTargetRotationPoint()
    {
        rotationPointMovementTimer = 0.0f;

        targetRotationPoint = new Vector3((3.5f * (Input.GetMouseButtonDown(0) ? -1f : 1f)),
                transform.position.y,
                (transform.position.z + carMovementData.rotateStartZDistance));

            targetRotationStart = targetRotationPoint;

            targetRotationEnd.x = (targetRotationPoint.x* (1f / 3.5f)) *2.5f;
            targetRotationEnd.y = transform.position.y;
    }
    private void BreakSpeed()
    {
        DOTween.Kill("VelocitySlowlyTween");

        DOTween.To(() => velocityMultiplier, x => velocityMultiplier = x, carMovementData.slowlyMultiplier, carMovementData.slowlyDuration).
                SetEase(Ease.OutExpo).
                SetId("VelocitySlowlyTween");
    }
    public void BreakSpeed(float _velocityMultiplier)
    {
        slowlyTrigger = true;

        DOTween.Kill("VelocitySlowlyTween");
        DOTween.Kill("VelocityMultiplierTween");
        StopCoroutine(setVelocityCoroutine);

        velocityMultiplier = _velocityMultiplier;
    }
    public IEnumerator SetVelocity()
    {
        yield return new WaitForSeconds(carMovementData.slowlyDuration);
        DOTween.Kill("VelocityMultiplierTween");

        DOTween.To(() => velocityMultiplier, x => velocityMultiplier = x, ((targetRotationEnd.x<0f) ? carMovementData.speedMultiplier : carMovementData.normalMultiplier), carMovementData.velocityDuration).
                SetEase(Ease.OutExpo).
                SetId("VelocityMultiplierTween");
    }

    public void UpdateCarVelocity()
    {
        carRB.velocity = targetVelocity;
        carRB.angularVelocity = Vector3.zero;
    }

    public void RotateCarToTarget()
    {
        lookPos = targetRotationPoint - transform.position;
        lookPos.y = 0;
        if (lookPos != Vector3.zero)
        {
            rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, carMovementData.rotationLerpValue * Time.deltaTime);
        }

    }

    private Vector3 LerpOverTime(ref Vector3 _start, ref Vector3 _end, ref float _timer, ref float _duration)
    {
        if (_timer < _duration)
        {
            _timer += Time.deltaTime;
        }

        return Vector3.Lerp(_start, _end, (_timer / _duration));
    }

    public void ResetCarVelocity()
    {
        carRB.velocity = Vector3.zero;
        carRB.angularVelocity = Vector3.zero;
    }

}
