using UnityEditor;
using UnityEngine;

namespace UnityFundamentals
{
    [CustomEditor(typeof(TerrainLightMapScaleSetter))]
    public class TerrainLightMapScaleSetterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Draw the default inspector
            DrawDefaultInspector();

            // Add a space for visual clarity
            EditorGUILayout.Space();

            // Add a "Apply Settings" button
            if (GUILayout.Button("Apply Settings"))
            {
                TerrainLightMapScaleSetter setter = (TerrainLightMapScaleSetter)target;

                // Call the ApplyTerrainSettings method
                setter.ApplyTerrainSettings();

                // Log the action
                Debug.Log("Terrain Lightmap Settings Applied!");

                // Mark the object as dirty to save changes
                EditorUtility.SetDirty(setter);
            }
        }
    }
}
