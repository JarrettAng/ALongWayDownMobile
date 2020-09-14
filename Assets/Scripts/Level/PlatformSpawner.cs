using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Platform To Spawn")]
    [SerializeField, Tooltip("The platform that should spawn")] private Platform platform;

    [Header("Spawning Attributes")]
    [SerializeField, Tooltip("Types of platform")] private PlatformType[] platformTypes;
    [SerializeField, Tooltip("How many platforms to spawn at the start?")] private int numberToSpawn = 3;
    [SerializeField, Tooltip("The height spacing between platforms")] private float gapBetweenPlatforms = 6f;

    [SerializeField, Tooltip("New platform spawn position for y")] private Vector2 platformSpawnPos;
    [SerializeField, Tooltip("Last platform spawn type")] private PlatformType lastPlatformType = PlatformType.NULL;

    private void Awake() {
        EventManager.OnPlatformPass += SpawnPlatform;

        for(int current = 0; current < numberToSpawn; current++) {
            SpawnPlatform();
        }
    }

    public void SpawnPlatform() {
        int index = Random.Range(0, platformTypes.Length);
        PlatformType platformType = platformTypes[index];

        // Prevents repeat of level exits
        if(platformTypes.Length > 1) {
            while(platformType == lastPlatformType) {
                index = Random.Range(0, platformTypes.Length);
                platformType = platformTypes[index];
            }
        }

        lastPlatformType = platformType;

        Platform newPlatform = Instantiate(platform, platformSpawnPos, Quaternion.identity);
        newPlatform.transform.parent = transform;

        newPlatform.Type = platformType;

        platformSpawnPos.y -= gapBetweenPlatforms;
    }
}
