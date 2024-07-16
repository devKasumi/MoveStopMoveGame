using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonFileHandler
{
    public static void SaveToJson<T>(T dataToSave, string fileName)
    {
        string dataToJson = JsonUtility.ToJson(dataToSave);
        string filePath = Application.dataPath + "/" + Constants.JSON_PATH + "/" + fileName;
        Debug.LogError(filePath);
        WriteFile(filePath, dataToJson);
    }

    public static T ReadFromJson<T>(string fileName)
    {
        string filePath = Application.dataPath + "/" + Constants.JSON_PATH + "/" + fileName;
        string data = ReadFile(filePath);

        //Debug.LogError(data);

        if (string.IsNullOrEmpty(data) || data == "{}")
        {
            return default(T);
        }

        T res = JsonUtility.FromJson<T>(data);

        return res;
    }

    private static void WriteFile(string filePath, string data)
    {
        FileStream fileStream = new FileStream(filePath, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(data);
        }
    }

    private static string ReadFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string data = reader.ReadToEnd();
                return data;    
            }
        }

        return "";
    }
}

