using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class LevelManager : MonoBehaviour
{ 

    [SerializeField] private TMPro.TMP_Text _ScoreUI;
    public int CurrentScore;
    [Space]
    [SerializeField] private GroundControllerScript _GroundContoller;

    [SerializeField] private EntityPassageController _DmgZoneSpawner;
    
    
    [Space] [Space]
    [SerializeField] public AnimationCurve _DifficultyLevelCurve;


    public int currentLevel;
    public int previousExperience;
    public int nextExperience;
    public int remainingExperience;

    [Space] [Space]
    [SerializeField] public AnimationCurve _DamageZoneSpawnFrequencyCurve;
    [SerializeField] public AnimationCurve _DamageZoneMoveSpeedCurve;
    [Space]
    [SerializeField] public AnimationCurve _GroundChangingCurve;
    [Space]
    [Space]
    [SerializeField] public AnimationCurve _MusicInstrumentsCurve;

    public void UpdateScore(float currentScore)
    {
        CurrentScore = (int)currentScore;
        _ScoreUI.text = "Score: " + CurrentScore * 100;

        HandleDifficultyScale();
    } 

    public void AddPoint()
    {
        CurrentScore++;  

        _ScoreUI.text = "Score: " + CurrentScore * 100;

        HandleDifficultyScale(); 
    }

    void HandleDifficultyScale()
    {
        if(CurrentScore - previousExperience < 0)
        {
            currentLevel--;
        }

        if((nextExperience - CurrentScore) <= 0)
        {
            currentLevel++;
        }

        if(currentLevel < 0)
        {
            currentLevel = 0;
        }

        if(CurrentScore < 0)
        {
            CurrentScore = 0;
        }

        previousExperience = (int)_DifficultyLevelCurve.Evaluate(currentLevel);
        nextExperience = (int)_DifficultyLevelCurve.Evaluate(currentLevel+1);

        remainingExperience = (nextExperience - CurrentScore);

    }

}



