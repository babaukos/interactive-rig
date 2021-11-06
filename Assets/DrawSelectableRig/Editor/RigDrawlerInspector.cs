using UnityEngine;
using UnityEditor;
using SelectableRig;

[CustomEditor(typeof(RigDrawler)), CanEditMultipleObjects]
public class RigDrawlerInspector : Editor 
{
	private RigDrawler tr;

	private void OnEnable() 
	{
		tr = (RigDrawler)target;
	}
	public override void OnInspectorGUI() 
	{
		base.OnInspectorGUI();
		GUI.enabled = tr.skeletRoot == null ? false : true;
		if(GUILayout.Button("Cretae Bones"))
		{
			tr.CreateBone();
		}
		if(GUILayout.Button("Remove Bones"))
		{
			tr.RemoveBone();
		}
		GUI.enabled = true;
	}
}