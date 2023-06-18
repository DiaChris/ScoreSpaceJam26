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
    [SerializeField] private AudioController _AudioController;
    
    
    [Space] [Space]
    [SerializeField] public AnimationCurve _DifficultyLevelCurve;


    public int currentLevel;
    public int previousExperience;
    public int nextExperience;
    public int remainingExperience;

    [Space] [Space]
    [SerializeField] public AnimationCurve _DamageZoneSpawnFrequencyCurve;
    public float spawnDelay;
    [SerializeField] public AnimationCurve _DamageZoneMoveSpeedCurve;
    public float dmgZoneSpeed;

    [Space]
    [SerializeField] public AnimationCurve _GroundChangingCurve;
    public float groundChangeDelay;
    [Space]
    [Space]
    [SerializeField] public AnimationCurve _MusicInstrumentsCurve;
    public int nextIntrument = 1;

    void Start()
    {
        CurrentScore = 0;
        UpdateScore(0);
    }

    public void UpdateScore(float currentScore)
    {
        if(currentScore > 99) return;   
        CurrentScore = (int)currentScore;
        
        OnScoreUpate();
    }

    void OnScoreUpate()
    {
        _ScoreUI.text = "Score: " + CurrentScore * 100;

        HandleDifficultyScale();


        HandleDamageZoneSpawnFrequency();
        HandleDamageZoneSpeed();
        HandleGroundChangingSpeed();

        HandleInstrumentsNumber();
    }

    public void AddPoint()
    {
        CurrentScore++;  

        OnScoreUpate(); 
    }

    public void FinalLevel()
    {
        for (int i = 0; i < 1000; i++)
        {
            AddPoint();
        }
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

    void HandleDamageZoneSpawnFrequency()
    {
        spawnDelay = _DamageZoneSpawnFrequencyCurve.Evaluate(currentLevel);

        _DmgZoneSpawner.SetSpawnDelay(spawnDelay);
    }

    void HandleDamageZoneSpeed()
    {
        dmgZoneSpeed =_DamageZoneMoveSpeedCurve.Evaluate(currentLevel);

        _DmgZoneSpawner.SetEntitySpeed(dmgZoneSpeed);
    }

    void HandleGroundChangingSpeed()
    {
        groundChangeDelay = _GroundChangingCurve.Evaluate(currentLevel);

        _GroundContoller.SetUpdateDelay(groundChangeDelay);
    }

    void HandleInstrumentsNumber()
    {
        nextIntrument = (int)_MusicInstrumentsCurve.Evaluate(currentLevel);

        _AudioController.SetTrack(nextIntrument - 1);
    }
}
