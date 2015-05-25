using UnityEngine;
using UnityEditor;
using UnityEditorInternal;


// [CustomEditor(typeof(Patrolling))]
public class CustomViewAIGuard :MonoBehaviour  {
// 	private ReorderableList list; 
// 	private void OnEnable(){
// 		list = new ReorderableList(serializedObject,serializedObject.FindProperty("waypointsList"),true,true,true,true);
// 		list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused)=> { 
// 			var element = list.serializedProperty.GetArrayElementAtIndex(index);
// 			
// 			Debug.Log(element.type);
// 		    EditorGUI.PropertyField(rect,element.FindPropertyRelative("Transform"),GUIContent.none);
// 	
// 		};
// 	}
// 	
// 	public override void OnInspectorGUI(){
// 		serializedObject.Update();
// 
// 		list.DoLayoutList();
// 		serializedObject.ApplyModifiedProperties();
// 	}
}
