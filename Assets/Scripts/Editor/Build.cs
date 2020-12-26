using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class Build
{
    [MenuItem("File/Build Android", false, 220)]
    static void BuildAndroidMenu()
    {
        BuildTargetGroup currentBuildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        BuildTarget currentBuildTarget = EditorUserBuildSettings.activeBuildTarget;

        BuildSummary summary = BuildAndroid();

        EditorUserBuildSettings.SwitchActiveBuildTargetAsync(currentBuildTargetGroup, currentBuildTarget);
    }

    static BuildSummary BuildAndroid()
    {
        string assetsDir = Application.dataPath;

        string[] scenes = new string[EditorBuildSettings.scenes.Length];
        for (int i = 0; i < scenes.Length; ++i)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }

        BuildPlayerOptions options = new BuildPlayerOptions();
        options.scenes = scenes;
        options.locationPathName = $"{assetsDir}/../AndroidBuild";
        options.target = BuildTarget.Android;
        options.options |= BuildOptions.Development;
        // options.options |= BuildOptions.AcceptExternalModificationsToPlayer;
        EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
        EditorUserBuildSettings.exportAsGoogleAndroidProject = true;

        BuildReport report = BuildPipeline.BuildPlayer(options);
        BuildSummary summary = report.summary;

        return summary;
    }
}
