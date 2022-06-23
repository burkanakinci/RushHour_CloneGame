using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Space]
    [SerializeField] private Vector3 offsetOnPlay;
    [SerializeField] private Transform carVisual;


    [Space]
    [SerializeField] private Vector3 offsetOnFinish;

    private Vector3 lookPos;
    private Quaternion rotation;
    [SerializeField] float rotationLerpValue= 5f;
    [SerializeField] float movementLerpValue = 5f;

    private void Update()
    {
        MoveCamera();
        LookTarget();
    }
    private void MoveCamera()
    {

        transform.position = Vector3.Lerp(transform.position,carVisual.position+offsetOnPlay,
            Time.deltaTime * movementLerpValue);
    }
    private void LookTarget()
    {
        lookPos = carVisual.position -transform.position;
         rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationLerpValue);
    }
    
}