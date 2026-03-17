#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config.Editor
{
    using Lang;

    [CustomEditor(typeof(PrefabComponentProviderConfig))]
    public class PrefabComponentProviderConfigEditor : UnityEditor.Editor
    {
        private const string BoxStyle = "box";
        private const int LabelHeight = 25;

        private PrefabComponentProviderConfigEditorModel _model;
        private bool _justValidated;

        private void OnEnable() =>
            _model = new PrefabComponentProviderConfigEditorModel(serializedObject);

        public override void OnInspectorGUI()
        {
            try
            {
                EditorGUI.BeginChangeCheck();

                serializedObject.Update();
                EditorGUILayout.LabelField(Localization.Get(Localization.Keys.TypePathPairs), EditorStyles.boldLabel);
                SerializedProperty pairsProperty = _model.PairsProperty;

                for (int i = 0; i < pairsProperty.arraySize; i++)
                    DrawElement(i);

                DrawFooterButtons();
                serializedObject.ApplyModifiedProperties();

                if (EditorGUI.EndChangeCheck() && _justValidated == false)
                    _model.ClearValidationResult();

                _justValidated = false;
            }
            catch (ExitGUIException)
            {
                throw;
            }
            catch (Exception e)
            {
                Debug.LogError($"Exception in {nameof(PrefabComponentProviderConfigEditor)}: {e}");
                base.OnInspectorGUI();
            }
        }

        private void DrawElement(int index)
        {
            SerializedProperty pairsProperty = _model.PairsProperty;

            if (index < 0 || index >= pairsProperty.arraySize)
            {
                Debug.LogError($"DrawElement: index {index} out of range (array size {pairsProperty.arraySize})");

                return;
            }

            SerializedProperty pair = pairsProperty.GetArrayElementAtIndex(index);

            if (pair == null)
            {
                Debug.LogError($"DrawElement: pair at index {index} is null");

                return;
            }

            SerializedProperty typeNameProp = pair.FindPropertyRelative(TypePathPair.TypeNameProperty);
            SerializedProperty pathProp = pair.FindPropertyRelative(TypePathPair.PathProperty);

            if (typeNameProp == null || pathProp == null)
            {
                Debug.LogError($"DrawElement: cannot find properties 'typeName' or 'path' at index {index}");

                return;
            }

            GameObject currentPrefab = _model.LoadPrefab(pathProp.stringValue);

            EditorGUILayout.BeginVertical(BoxStyle);
            {
                EditorGUILayout.BeginHorizontal();
                {
                    GameObject newPrefab = DrawPrefabFieldCompact(currentPrefab);
                    if (newPrefab != currentPrefab)
                        HandlePrefabChange(newPrefab, pathProp, typeNameProp);

                    EditorGUILayout.BeginVertical(GUILayout.Height(LabelHeight));
                    GUILayout.FlexibleSpace();
                    DrawComponentPopupCompact(pathProp.stringValue, typeNameProp,
                        GUILayout.ExpandWidth(true), GUILayout.MinWidth(LabelHeight));

                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical(GUILayout.Height(LabelHeight));
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.PropertyField(pathProp, GUIContent.none,
                        GUILayout.ExpandWidth(true), GUILayout.MinWidth(LabelHeight));

                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndVertical();

                    GUILayout.FlexibleSpace();
                    DrawRemoveButton(pairsProperty, index);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        private GameObject DrawPrefabFieldCompact(GameObject currentPrefab)
        {
            return (GameObject)EditorGUILayout.ObjectField(
                GUIContent.none,
                currentPrefab,
                typeof(GameObject),
                false,
                GUILayout.MinWidth(LabelHeight),
                GUILayout.MaxWidth(LabelHeight * 8),
                GUILayout.Height(LabelHeight),
                GUILayout.ExpandWidth(true)
            );
        }

        private void DrawComponentPopupCompact(string path, SerializedProperty typeNameProp, params GUILayoutOption[] options)
        {
            if (string.IsNullOrEmpty(path))
            {
                EditorGUILayout.LabelField(Localization.GetContent(Localization.Keys.NoPrefabSelected), options);

                return;
            }

            string[] names = _model.GetCachedComponentNames(path);
            bool hasValidCache = names != null && names.Length > 0;

            if (hasValidCache == false)
            {
                GameObject prefab = _model.LoadPrefab(path);

                if (prefab != null)
                {
                    _model.UpdateComponentCache(path, prefab);
                    names = _model.GetCachedComponentNames(path);
                }
            }

            if (names == null || names.Length == 0)
            {
                EditorGUILayout.LabelField(Localization.GetContent(Localization.Keys.NoComponentsFound), options);

                return;
            }

            int selected = ArrayIndexOf(names, typeNameProp.stringValue);
            if (selected < 0)
                selected = 0;

            if (names.Length == 1)
            {
                EditorGUILayout.LabelField(names[0], options);
                typeNameProp.stringValue = names[0];
            }
            else
            {
                int newSelected = EditorGUILayout.Popup(selected, names, options);

                if (newSelected != selected)
                    typeNameProp.stringValue = names[newSelected];
            }
        }

        private void DrawRemoveButton(SerializedProperty pairsProperty, int index)
        {
            GUIContent minusIcon = EditorGUIUtility.IconContent("Toolbar Minus");
            minusIcon.tooltip = Localization.Get(Localization.Keys.Remove);

            if (GUILayout.Button(minusIcon, GUILayout.Width(LabelHeight), GUILayout.Height(LabelHeight)))
            {
                pairsProperty.DeleteArrayElementAtIndex(index);
                serializedObject.ApplyModifiedProperties();
                GUIUtility.ExitGUI();
            }
        }

        private void DrawAddButton()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUIContent plusIcon = EditorGUIUtility.IconContent("Toolbar Plus");
            plusIcon.tooltip = Localization.Get(Localization.Keys.AddNewPair);

            if (GUILayout.Button(plusIcon, GUILayout.Width(LabelHeight), GUILayout.Height(LabelHeight)))
                _model.PairsProperty.InsertArrayElementAtIndex(_model.PairsProperty.arraySize);

            EditorGUILayout.EndHorizontal();
        }

        private void DrawFooterButtons()
        {
            EditorGUILayout.Space();
            DrawAddButton();
            EditorGUILayout.Space();

            Language newLanguage = (Language)EditorGUILayout.EnumPopup(Localization.Get(Localization.Keys.LanguageLabel), Localization.CurrentLanguage);

            if (newLanguage != Localization.CurrentLanguage)
            {
                Localization.CurrentLanguage = newLanguage;
                _model.ClearValidationResult();
                Repaint();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button(Localization.Get(Localization.Keys.CheckPrefabsText)))
            {
                PrefabComponentProviderConfig config = (PrefabComponentProviderConfig)target;
                PrefabComponentProviderConfigValidator.CheckPresenceComponentsSpecifiedPaths(config);
                _model.ValidateConfig(config);
                _justValidated = true;
            }

            ValidationResult validationResult = _model.LastValidationResult;

            if (validationResult != null && validationResult.HasMessage)
            {
                EditorGUILayout.Space();

                GUIStyle style = new(EditorStyles.label)
                {
                    alignment = TextAnchor.MiddleCenter,
                    normal = { textColor = validationResult.Color },
                    wordWrap = true,
                };

                EditorGUILayout.LabelField(validationResult.Message, style, GUILayout.ExpandWidth(true));
            }
        }

        private void HandlePrefabChange(GameObject newPrefab, SerializedProperty pathProp, SerializedProperty typeNameProp)
        {
            if (newPrefab != null)
            {
                string assetPath = AssetDatabase.GetAssetPath(newPrefab);
                string resourcesPath = _model.GetResourcesRelativePath(assetPath);

                if (resourcesPath != null)
                {
                    pathProp.stringValue = resourcesPath;
                    UpdateComponentListFromPrefab(resourcesPath, newPrefab, typeNameProp);
                }
                else
                {
                    Debug.LogError(string.Format(Localization.Get(Localization.Keys.ErrorPrefabNotInResources), assetPath));
                }
            }
            else
            {
                pathProp.stringValue = string.Empty;
                typeNameProp.stringValue = string.Empty;
            }
        }

        private void UpdateComponentListFromPrefab(string path, GameObject prefab, SerializedProperty typeNameProp)
        {
            _model.UpdateComponentCache(path, prefab);
            string[] names = _model.GetCachedComponentNames(path);

            if (names == null || names.Length == 0)
            {
                Debug.LogError(string.Format(Localization.Get(Localization.Keys.ErrorNoMonoBehaviours), prefab.name));
                return;
            }

            string currentTypeName = typeNameProp.stringValue;
            if (string.IsNullOrEmpty(currentTypeName) == false && ArrayContains(names, currentTypeName))
                return;

            typeNameProp.stringValue = names[0];
        }

        private static bool ArrayContains(string[] array, string value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value)
                    return true;
            }
            return false;
        }

        private static int ArrayIndexOf(string[] array, string value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value)
                    return i;
            }

            return -1;
        }
    }
}
#endif