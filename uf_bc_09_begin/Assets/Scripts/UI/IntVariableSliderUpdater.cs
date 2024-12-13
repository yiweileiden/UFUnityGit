using UnityEngine;   
using UnityEngine.UI;

namespace UnityFundamentals
{
    // This class is responsible for updating a UI Slider component with the value of an associated IntVariable
    // whenever the variable changes. It listens for changes in the `IntVariable` and updates the UI element accordingly.
    // The class automatically subscribes and unsubscribes from the `OnChanged` event when the script is enabled or disabled.
    // 
    // @author J.C. Wichman

    public class IntVariableSliderUpdater : MonoBehaviour
    {
        // The IntVariable whose value this script tracks and displays on the slider.
        // This variable needs to be assigned through the Unity Inspector or programmatically.
        [SerializeField] private IntVariable variableToCheck;

        // The Slider UI component that will reflect the value of the variable.
        // This should be assigned through the Unity Inspector or automatically assigned via `Reset()`.
        [SerializeField] private Slider slider;

        // Automatically assigns the `Slider` component from the GameObject when the component is reset.
        // This is useful for auto-wiring in the editor when the script is added to a GameObject that already has a `Slider` component.
        private void Reset()
        {
            slider = GetComponent<Slider>();
        }

        // Called when the script is enabled. Subscribes to the `OnChanged` event of `variableToCheck`.
        // Whenever the variable changes, the `Check()` method is called to update the slider.
        // Additionally, it immediately updates the slider when enabled, in case the variable already holds a value.
        private void OnEnable()
        {
            // Subscribe to changes in variableToCheck
            variableToCheck.OnChanged += Check;
            // Update the slider value immediately
            Check(variableToCheck);
        }

        // Called when the script is disabled. Unsubscribes from the `OnChanged` event of `variableToCheck`
        // to prevent memory leaks or unintended event handling when the object is no longer active.
        private void OnDisable()
        {
            // Unsubscribe from event to avoid issues
            variableToCheck.OnChanged -= Check;
        }

        // Updates the `Slider` component with the current value of the `IntVariable`.
        // This method is called when `variableToCheck` changes its value.
        private void Check(IntVariable pVariableToCheck)
        {
            // Update the slider to reflect the current value of the variable
            slider.value = pVariableToCheck.GetValue();
        }
    }
}
