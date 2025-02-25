using UnityEngine;
using System;
using com.ondad.alertpanels;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace com.ondad.alertpanels
{
    [CreateAssetMenu(fileName = "AlertPanel_Config", menuName = "Config/AlertPanel_Config", order = 1)]
    public class AlertPanel_Config : ScriptableObject
    {
        private static AlertPanel_Config instance;

        // Example configuration sections (customize these based on your needs)
        [Header("Alert Settings")]
        [SerializeField] private AlertSettings alertSettings;

        // Singleton accessor with automatic asset loading
        public static AlertPanel_Config Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<AlertPanel_Config>("AlertPanel_Config");
                    if (instance == null)
                    {
                        Debug.LogWarning("AlertPanel_Config not found in Resources folder. Using default settings.");
                        instance = CreateInstance<AlertPanel_Config>();
                    }
                }
                return instance;
            }
        }

        // Getters for each settings section
        public AlertSettings alertConfig => alertSettings;

        // Configuration section classes
        [Serializable]
        public class AlertSettings
        {
            [Tooltip("Default UI Button hoveredScale")]
            public float uiButtonHoverScale = 1.2f;

            [Tooltip("Default UI Button speed")]
            public float uiBtnAnimSpeed = 0.1f;

            [Tooltip("Default UI Panel speed")]
            public float uiPanelAnimSpeed = 0.1f;
        }
    }

#if UNITY_EDITOR
    // Custom editor to add helpful buttons and organization
    [CustomEditor(typeof(AlertPanel_Config))]
    public class MetaConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            AlertPanel_Config config = (AlertPanel_Config)target;

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox(
                "This is the main configuration asset for the alert. Place this in a Resources folder to auto-load.",
                MessageType.Info
            );
            EditorGUILayout.Space();
            DrawDefaultInspector();
        }

    }
#endif
}