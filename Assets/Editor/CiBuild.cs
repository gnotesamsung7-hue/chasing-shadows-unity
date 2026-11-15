using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;

// Build entry point for GitHub Actions. Everything is set from code because
// there is no Unity Editor to click through.
public static class CiBuild
{
    public static void Android()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        Directory.CreateDirectory("Assets/Scenes");
        EditorSceneManager.SaveScene(scene, "Assets/Scenes/Main.unity");

        PlayerSettings.companyName = "Mark Dave";
        PlayerSettings.productName = "Chasing Shadows Unity Test";
        PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.Android, "com.markdave.chasingshadows.unitytest");
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;
        PlayerSettings.SetScriptingBackend(NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;

        string path = Arg("-customBuildPath") ?? "build/Android/unity-test.apk";
        if (!path.EndsWith(".apk")) path += ".apk";
        Directory.CreateDirectory(Path.GetDirectoryName(path));

        BuildReport report = BuildPipeline.BuildPlayer(
            new[] { "Assets/Scenes/Main.unity" }, path, BuildTarget.Android, BuildOptions.None);
        Debug.Log("Build result: " + report.summary.result + " -> " + path);
        EditorApplication.Exit(report.summary.result == BuildResult.Succeeded ? 0 : 1);
    }

    static string Arg(string name)
    {
        string[] args = Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length - 1; i++)
            if (args[i] == name) return args[i + 1];
        return null;
    }
}
