using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject friendPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector2 friendSpawnPosition;
    [SerializeField] private Vector2 enemySpawnPosition;
    private Quaternion baseRotation = Quaternion.Euler(0, 0, 0);

    void Start()
    {
        SpawnObject(friendSpawnPosition, baseRotation, friendPrefab);
        SpawnObject(enemySpawnPosition, baseRotation, enemyPrefab);
    }

    public void SpawnObject(Vector2 position, Quaternion rotation, GameObject objectPrefab)
    {
        Instantiate(objectPrefab, new Vector3(position.x, position.y, 0), rotation);
    }
}
