using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MoveSelectedObjectsWithCombinedBounds : EditorWindow
{
    private const float moveAmount = 1000f; // Amount to move up by
    private const float raycastDistance = 2000f; // Max distance for raycasting

    [MenuItem("Tools/Move Objects Up and Down with Combined Mesh Bounds")]
    static void MoveUpAndDownWithCombinedBounds()
    {
        if (Selection.transforms.Length == 0)
        {
            Debug.LogWarning("No objects selected.");
            return;
        }

        Undo.RegisterCompleteObjectUndo(Selection.transforms, "Move Objects Up and Down with Combined Mesh Bounds");

        foreach (Transform selectedTransform in Selection.transforms)
        {
            if (selectedTransform != null)
            {
                // Get the combined bounds of the object, including its children
                Bounds combinedBounds;
                if (!GetCombinedMeshBounds(selectedTransform, out combinedBounds))
                {
                    Debug.LogWarning($"No MeshRenderers found on {selectedTransform.name}. Skipping.");
                    continue;
                }

                // Move object up by 1000 units
                Vector3 originalPosition = selectedTransform.position;
                selectedTransform.position += Vector3.up * moveAmount;

                // Perform raycast downward from the bottom of the combined bounds
                Vector3 rayOrigin = originalPosition;
                RaycastHit hit;
                if (Physics.Raycast(rayOrigin, Vector3.down, out hit, raycastDistance))
                {
                    // Adjust the position based on the raycast hit point and the object's combined bounds
                    selectedTransform.position = hit.point;// + Vector3.up * combinedBounds.extents.y;
                    Debug.Log(hit.transform.name);
                }
                else
                {
                    // If no hit, move back to the original position
                    selectedTransform.position = originalPosition;
                    Debug.LogWarning($"No collision detected for object {selectedTransform.name}. Moved back to original position.");
                }
            }
        }

        Debug.Log("Move operation completed with combined mesh bounds.");
    }

    // Recursive method to get all MeshRenderers and calculate combined bounds
    static bool GetCombinedMeshBounds(Transform obj, out Bounds combinedBounds)
    {
        combinedBounds = new Bounds();
        bool hasBounds = false;

        // Get all MeshRenderers in this object and its children
        MeshRenderer[] meshRenderers = obj.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in meshRenderers)
        {
            if (hasBounds)
            {
                combinedBounds.Encapsulate(renderer.bounds);
            }
            else
            {
                combinedBounds = renderer.bounds;
                hasBounds = true;
            }
        }

        return hasBounds;
    }
}
