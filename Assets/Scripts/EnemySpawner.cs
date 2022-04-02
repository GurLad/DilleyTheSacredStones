using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyType> Enemies;
    public TextAsset Level;

    private void Start()
    {
        
    }
}

[System.Serializable]
public record EnemyType
{
    public string Name;
    public AEnemy Object;
}