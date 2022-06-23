using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleCarController : MonoBehaviour,IPooledObject
{
    [SerializeField] private SkinnedMeshRenderer[] obstacleCarVisuals;
    private int tempObstacleCarVisual;

    [SerializeField] private Rigidbody obstacleCarRB;
    public float speedMultiplier;

    [SerializeField] private BoxCollider boxCollider;

    private ICollided sideCollided, frontCollided;
    [SerializeField] private ParticleSystem sideCollidedParticle;

    private void Awake()
    {
        sideCollided = new SideCollided(20f);
        frontCollided = new FrontCollided();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerCar") &&
            this.gameObject.layer == LayerMask.NameToLayer("ObstacleCar"))
        {
            this.gameObject.layer = LayerMask.NameToLayer("ObstacleCarCollided");
            if (collision.GetContact(0).normal.z>=0.8f|| collision.GetContact(0).normal.z <= -0.8f)
            {
                collision.gameObject.layer = LayerMask.NameToLayer("PlayerCarCollided");
                frontCollided.Damage();
                frontCollided.PlayEffect(collision.GetContact(0).point,ref CarManager.Instance.frontCollidedParticle);

            }
            else
            {
                sideCollided.PlayEffect(collision.GetContact(0).point,ref sideCollidedParticle);
                sideCollided.Damage();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCar") &&
                this.gameObject.layer == LayerMask.NameToLayer("ObstacleCar"))
        {
            CarManager.Instance.GetCarMovement().BreakSpeed(obstacleCarRB.velocity.z);
        }
    }

    public void SetObstacleCarVelocity()
    {
        obstacleCarRB.velocity = transform.forward * (transform.position.x<0?50f:8f); 
        obstacleCarRB.angularVelocity = Vector3.zero;
    }
    public void ResetObstacleCarVelocity()
    {
        obstacleCarRB.velocity = Vector3.zero;
        obstacleCarRB.angularVelocity = Vector3.zero;
    }

    public void OnObjectSpawn()
    {
        for(int i=obstacleCarVisuals.Length-1;i>=0;i--)
        {
            obstacleCarVisuals[i].transform.parent.gameObject.SetActive(false);
        }

        tempObstacleCarVisual = UnityEngine.Random.Range(0, obstacleCarVisuals.Length);
        obstacleCarVisuals[tempObstacleCarVisual].transform.parent.gameObject.SetActive(true);

        boxCollider.size = obstacleCarVisuals[tempObstacleCarVisual].bounds.size;

        this.gameObject.layer = LayerMask.NameToLayer("ObstacleCar");

        GameManager.Instance.movingObstacleCars.Add(this);
    }
    public void OnObjectDeactive()
    {
        GameManager.Instance.movingObstacleCars.Remove(this);

        ObjectPool.Instance.DeactiveObject("ObstacleCar",this);
    }
    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
