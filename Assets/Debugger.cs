using System.IO;
using UnityEngine;

public static class Debugger
{
    private static string filePath;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        string debugFolder = Path.Combine(Application.dataPath, "Debug");

        if (!Directory.Exists(debugFolder)) Directory.CreateDirectory(debugFolder);

        string fileName = "Log_" + System.DateTime.Now.ToString("dd.MM.yy_HH_mm") + ".txt";

        filePath = Path.Combine(debugFolder, fileName);
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine("Начало игровой сессии");
        }
    }

    public static void Log(string message)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            Debug.LogError("Файл не был создан. Убедитесь, что Initialize был вызван.");
            return;
        }

        string logMessage = $"{System.DateTime.Now.ToString("HH:mm:ss")} - {message}";

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(logMessage);
        }
    }
}
