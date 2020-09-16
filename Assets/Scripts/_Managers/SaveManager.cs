using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public class SaveManager : Singleton<SaveManager>
{
    public static PlayerData StoredPlayerData;

    private void Awake() {
        if(Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);

        LoadGameData();
    }

    public static void SaveGameData() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/ALWDPlayerData.dat", FileMode.Create);

        bf.Serialize(stream, StoredPlayerData);

        stream.Close();
    }

    private void LoadGameData() {
        StoredPlayerData = new PlayerData();

        if(File.Exists(Application.persistentDataPath + "/ALWDPlayerData.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/ALWDPlayerData.dat", FileMode.Open);
            StoredPlayerData = bf.Deserialize(stream) as PlayerData;

            stream.Close();
        } else {
            LoadDefaultValues();
        }

        void LoadDefaultValues() {
            StoredPlayerData.Highscore = 0;
            StoredPlayerData.HasPlayedBefore = false;
        }
    }

    public static void SaveSoundData(SoundData data) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/ALWDSoundData.dab", FileMode.Create);

        bf.Serialize(stream, data);

        stream.Close();
    }

    public static SoundData LoadSoundData() {
        SoundData savedData;

        if(File.Exists(Application.persistentDataPath + "/ALWDSoundData.dab")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/ALWDSoundData.dab", FileMode.Open);
            savedData = bf.Deserialize(stream) as SoundData;

            stream.Close();
        } else {
            savedData = new SoundData();
        }

        return savedData;
    }
}
