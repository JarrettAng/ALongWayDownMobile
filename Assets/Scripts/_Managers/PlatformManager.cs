using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : Singleton<PlatformManager>
{
    [System.Serializable]
    public class Level {
        public string name;
        public int SpawnLevel;
        public List<GameObject> Prefabs;
    }

    [System.Serializable]
    public class LevelPack {
        public Level[] Levels;
    }

    [Header("Mandatory Sections")]
    [SerializeField, Tooltip("Exit section type")] private GameObject exitSection;

    [Header("Other Sections")]
    [SerializeField] private LevelPack levelPack;
    [SerializeField] private LevelPack mandatoryLevelPack;

    [Header("Section Spawnpoints")]
    // Sections centers are at 1/4, 2/4, 3/4 of screen length
    public float LeftSectionCenter;
    public float MidSectionCenter;
    public float RightSectionCenter;

    private Queue<Level> levelOrder;
    private Queue<Level> mandatoryLevelOrder;

   [SerializeField] private List<GameObject> prefabsToUse;
   [SerializeField] private List<GameObject> mandatoryPrefabsToUse;

    private int platformsPassed = 0;

    private void Awake() {
        EventManager.OnPlatformPass += IncrementSpawnDifficulty;
        EventManager.OnPlatformPass += IncrementMandatorySpawnDifficulty;

        float horizontalExtent = Camera.main.orthographicSize * Screen.width / Screen.height;

        LeftSectionCenter = Camera.main.transform.position.x - horizontalExtent / 2;
        RightSectionCenter = Camera.main.transform.position.x + horizontalExtent / 2;
        MidSectionCenter = Camera.main.transform.position.x;

        SetupLevelsToUse();
        SetupMandatoryLevelsToUse();

        void SetupLevelsToUse() {
            levelOrder = new Queue<Level>();
            foreach(Level level in levelPack.Levels) {
                levelOrder.Enqueue(level);
            }

            UpdateLevelPrefabs();
        }

        void SetupMandatoryLevelsToUse() {
            mandatoryLevelOrder = new Queue<Level>();
            foreach(Level level in mandatoryLevelPack.Levels) {
                mandatoryLevelOrder.Enqueue(level);
            }

            UpdateMandatoryLevelPrefabs();
        }
    }

    private void IncrementSpawnDifficulty() {
        platformsPassed++;

        // Stop counting since there are no longer harder obstacles
        if(levelOrder.Count <= 0) {
            EventManager.OnPlatformPass -= IncrementSpawnDifficulty;
            return;
        }

        if(levelOrder.Peek().SpawnLevel <= platformsPassed) {
            UpdateLevelPrefabs();
        }
    }

    private void IncrementMandatorySpawnDifficulty() {
        // Stop counting since there are no longer harder obstacles
        if(mandatoryLevelOrder.Count <= 0) {
            EventManager.OnPlatformPass -= IncrementMandatorySpawnDifficulty;
            return;
        }

        if(levelOrder.Peek().SpawnLevel <= platformsPassed) {
            UpdateMandatoryLevelPrefabs();
        }
    }

    private void UpdateLevelPrefabs() {
        List<GameObject> newPrefabs = levelOrder.Dequeue().Prefabs;
        prefabsToUse = newPrefabs;
    }

    private void UpdateMandatoryLevelPrefabs() {
        List<GameObject> newMandatoryPrefabs = mandatoryLevelOrder.Dequeue().Prefabs;
        mandatoryPrefabsToUse = newMandatoryPrefabs;
    }

    public GameObject GetRandomSectionPrefab() {
        int index = Random.Range(0, prefabsToUse.Count);

        return prefabsToUse[index];
    }

    public GameObject GetMandatorySectionPrefab() {
        return mandatoryPrefabsToUse[0];
    }
    
    #region Mandatory Sections

    public GameObject GetExitPrefab() {
        return exitSection;
    }

    #endregion
}
