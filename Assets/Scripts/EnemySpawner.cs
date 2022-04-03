using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyType> Enemies;
    public List<TextAsset> Levels;
    private List<string> lines;
    private int lastLine = -1;

    private void Start()
    {
        lines = new List<string>(Levels[GameConsts.CurrentLevel].text.Replace("\r", "").Split("\n"));
    }

    private void Update()
    {
        if (Conductor.SongPositionInBeats > lastLine)
        {
            ParseLine(lines[(lastLine = Conductor.SongPositionInBeats) % lines.Count]);
        }
    }

    private void ParseLine(string line)
    {
        if (line != "")
        {
            string[] parts = line.Split(";");
            foreach (string part in parts)
            {
                if (part != "")
                {
                    SpawnEnemyFromString(part);
                }
            }
        }
    }

    private void SpawnEnemyFromString(string enemy)
    {
        string[] parts = enemy.Split(",");
        AEnemy enemyObject = Enemies.Find(a => a.Name.ToLower() == parts[0].ToLower())?.Object;
        if (enemyObject != null && parts.Length > 2)
        {
            enemyObject = Instantiate(enemyObject);
            enemyObject.Generate(new Vector2Int(int.Parse(parts[1]), int.Parse(parts[2])));
            enemyObject.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Invalid enemy - " + enemy);
        }
    }
}

[System.Serializable]
public record EnemyType
{
    public string Name;
    public AEnemy Object;
}