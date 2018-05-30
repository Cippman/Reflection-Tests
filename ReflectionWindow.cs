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
//        [System.Serializable]
//        public struct TypeReference
//        {
//            public Type type;
//
//            public TypeReference(Type type) : this()
//            {
//                this.type = type;
//            }
//            
//        }
//        
//        [System.Serializable]
//        public struct AssemblyReference
//        {
//            public Assembly assembly;
//            public TypeReference[] types;
//            public string[] typeNames;
//
//            public AssemblyReference (Assembly assembly): this()
//            {
//                this.assembly = assembly;
//                Type[] tmpTypes = this.assembly.GetTypes();
//                this.types = new TypeReference[tmpTypes.Length];
//                for (int i = 0; i < types.Length; i++)
//                {
//                    this.types[i] = new TypeReference(tmpTypes[i]);
//                }
//                typeNames = tmpTypes.Select(t => t.FullName).ToArray();
//            }
//        }
//
//        private AssemblyReference[] assemblies = null;
//        [SerializeField] private string[] assemblyNames;
//        
//        [SerializeField] private int assemblyIndex;
//        [SerializeField] private int typeIndex;
        
//        private Assembly currentAssembly;
        private Type currentType;

        private string inputType;
        
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
//            Assembly[] tmpAssemblies = AppDomain.CurrentDomain.GetAssemblies();
//            assemblies = new AssemblyReference[tmpAssemblies.Length];
//            for (int i = 0; i < tmpAssemblies.Length; i++)
//            {
//                assemblies[i] = new AssemblyReference(tmpAssemblies[i]);
//            }
//            assemblyNames = tmpAssemblies.Select(s => s.FullName).ToArray();
            
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
                if (currentType != null)
                {
                    RepaintType(currentType);
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(ser_fields);
            EditorGUILayout.PropertyField(ser_properties);
            EditorGUILayout.PropertyField(ser_methods);

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
