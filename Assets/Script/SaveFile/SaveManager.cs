using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class SaveManager 
{
    private static string SavePath =>
       Path.Combine(Application.persistentDataPath, "save.json");

    public static async Task SaveAsync(string levelName, int score, int Life, HashSet<int> collected)
    {
        SaveData data = new SaveData
        {
            LevelName = levelName,
            CountMoney = score,
            CountLife = Life,
            СollectedBonus = collected
        };

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        await File.WriteAllTextAsync(SavePath, json);
    }

    public static async Task<SaveData> LoadAsync()
    {
        if (!File.Exists(SavePath))
            return null;

        string json = await File.ReadAllTextAsync(SavePath);
        return JsonConvert.DeserializeObject<SaveData>(json);
    }


    public static async Task ResetSaveToDefaultsAsync()
    {
        SaveData defaultData = new SaveData
        {
            LevelName = "Level_01",
            CountMoney = 0,
            CountLife = 1,
            СollectedBonus = new HashSet<int>()
        };

        string json = JsonConvert.SerializeObject(defaultData, Formatting.Indented);
        await File.WriteAllTextAsync(SavePath, json);
    }
}
 