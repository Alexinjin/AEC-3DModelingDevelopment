using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace UCIAPEP
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class Slab : MonoBehaviour
	{
		MeshRenderer renderer;
		MeshFilter filter;
		public Wall lowerWall = null;
		public Wall upperWall = null;
		public Mesh mesh; 
		public float height = 1f;
		public List<Vector3> vertiList = new List<Vector3>();
		public List<Vector3> normalList = new List<Vector3>();
		public List<int> triList = new List<int>();
		public void Start()
		{
			renderer = GetComponent<MeshRenderer>();
			filter = GetComponent<MeshFilter>();
			mesh = new Mesh();
			filter.mesh = mesh;
		}
		public void Update()
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				CreateSlab(1f);
				RewriteMesh();
			}
		}
		public void CreateSlab(float extension)
		{
			if (extension < 0)
				extension = 0;
			if (lowerWall)
			{
				//create vertical surface
				vertiList = lowerWall.vertiList;
				normalList = lowerWall.normalList;

				Vector3 tempNormal;
				tempNormal = Vector3.Normalize(normalList[0] + normalList[normalList.Count - 2]);
				vertiList[0] += tempNormal * extension;
				vertiList[1] += tempNormal * extension;
				vertiList[vertiList.Count-2] += tempNormal * extension;
				vertiList[vertiList.Count-1] += tempNormal * extension;

				vertiList[1] = new Vector3(vertiList[1].x,height,vertiList[1].z);
				vertiList[vertiList.Count-1] = new Vector3(vertiList[vertiList.Count-2].x,height,vertiList[vertiList.Count-2].z);
				for (int i = 2; i < vertiList.Count-2; i+=4)
				{
					tempNormal = Vector3.Normalize(normalList[i] + normalList[i+2]);
					vertiList[i] += tempNormal * extension;
					vertiList[i+1] += tempNormal * extension;
					vertiList[i+2] += tempNormal * extension;
					vertiList[i+3] += tempNormal * extension;

					vertiList[i+1] = new Vector3(vertiList[i+1].x,height,vertiList[i+1].z);
					vertiList[i+3] = new Vector3(vertiList[i+3].x,height,vertiList[i+3].z);

				}
				triList = lowerWall.triList;

				//create horizontal surface
				vertiList.Add(transform.position-(5-height)*Vector3.up);
				vertiList.Add(transform.position-5*Vector3.up);
				normalList.Add(Vector3.up);
				normalList.Add(Vector3.down);
				for (int i = 0; i < vertiList.Count-2; i+=4)
				{
					//upper
					triList.AddRange(new List<int>() {
						vertiList.Count-2,i+1,i+3,
						i+2,i,vertiList.Count-1
					});
				}
			}
			else
			{
				Debug.Log("Slab : lower wall is missing");
			}
		}
		public void RewriteMesh()
		{
			mesh.Clear();
			mesh.SetVertices(vertiList);
			mesh.SetNormals(normalList);
			mesh.SetTriangles(triList,0);
		}
	}
}
