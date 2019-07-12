using System;
using UnityEditor;
using UnityEngine;

namespace Ashkatchap.Annotation {
	[CustomEditor(typeof(Annotation))]
	public class AnnotationEditorMonoBehaviour : AnnotationEditorBase { }

	[CustomEditor(typeof(AnnotationSMB))]
	public class AnnotationEditorSMB : AnnotationEditorBase { }

	public abstract class AnnotationEditorBase : Editor {
		static GUIStyle textAreaStyle;
		SerializedProperty text;

		void OnEnable() {
			text = serializedObject.FindProperty("text");
		}

		public override void OnInspectorGUI() {
			EditorGUILayout.HelpBox("Annotations are stripped from builds", MessageType.Info);

			if (textAreaStyle == null) {
				textAreaStyle = GUI.skin.textArea;
				textAreaStyle.wordWrap = true;
				textAreaStyle.richText = true;
			}

			serializedObject.Update();
			text.stringValue = WithoutSelectAll(() => EditorGUILayout.TextArea(text.stringValue, textAreaStyle));
			serializedObject.ApplyModifiedProperties();
		}

		// https://stackoverflow.com/questions/44097608/how-can-i-stop-immediate-gui-from-selecting-all-text-on-click/44097609#44097609
		private T WithoutSelectAll<T>(Func<T> guiCall) {
			bool preventSelection = (Event.current.type == EventType.MouseDown);

			Color oldCursorColor = GUI.skin.settings.cursorColor;

			if (preventSelection)
				GUI.skin.settings.cursorColor = new Color(0, 0, 0, 0);

			T value = guiCall();

			if (preventSelection)
				GUI.skin.settings.cursorColor = oldCursorColor;

			return value;
		}
	}
}
