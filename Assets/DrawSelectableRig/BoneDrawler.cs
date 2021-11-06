#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace SelectableRig
{
[SelectionBase]
public class BoneDrawler : MonoBehaviour 
{
	[SerializeField, HideInInspector]
	private RigDrawler rigDrawler;
	public RigDrawler RigDrawler
	{
		get
		{
			return rigDrawler;
		}
	}
	
	public void BoneInit(RigDrawler rc)
	{
		rigDrawler = rc;
	}
	private void OnDrawGizmos() 
	{
		Vector3 curent = transform.position;
		
		if(RigDrawler.drawAxis == VaribalState.on)
		{
			// Draw Pivot Bones
			Gizmos.color = Color.red;
			Gizmos.DrawRay(curent, transform.right * 0.1f);
			Gizmos.color = Color.green;
			Gizmos.DrawRay(curent, transform.up * 0.1f);
			Gizmos.color =  Color.blue;
			Gizmos.DrawRay(curent, transform.forward * 0.1f);
		}

		foreach ( Transform child in transform)
        {
			Vector3 chil = child.position;
			Vector3 midle = (chil + curent) / 2;
			Vector3 dir = chil - curent;

			Gizmos.color = new Color(1, 1, 1, 0);
			Gizmos.matrix = Matrix4x4.TRS(midle, Quaternion.LookRotation(dir), new Vector3(rigDrawler.thickness, rigDrawler.thickness, dir.magnitude));
			Gizmos.DrawCube(Vector3.zero, Vector3.one);
			Gizmos.DrawSphere(Vector3.zero, rigDrawler.thickness * 1.5f);	

			Handles.color = rigDrawler.normalColor;
            Handles.zTest = UnityEngine.Rendering.CompareFunction.Always;
			Handles.DrawBezier(curent, chil, curent, chil, Color.red, null, 100 * rigDrawler.thickness);
			Handles.color = rigDrawler.selectColor;
			Handles.DrawSphere(0, curent, Quaternion.LookRotation(dir), rigDrawler.thickness * 1.5f);
		}
		
		if(RigDrawler.drawName == VaribalState.on)
		{
			DrawString(gameObject.name, curent, Color.red, Color.black);
		}
	}
	// private void OnDrawGizmosSelected() 
	// {
	// 	foreach ( Transform child in transform)
    //     {
	// 		var shild = child.position;
	// 		var curent = transform.position;
	// 		var midle = (shild + curent) / 2;
	// 		var dir = shild - curent;

	// 		// Gizmos.color = rigDrawler.selectColor;
	// 		// Gizmos.matrix = Matrix4x4.TRS(midle, Quaternion.LookRotation(dir), new Vector3(rigDrawler.thickness, rigDrawler.thickness, dir.magnitude));
	// 		// Gizmos.DrawCube(Vector3.zero, Vector3.one);

	// 		// Gizmos.color = Color.yellow;
	// 		// Gizmos.matrix = Matrix4x4.TRS(curent, Quaternion.LookRotation(dir), Vector3.one);
	// 		// Gizmos.DrawSphere(Vector3.zero, rigDrawler.thickness * 1.5f);


	// 		UnityEditor.Handles.color = rigDrawler.selectColor;
    //         UnityEditor.Handles.zTest = UnityEngine.Rendering.CompareFunction.Never;
	// 		UnityEditor.Handles.DrawBezier(transform.position, shild, transform.position, shild, Color.red, null, rigDrawler.thickness);
	// 	}
	// }

	private void DrawString(string text, Vector3 worldPos, Color textColor, Color backColor)
	{
		var view = UnityEditor.SceneView.currentDrawingSceneView;
		Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);

		if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
		{
		   return;
		}
		
		UnityEditor.Handles.BeginGUI();
		UnityEditor.Handles.color = Color.blue;
		GUIStyle content = new GUIStyle();
		content.normal.textColor = textColor;
		content.normal.background = MakeTex(1, 1, backColor);

		UnityEditor.Handles.Label(worldPos, text, content);
		UnityEditor.Handles.EndGUI();
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
}
#endif
