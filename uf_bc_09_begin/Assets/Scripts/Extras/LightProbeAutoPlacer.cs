using UnityEngine;

namespace UnityFundamentals
{
    [RequireComponent(typeof(LightProbeGroup))]
    public class LightProbeAutoPlacer : MonoBehaviour
    {
#if UNITY_EDITOR

        [Header("Bounds Settings")]
        public Vector3 boundsSize = new Vector3(10, 10, 10); // The size of the area to fill with light probes
        public Vector3 boundsCenter = Vector3.zero;          // Center of the bounds relative to the Light Probe Group

        [Header("Probe Placement Settings")]
        public float probeSpacing = 2.0f; // Distance between light probes
        private float probeRadius = 0.1f;  // Radius for collider check

        private LightProbeGroup lightProbeGroup;

        [ContextMenu("Generate Light Probes")]
        public void GenerateLightProbes()
        {
            lightProbeGroup = GetComponent<LightProbeGroup>();

            // Calculate bounds
            Bounds bounds = new Bounds(boundsCenter, boundsSize);

            // Create a list to hold probe positions
            var probePositions = new System.Collections.Generic.List<Vector3>();

            // Loop through the bounds using the specified spacing
            for (float x = bounds.min.x; x <= bounds.max.x; x += probeSpacing)
            {
                for (float y = bounds.min.y; y <= bounds.max.y; y += probeSpacing)
                {
                    for (float z = bounds.min.z; z <= bounds.max.z; z += probeSpacing)
                    {
                        Vector3 probePosition = new Vector3(x, y, z);

                        // Check if the position is valid
                        if (!IsProbePositionValid(probePosition)) continue;

                        // Add the position to the list
                        probePositions.Add(probePosition);
                    }
                }
            }

            // Set the positions in the Light Probe Group
            lightProbeGroup.probePositions = probePositions.ToArray();
            Debug.Log($"Generated {probePositions.Count} light probes.");
        }

        // Validation logic: Ensure probe is not inside a collider
        private bool IsProbePositionValid(Vector3 position)
        {
            // Check if there are any colliders overlapping the probe position
            Collider[] overlappingColliders = Physics.OverlapSphere(position, probeRadius);
            return overlappingColliders.Length == 0;
        }

        private void OnDrawGizmosSelected()
        {
            // Draw the bounds in the editor for visualization
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position + boundsCenter, boundsSize);
        }

#endif
    }
}
