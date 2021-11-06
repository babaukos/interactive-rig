#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosSkiletonBones : MonoBehaviour
{
	public Transform Skelet;
	public VaribalState drawPivot = VaribalState.off;
	public VaribalState drawName = VaribalState.off;
	public Color mainColor = Color.gray;

	public enum VaribalState
	{
		on,
		off,
	}

	private void OnDrawGizmos() 
	{
		if(Skelet == null)
			return;
			
		DrawBone(Skelet);
	}
	private void DrawBone(Transform t)
    {
        foreach ( Transform child in t)
        {
            float len = 0.05f;
            Vector3 loxalX = new Vector3(len, 0, 0);
            Vector3 loxalY = new Vector3(0, len, 0);
            Vector3 loxalZ = new Vector3(0, 0, len);
            loxalX = child.rotation * loxalX;
            loxalY = child.rotation * loxalY;
            loxalZ = child.rotation * loxalZ;
           
		   	// Draw Bones
			Gizmos.color = mainColor;
            Gizmos.DrawLine(t.position * 0.1f + child.position * 0.9f,  t.position * 0.9f + child.position * 0.1f);
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(child.position, len / 8);
			
			// Draw Pivot Bones
		   	if(drawPivot == VaribalState.on)
			{
			   	Gizmos.color = Color.red;
				Gizmos.DrawLine(child.position,  child.position + loxalX);
				Gizmos.color = Color.green;
				Gizmos.DrawLine(child.position,  child.position + loxalY);
				Gizmos.color =  Color.blue;
				Gizmos.DrawLine(child.position,  child.position + loxalZ);
			}
			if(drawName == VaribalState.on)
			{
				DrawString(child.name, child.position, Color.red, Color.black);
			}
			// Recursive draw
            DrawBone(child);
        }
    }

	private void DrawString(string text, Vector3 worldPos, Color textColor, Color backColor)
	{
		//#if UNITY_EDITOR

		//var view = UnityEditor.SceneView.currentDrawingSceneView;
		//Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);

		//if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
		//{
		//    //GUI.color = restoreColor;
		//    Handles.EndGUI();
		//    return;
		//}

		//Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
		//GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height - 37, size.x, size.y), text);

		UnityEditor.Handles.BeginGUI();
		UnityEditor.Handles.color = Color.blue;
		GUIStyle content = new GUIStyle();
		content.normal.textColor = textColor;
		content.normal.background = MakeTex(1, 1, backColor);

		UnityEditor.Handles.Label(worldPos, text, content);
		UnityEditor.Handles.EndGUI();
		//#endif
	}
	private  Texture2D MakeTex(int width, int height, Color col)
	{
		Color[] pix = new Color[width * height];

		for (int i = 0; i < pix.Length; i++)
			pix[i] = col;

		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pix);
		result.Apply();

		return result;
	}
}
#endif