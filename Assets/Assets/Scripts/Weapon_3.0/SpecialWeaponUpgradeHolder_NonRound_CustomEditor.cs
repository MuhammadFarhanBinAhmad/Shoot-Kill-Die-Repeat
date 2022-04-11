using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(SpecialWeaponUpgradeHolder_NonRound))]
public class SpecialWeaponUpgradeHolder_NonRound_CustomEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var SWUH_NR = target as SpecialWeaponUpgradeHolder_NonRound;

        SWUH_NR.is_MissleLauncher = EditorGUILayout.Toggle("IsMissleLauncher", SWUH_NR.is_MissleLauncher);
        SWUH_NR.is_FlameThrower = EditorGUILayout.Toggle("IsFlameThrower", SWUH_NR.is_FlameThrower);

        using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(SWUH_NR.is_MissleLauncher)))
        {
            if (group.visible)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PrefixLabel("MissleLauncherElements");

                EditorGUILayout.PrefixLabel("MissleLauncherRestTime");
                SWUH_NR.launcher_Rest_Time = EditorGUILayout.FloatField(SWUH_NR.launcher_Rest_Time);
                EditorGUILayout.PrefixLabel("MissleLauncherRestTimeNeeded");
                SWUH_NR.launcher_Rest_Time_Needed = EditorGUILayout.FloatField(SWUH_NR.launcher_Rest_Time_Needed);

                EditorGUILayout.PrefixLabel("MissleLauncherGameObject");
                SWUH_NR.Missle = (GameObject)EditorGUILayout.ObjectField(SWUH_NR.Missle,typeof(GameObject),true);
                EditorGUILayout.PrefixLabel("MissleLauncherSpawnPos");
                SWUH_NR.missle_Spawn_Pos = EditorGUILayout.ObjectField(SWUH_NR.missle_Spawn_Pos,typeof(Transform),true) as Transform;
                EditorGUI.indentLevel--;
            }
        }

        using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(SWUH_NR.is_FlameThrower)))
        {
            if (group.visible)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PrefixLabel("FlameThrowerElements");

                EditorGUILayout.PrefixLabel("FlameThrowerVFX");
                SWUH_NR.effect_Fire = (ParticleSystem)EditorGUILayout.ObjectField(SWUH_NR.effect_Fire, typeof(ParticleSystem), true);
                EditorGUILayout.PrefixLabel("FlameThrowerCollider");
                SWUH_NR.collider_Fire = (GameObject)EditorGUILayout.ObjectField(SWUH_NR.collider_Fire, typeof(GameObject), true);
                EditorGUILayout.PrefixLabel("FireSpawnPos");
                SWUH_NR.fire_Spawn_Pos = EditorGUILayout.ObjectField(SWUH_NR.fire_Spawn_Pos, typeof(Transform), true) as Transform;
                EditorGUILayout.PrefixLabel("FireRate");
                SWUH_NR.fire_Rate = EditorGUILayout.FloatField(SWUH_NR.fire_Rate);
                EditorGUILayout.PrefixLabel("FlameThroweRestTime");
                SWUH_NR.FlameThrower_Rest_Time = EditorGUILayout.FloatField(SWUH_NR.FlameThrower_Rest_Time);
                EditorGUILayout.PrefixLabel("FlameThroweRestTimeNeeded");
                SWUH_NR.FlameThrower_Rest_Time_Needed = EditorGUILayout.FloatField(SWUH_NR.FlameThrower_Rest_Time_Needed);
                EditorGUI.indentLevel--;
            }
        }
    }
}
