using UnityEngine;

[CreateAssetMenu(fileName = "CarMovementData", menuName = "Car Movement Data")]
public class CarMovementData : ScriptableObject
{

    #region SetRotationPoint
    [Space]
    [Header("Target Rotation Values")]
    public float targetRotationDuration = 1.5f;
    public float rotationLerpValue = 1f;
    public float rotateStartZDistance=0f;
    public float rotateZDistance = 20f;
    #endregion

    #region SetVelovity
    [Space]
    [Header("Velocity Multiplier Values")]
    public float slowlyMultiplier=10f;
    public float normalMultiplier=30f;
    public float speedMultiplier=60f;
    public float velocityDuration=1.2f;
    public float slowlyDuration=0.6f;
    #endregion
}
