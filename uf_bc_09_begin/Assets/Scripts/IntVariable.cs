using System;
using System.Diagnostics;
using UnityEngine;

namespace UnityFundamentals
{

    // ScriptableObject representing a Integer Variable with a value.
    // 
    // To create an IntVariable asset, right click on a folder in the project window and choose Create > IntVariable.
    // 
    // This script uses a LOT of advanced features such as [SerializeField], custom C# events, generics, 
    // conditional compilation, statics, explicit access modifiers etc. You can IGNORE ALL OF THIS.
    // 
    // The key is in how to use this ScriptableObject:
    // -> just create an IntVariable, drag it into a UnityEvent or IntVariableCondition
    // (and call it public methods ChangeBy and ChangeTo where applicable)
    //
    // @author J.C. Wichman

    [CreateAssetMenu]
    public class IntVariable : ScriptableObject
    {
        //Here we MUST use private since accessing the value directly will not trigger changed events.
        [SerializeField] private int value = 0;

        public event Action<IntVariable> OnChanged;

        public void ChangeValueBy (int pValue)
        {
            SetValueAndDispatchEvent(value + pValue);
        }

        public void ChangeValueTo (int pValue)
        {
            SetValueAndDispatchEvent(pValue);
        }

        public int GetValue()
        {
            Log("returning value");
            return value;
        }

        private void SetValueAndDispatchEvent (int pNewValue, bool pForce = false)
        {
            if (value == pNewValue) return;

            Log("setting value to");

            value = pNewValue;
            OnChanged?.Invoke(this);
        }

        [Conditional("UNITY_EDITOR")]
        private void Log(string pMsg)
        {
            if (PlayerPrefs.HasKey("VARIABLE_LOGGING")) UnityEngine.Debug.Log("<color=blue>[ITEM] => " + name + " " + pMsg + " " + value + "</color>");
        }

#if UNITY_EDITOR
        [SerializeField][HideInInspector] private int editValue;

        // register an event handler when the class is initialized
        void OnEnable()
        {
            UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDisable()
        {
            UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange state)
        {
            switch (state)
            {
                case UnityEditor.PlayModeStateChange.ExitingEditMode: editValue = value; break;            
                case UnityEditor.PlayModeStateChange.EnteredEditMode: value = editValue; break;            
            }
        }

        private void OnValidate()
        {
            if (Application.isPlaying) OnChanged?.Invoke(this);
        }

        [UnityEditor.MenuItem("Tools/Toggle variable logging")]
        private static void ToggleLogging()
        {
            if (PlayerPrefs.HasKey("VARIABLE_LOGGING"))
            {
                PlayerPrefs.DeleteKey("VARIABLE_LOGGING");
            }
            else
            {
                PlayerPrefs.SetInt("VARIABLE_LOGGING",1);
            }
        
            UnityEngine.Debug.Log("Variable logging is now:" + (PlayerPrefs.HasKey("VARIABLE_LOGGING") ? "on" : "off"));
        }

#endif
    }
}
