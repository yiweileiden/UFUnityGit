using TMPro;   
using UnityEngine;   

namespace UnityFundamentals
{
    // This class is responsible for updating a TextMeshPro text component
    // with the value of an associated IntVariable whenever the variable changes.
    // It listens for changes in the `IntVariable` and updates the UI element accordingly.
    // The class automatically subscribes and unsubscribes from the `OnChanged` event
    // when the script is enabled or disabled.
    // 
    // @author J.C. Wichman
    
    public class IntVariableTMP_TextUpdater : MonoBehaviour
    {
        // The IntVariable whose value this script tracks and displays in the text field.
        // This variable needs to be assigned through the Unity Inspector or programmatically.
        [SerializeField] private IntVariable variableToCheck;

        // The TextMeshPro text component that will display the value of the variable.
        // This should be assigned through the Unity Inspector or automatically assigned via `Reset()`.
        [SerializeField] private TMP_Text textField;

        // Automatically assigns the `TMP_Text` component from the GameObject when the component is reset.
        // This is useful for auto-wiring in the editor when the script is added to a GameObject
        // that already has a `TMP_Text` component.
        private void Reset()
        {
            textField = GetComponent<TMP_Text>();
        }

        // Called when the script is enabled. Subscribes to the `OnChanged` event of `variableToCheck`.
        // Whenever the variable changes, the `Check()` method is called to update the text field.
        // Additionally, it immediately updates the text field when enabled, in case the variable 
        // already holds a value.
        private void OnEnable()
        {
            // Subscribe to changes in variableToCheck
            variableToCheck.OnChanged += Check;
            // Update the text field immediately
            Check(variableToCheck);
        }

        // Called when the script is disabled. Unsubscribes from the `OnChanged` event of `variableToCheck`
        // to prevent memory leaks or unintended event handling when the object is no longer active.
        private void OnDisable()
        {
            // Unsubscribe from event to avoid issues
            variableToCheck.OnChanged -= Check;
        }

        // Updates the `TMP_Text` component with the current value of the `IntVariable`.
        // This method is called when `variableToCheck` changes its value.
        private void Check(IntVariable pVariableToCheck)
        {
            // Update the text field to display the current value of the variable
            textField.text = "" + pVariableToCheck.GetValue();
        }
    }
}
