using UnityEngine;

public interface IPooledObject
{
    void OnObjectSpawn();
    void OnObjectDeactive();
    GameObject GetGameObject();
}
