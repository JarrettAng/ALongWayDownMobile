using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T instance;

    public static T Instance {
        get {
            // If there is already an instance of this object, return it.
            if(instance != null) {
                return instance;
            }

            var instances = FindObjectsOfType<T>();
            var count = instances.Length;

            if(count > 0) {
                // If one object found and is not set an instance, assign it.
                if(count == 1) {
                    return instance = instances[0];
                }

                // If more than one object is found, destroy all and return the first one.
                for(int x = 1; x < count; x++) {
                    Destroy(instances[x]);
                }
                return instance = instances[0];
            }

            Debug.LogError("Instance of" + nameof(Singleton<T>) + "not found.");
            return null;

        }
    }
}
