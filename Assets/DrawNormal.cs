using UnityEngine;
using System.Collections;
[RequireComponent(typeof(MeshFilter))]
public class DrawNormal : MonoBehaviour {
	public MeshFilter filter;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos()
	{	filter = GetComponent<MeshFilter>();
		for (int i = 0; i < filter.mesh.vertices.Length; ++i)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawRay(filter.mesh.vertices[i], filter.mesh.normals[i]);
		}
	}	
}
