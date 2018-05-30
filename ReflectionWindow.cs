using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using CippSharp.Reflection.Extensions;

namespace CippSharp.Reflection
{
    public class ReflectionWindow : EditorWindow
    {
        private Type currentType;
        [SerializeField] private string inputType;
        
        [SerializeField, FoldableTextArea(1, 255)] public string fields;
        [SerializeField, FoldableTextArea(1, 255)] public string properties;
        [SerializeField, FoldableTextArea(1, 255)] public string methods;
        
        //OnGui
        private Vector2 scrollView;
        private SerializedObject serializedObject;
        private SerializedProperty ser_inputType;
        private SerializedProperty ser_fields;
        private SerializedProperty ser_properties;
        private SerializedProperty ser_methods;

        [MenuItem("Window/Reflection Inspection")]
        private static void OpenReflectionWindow()
        {
            ReflectionWindow window = EditorWindow.GetWindow<ReflectionWindow>();
            window.Show();
        }

        private void OnEnable()
        {
            //OnGui
            serializedObject = new SerializedObject(this);
            ser_inputType = serializedObject.FindProperty("inputType");
            ser_fields = serializedObject.FindProperty("fields");
            ser_properties = serializedObject.FindProperty("properties");
            ser_methods = serializedObject.FindProperty("methods");
        }

        private void OnGUI()
        {
            scrollView = EditorGUILayout.BeginScrollView(scrollView);
            serializedObject.Update();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(ser_inputType);
            
            if (GUILayout.Button("Search", EditorStyles.miniButtonRight))
            {
                SearchType();
                RepaintType(currentType);
            }
            
            EditorGUILayout.EndHorizontal();
            
            bool guiStatus = GUI.enabled;
            GUI.enabled = false;
            EditorGUILayout.PropertyField(ser_fields);
            EditorGUILayout.PropertyField(ser_properties);
            EditorGUILayout.PropertyField(ser_methods);
            GUI.enabled = guiStatus;
            
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndScrollView();
        }

        void SearchType()
        {
            currentType = null;
            Assembly[] tmpAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly tmpAssembly in tmpAssemblies)
            {
                Type[] tmpTypes = tmpAssembly.GetTypes();
                foreach (Type tmpType in tmpTypes)
                {
                    if (tmpType.FullName == inputType)
                    {
                        currentType = tmpType;
                        return;
                    }
                }
            }
        }

        void RepaintType(Type targetType)
        {
            if (targetType == null)
            {
                fields = "Fields:";
                properties = "Properties:";
                methods = "Methods:";
                return;
            }

            List<FieldInfo> fieldInfos = ReflectionUtility.GetFields(targetType);
            fields = "Fields:";
            for (int i = 0; i < fieldInfos.Count; i++)
            {
                fields += string.Format("\n{0}", ReflectionUtility.FieldInfoToString(fieldInfos[i]));
            }
            
            List<PropertyInfo> propertyInfos = ReflectionUtility.GetProperties(targetType);
            properties = "Properties:";
            for (int i = 0; i < propertyInfos.Count; i++)
            {
                properties += string.Format("\n{0}", ReflectionUtility.PropertyInfoToString(propertyInfos[i]));
            }

            methods = "Methods:";
            List<MethodInfo> methodInfos = ReflectionUtility.GetMethods(targetType);
            for (int i = 0; i < methodInfos.Count; i++)
            {
                methods += string.Format("\n{0}", ReflectionUtility.MethodInfoToString(methodInfos[i]));
            }

        }
    }
}
