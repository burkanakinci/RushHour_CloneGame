using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameStateMachine gameStateMachine;

    public int level
    {
        get
        {
            if (!PlayerPrefs.HasKey("Level"))
            {
                PlayerPrefs.SetInt("Level", 1);
            }

            return PlayerPrefs.GetInt("Level");

        }

        set => PlayerPrefs.SetInt("Level", value);
    }

    public event Action levelStart;
    private int maxLevelObject=1;
    private int tempLevel;
    private GameObject levelObject;

    public List<ObstacleCarController> movingObstacleCars;
    public bool fpsLock=true;

    private Dictionary<string, GameObject> passedLevels;

    private void Awake()
    {
        Instance = this;

        passedLevels = new Dictionary<string, GameObject>();

        if (fpsLock)
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
        }

        SpawnPassedLevel();

        levelStart += gameStateMachine.ChangeInitialState;
        levelStart += CleanSceneObject;
        levelStart += SpawnSceneObject;
    }
    private void Start()
    {
        maxLevelObject = Resources.LoadAll("LevelObjects", typeof(GameObject)).Length;

        StartLevelStartAction();
    }

    public void StartLevelStartAction()
    {
        levelStart?.Invoke();
    }

    private void CleanSceneObject()
    {
        for(int i=movingObstacleCars.Count-1;i>=0;i--)
        {
            movingObstacleCars[i].OnObjectDeactive();
        }

        if(levelObject!=null)
        {
            levelObject.SetActive(false);
        }
    }
    private void SpawnSceneObject()
    {
        GetLevelData();
    }
    private void GetLevelData()
    {
        tempLevel = (level % maxLevelObject) > 0 ? (level % maxLevelObject) : maxLevelObject;

        if (level<=maxLevelObject)
        {
            levelObject = Instantiate(Resources.Load<GameObject>("LevelObjects/Level" + tempLevel), Vector3.zero, Quaternion.identity);
            passedLevels.Add(("LevelObjects/Level" + tempLevel),levelObject);
        }
        else
        {
            levelObject = passedLevels[("LevelObjects/Level" + tempLevel)];
        }

        levelObject.SetActive(true);
    }

    public GameStateMachine GetGameStateMachine()
    {
        return gameStateMachine;
    }

    private void SpawnPassedLevel()
    {
        tempLevel = (level % maxLevelObject) > 0 ? (level % maxLevelObject) : maxLevelObject;
        for (int i = tempLevel; i > 0; i--)
        {
            levelObject = Instantiate(Resources.Load<GameObject>("LevelObjects/Level" + tempLevel), Vector3.zero, Quaternion.identity);
            passedLevels.Add(("LevelObjects/Level" + tempLevel), levelObject);
            levelObject.SetActive(false);
        }
    }
}
