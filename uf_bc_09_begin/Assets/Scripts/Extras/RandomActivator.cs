using UnityEngine;   

namespace UnityFundamentals
{
    // This class is responsible for activating a GameObject based on a random chance when the game starts.
    // It also ensures that the object is deactivated when changes are made in the Unity editor via OnValidate.
    //
    // @author J.C. Wichman

    public class RandomActivator : MonoBehaviour
    {
        // The GameObject that will be conditionally activated.
        // This should be assigned through the Unity Inspector.
        public GameObject objectToActivate;

        // The chance that the object will be activated when the game starts.
        // A value between 0 (never activate) and 1 (always activate), adjustable in the Unity Inspector.
        [Range(0f, 1f)]
        public float chance = 0;

        // Called when the game starts.
        // It activates the object based on a random chance comparison with the `chance` value.
        void Start()
        {
            // If the random value is less than or equal to the `chance`, activate the object.
            if (Random.value <= chance)
                objectToActivate.SetActive(true);
        }

        // Called when a value is changed in the Unity Editor (e.g., when `chance` or `objectToActivate` is adjusted).
        // It ensures that the object is deactivated in the editor until the game starts.
        private void OnValidate()
        {
            // If the objectToActivate is assigned, ensure it is deactivated when modifying settings.
            if (objectToActivate != null)
                objectToActivate.SetActive(false);
        }
    }
}
