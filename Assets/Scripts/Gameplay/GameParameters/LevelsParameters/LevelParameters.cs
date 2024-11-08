using UnityEngine;
using System.IO;

namespace GameParameters
{
    [CreateAssetMenu(fileName = "LevelParameters", menuName = "Level/LevelParameters")]
    public class LevelParameters : ScriptableObject
    {
        public int levelId;
        public PlayerResources playerStartResources;
        public UnitsSpawnTimings zombiesSpawnCycleParameters;
        public Sprite levelBackground;
        public float fastestLevelCompleteTime;
        public bool isLevelAvailable;

        private string jsonFilePath;

        [System.Serializable]
        private class LevelRecord
        {
            public float fastestLevelCompleteTime;
            public bool isLevelAvailable;
        }

        private void OnEnable()
        {
            jsonFilePath = "Assets/Resources/Level" + levelId + "Parameters.json";
            CheckAndCreateJsonFile();
            LoadTimeFromJson();
        }

        private void CheckAndCreateJsonFile()
        {
            if (!File.Exists(jsonFilePath))
            {
                LevelRecord defaultData = new LevelRecord
                {
                    fastestLevelCompleteTime = float.MaxValue,
                    isLevelAvailable = true,
                };
                
                Directory.CreateDirectory("Assets/Resources/Level" + levelId);
                string json = JsonUtility.ToJson(defaultData, true);
                File.WriteAllText(jsonFilePath, json);
            }
        }

        public void LoadTimeFromJson()
        {
            if (File.Exists(jsonFilePath))
            {
                string jsonText = File.ReadAllText(jsonFilePath);
                LevelRecord data = JsonUtility.FromJson<LevelRecord>(jsonText);
                fastestLevelCompleteTime = data.fastestLevelCompleteTime;
                isLevelAvailable = data.isLevelAvailable;
            }
        }

        public void UpdateFastestLevelCompleteTime(float levelCompleteTime)
        {
            if (levelCompleteTime < fastestLevelCompleteTime)
            {
                fastestLevelCompleteTime = levelCompleteTime;
                SaveLevelRecordToJson();
            }
        }

        public void UpdateLevelAvailability(bool newLevelAvailability)
        {
            isLevelAvailable = newLevelAvailability;
            SaveLevelRecordToJson();
        }

        private void SaveLevelRecordToJson()
        {
            LevelRecord data = new LevelRecord
            {
                fastestLevelCompleteTime = fastestLevelCompleteTime,
                isLevelAvailable = isLevelAvailable,
            };

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(jsonFilePath, json);
        }
    }
}