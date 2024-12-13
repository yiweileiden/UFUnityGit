using System;
using UnityEngine;

namespace UnityFundamentals
{
    public class TerrainLightMapScaleSetter : MonoBehaviour
    {
#if UNITY_EDITOR

        [Serializable]
        public class TerrainSettings
        {
            public Terrain[] applyTo;
            public float scaleInLightmap = 1;
        }

        public TerrainSettings[] terrainSettings;

        public void ApplyTerrainSettings()
        {
            foreach (TerrainSettings terrainSetting in terrainSettings) {
                foreach (Terrain terrain in terrainSetting.applyTo)
                {
                    UnityEditor.SerializedObject s = new UnityEditor.SerializedObject(terrain);
                    s.FindProperty("m_ScaleInLightmap").floatValue = terrainSetting.scaleInLightmap;
                    s.ApplyModifiedProperties();
                }
            }
        }

#endif
    }
}
