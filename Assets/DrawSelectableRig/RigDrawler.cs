#if UNITY_EDITOR
using UnityEngine;
using System.Collections.Generic;

namespace SelectableRig
{
public enum VaribalState
{
	on,
	off,
}

public class RigDrawler : MonoBehaviour 
{
	public Transform skeletRoot;
	public VaribalState drawAxis = VaribalState.off;
	public VaribalState drawName = VaribalState.off;
	public float thickness = 0.3f;
	public Color normalColor = Color.gray;
	public Color selectColor = Color.magenta;


	[SerializeField, HideInInspector]
	private List<Transform> skeltBones;

	private void OnDestroy() 
	{
		RemoveBone();
	}
	private void CreateBone(Transform t)
    {
        foreach ( Transform child in t)
        {
			BoneDrawler b;
			b = child.gameObject.AddComponent<BoneDrawler>();
			b.BoneInit(this);
			skeltBones.Add(child);

			// Draw Pivot Bones
            CreateBone(child);
        }
    }
	private void RmoveBone(Transform t)
    {
        foreach ( Transform child in t)
        {
			BoneDrawler b;
			b = child.gameObject.GetComponent<BoneDrawler>();
			DestroyImmediate(b);
			skeltBones.Remove(child);

			// Draw Pivot Bones
            RmoveBone(child);
        }
    }

	public void CreateBone()
	{
		CreateBone(skeletRoot);
	}
	public void RemoveBone()
	{
		RmoveBone(skeletRoot);
	}
}
}
#endif