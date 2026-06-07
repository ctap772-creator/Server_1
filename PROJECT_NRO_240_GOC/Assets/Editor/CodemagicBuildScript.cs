using UnityEditor;
using System.IO;

public class CodemagicBuildScript
{
    public static void BuildIOS()
    {
        // Định nghĩa đường dẫn xuất dự án Xcode giống như trong file yaml
        string buildPath = "build/ios";
        
        // Lấy danh sách các Scene đang được bật trong Build Settings của game bạn
        string[] scenes = GetBuildScenes();

        // Kích hoạt lệnh build của Unity
        BuildPipeline.BuildPlayer(scenes, buildPath, BuildTarget.iOS, BuildOptions.None);
    }

    private static string[] GetBuildScenes()
    {
        System.Collections.Generic.List<string> scenes = new System.Collections.Generic.List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                scenes.Add(scene.path);
            }
        }
        return scenes.ToArray();
    }
}