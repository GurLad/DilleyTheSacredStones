using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatController : MonoBehaviour
{
    private static BeatController current;
    [Header("Stats")]
    public float TargetScore;
    public float MoveScore;
    public float HitScore;
    public float TakeDamageScore;
    public float MultiplierIncreaseRate;
    public float DecayRate;
    [Header("Objects")]
    public Image ScoreBar;
    public Vector2 SizeRange;
    private float score;
    private float multiplier = 1;
    private Vector2 baseBarSize;

    private void Awake()
    {
        current = this;
        baseBarSize = ScoreBar.rectTransform.sizeDelta;
    }

    private void Update()
    {
        score -= Time.deltaTime * DecayRate;
        score = Mathf.Max(0, score);
        //Debug.Log("S: " + score + ", M: " + multiplier);
        if (score < TargetScore)
        {
            UpdateBarSize();
        }
        else
        {
            // Win code
            GameConsts.CurrentLevel++;
            SceneLoader.LoadScene("ShipCutscene");
        }
    }

    private void UpdateBarSize()
    {
        ScoreBar.rectTransform.sizeDelta = new Vector2(baseBarSize.x, SizeRange.x + (SizeRange.y - SizeRange.x) * score / TargetScore);
    }

    public static void RecordMove()
    {
        float accuracy = Conductor.BeatAccuracy(true);
        current.score += accuracy * current.MoveScore * current.multiplier;
        if (accuracy > 0)
        {
            current.multiplier += accuracy * current.MoveScore * current.MultiplierIncreaseRate;
        }
    }

    public static void RecordHit(float accuracy)
    {
        if (accuracy > 0)
        {
            current.score += accuracy * current.HitScore * current.multiplier;
            current.multiplier += accuracy * current.HitScore * current.MultiplierIncreaseRate;
        }
    }

    public static void RecordTakeDamage()
    {
        current.score -= current.TakeDamageScore;
    }    
}
