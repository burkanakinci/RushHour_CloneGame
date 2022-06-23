using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerTrigger : MonoBehaviour
{
    public enum Direction
    {
        Forward,
        Backward
    }
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Direction obstacleDirection;

    private void OnEnable()
    {
        this.gameObject.layer = LayerMask.NameToLayer("ObstacleCarSpawnerTrigger");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerCar"))
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");

            ObjectPool.Instance.SpawnFromPool("ObstacleCar", 
                spawnPos.position,
                Quaternion.Euler(Vector3.up*180f*(obstacleDirection==Direction.Backward?1f:0f)),
                Vector3.one);
        }
    }
}
