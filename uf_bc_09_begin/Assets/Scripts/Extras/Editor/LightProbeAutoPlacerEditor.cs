using UnityEditor;
using UnityEngine;

namespace UnityFundamentals
{
    [CustomEditor(typeof(LightProbeAutoPlacer))]
    public class LightProbeAutoPlacerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Draw the default inspector
            DrawDefaultInspector();

            // Add a space for visual clarity
            EditorGUILayout.Space();

            // Add a "Generate/Update" button
            if (GUILayout.Button("Generate/Update Light Probes"))
            {
                LightProbeAutoPlacer placer = (LightProbeAutoPlacer)target;

                // Call the GenerateLightProbes method
                placer.GenerateLightProbes();

                // Mark the object as dirty so changes are saved
                EditorUtility.SetDirty(placer);
            }
        }
    }
}
