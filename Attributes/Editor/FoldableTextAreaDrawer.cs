/*======= Copyright (c) Immerxive Srl, All rights reserved. =========================
 * 
 * Author: Alessandro Salani (Cippo)
 * 
 * Purpose: same attribute as unity's TextArea, but foldable.
 *
 * Notes: 
 * 
 * ==================================================================================
 */
using UnityEngine;
using UnityEditor;
using CippSharp;

namespace IMXEditor.Attributes
{
	[CustomPropertyDrawer(typeof(FoldableTextAreaAttribute))]
	public class FoldableTextAreaDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType == SerializedPropertyType.String)
			{
				Rect foldoutRect = position;
				foldoutRect.height = EditorGUIUtility.singleLineHeight;
				string validName = (property.isExpanded) ? "" : property.displayName; 
				property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, new GUIContent(validName), EditorStyles.foldout);

				if (property.isExpanded)
				{
					Rect tmpPosition = position;
					label = EditorGUI.BeginProperty(tmpPosition, label, property);
					Rect labelPosition = EditorGUI.IndentedRect(tmpPosition);
					labelPosition.height = 16f;
					tmpPosition.yMin += labelPosition.height;
					EditorGUI.HandlePrefixLabel(tmpPosition, labelPosition, label);
					EditorGUI.BeginChangeCheck();
					string str = EditorGUI.TextArea(tmpPosition, property.stringValue);
					if (EditorGUI.EndChangeCheck())
					{
						property.stringValue = str;
					}
					EditorGUI.EndProperty();
				}
			}
			else
			{
				EditorGUI.LabelField(position, label.text, "Use FoldableTextAreaAttribute with string.");
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (property.isExpanded)
			{
				FoldableTextAreaAttribute attribute = this.attribute as FoldableTextAreaAttribute;
				float textAreaHeight = (float) ((Mathf.Clamp(
						          Mathf.CeilToInt(EditorStyles.textArea.CalcHeight(new GUIContent(property.stringValue), 
							      EditorGUIUtility.currentViewWidth) / 13f), attribute.minLines, attribute.maxLines) -1) * 13);
				return 32f + textAreaHeight;
			}
			else
			{
				return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
			}
		}

	}
}