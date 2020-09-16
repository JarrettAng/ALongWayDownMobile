using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public PlatformType Type;

    [Header("Platform Attrivbutes")]
    [SerializeField, Tooltip("How big is the hole?")] private float holeSize = 4f;

    [SerializeField, Tooltip("Platform left of hole")] private GameObject leftPlatform = default;
    [SerializeField, Tooltip("Platform right of hole")] private GameObject rightPlatform = default;

    private PlatformManager platformManager;
    private Vector2 sectionSpawnPos;

    private void Start() {
        sectionSpawnPos = new Vector2(0, transform.position.y);

        platformManager = PlatformManager.Instance;

        SetupPlatform();
    }

    /// <summary>
    /// Fills the sections of the platform. Every section takes up 1/3 of platform
    /// </summary>
    private void SetupPlatform() {
        bool mandatoryEnemySectionAdded = false;

        SetupLeftSection();
        SetupMiddleSection();
        SetupRightSection();

        void SetupLeftSection() {
            GameObject sectionPrefab;
            sectionSpawnPos.x = platformManager.LeftSectionCenter;

            if(Type == PlatformType.LEFT_EXIT) { // Spawn exit
                sectionPrefab = platformManager.GetExitPrefab();
                AlignPlatformsToFormHole();
            } else { // Spawn normally

                // TO DO: IMPROVE (REFACTOR) MANDATORY SECTION ADDING
                bool mandatorySection = Random.value > 0.5f;
                if(mandatorySection) {
                    sectionPrefab = AddMandatorySection();
                } else {
                    sectionPrefab = platformManager.GetRandomSectionPrefab();
                }
            }

            sectionPrefab = Instantiate(sectionPrefab, sectionSpawnPos, Quaternion.identity);
            sectionPrefab.transform.parent = transform;
        }

        void SetupMiddleSection() {
            GameObject sectionPrefab;
            sectionSpawnPos.x = platformManager.MidSectionCenter;

            if(Type == PlatformType.CENTER_EXIT) { // Spawn exit
                sectionPrefab = platformManager.GetExitPrefab();
                AlignPlatformsToFormHole();
            } else { // Spawn normally

                // TO DO: IMPROVE (REFACTOR) MANDATORY SECTION ADDING
                bool mandatorySection = Random.value > 0.5f;
                if(!mandatoryEnemySectionAdded && (Type == PlatformType.RIGHT_EXIT || mandatorySection)) {
                    sectionPrefab = AddMandatorySection();
                } else {
                    sectionPrefab = platformManager.GetRandomSectionPrefab();
                }
            }

            sectionPrefab = Instantiate(sectionPrefab, sectionSpawnPos, Quaternion.identity);
            sectionPrefab.transform.parent = transform;
        }

        void SetupRightSection() {
            GameObject sectionPrefab;
            sectionSpawnPos.x = platformManager.RightSectionCenter;

            if(Type == PlatformType.RIGHT_EXIT) { // Spawn exit
                sectionPrefab = platformManager.GetExitPrefab();
                AlignPlatformsToFormHole();
            } else { // Spawn normally

                // TO DO: IMPROVE (REFACTOR) MANDATORY SECTION ADDING
                if(!mandatoryEnemySectionAdded) {
                    sectionPrefab = AddMandatorySection();
                } else {
                    sectionPrefab = platformManager.GetRandomSectionPrefab();
                }
            }

            sectionPrefab = Instantiate(sectionPrefab, sectionSpawnPos, Quaternion.identity);
            sectionPrefab.transform.parent = transform;
        }

        GameObject AddMandatorySection() {
            mandatoryEnemySectionAdded = true;

            return platformManager.GetMandatorySectionPrefab();
        }
    }

    private void AlignPlatformsToFormHole() {
        Vector2 leftHolePos = sectionSpawnPos;
        float leftPlatformOffset = leftPlatform.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        leftHolePos.x = leftHolePos.x - leftPlatformOffset - holeSize / 2f;
        leftPlatform.transform.position = leftHolePos;

        Vector2 rightHolePos = sectionSpawnPos;
        float rightPlatformOffset = rightPlatform.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        rightHolePos.x = rightHolePos.x + rightPlatformOffset + holeSize / 2f;
        rightPlatform.transform.position = rightHolePos;
    }
}
