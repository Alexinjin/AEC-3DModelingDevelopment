using UnityEngine;
using System.Collections.Generic;
using System.Collections;
namespace UCIAPEP
{
	public class Building
	{
		float slabHeightDef = 0.5f;
		float slabExtentDef = 0.25f;

	}

	public class Storey
	{
		public Storey(Zone zone)
		{
			//check whether in zone
		}
		Vector3 center;
		float maxHeight=6f; //Maxium height of a storey, for pillars
		//List<Vector3> positions;//pillar pos
		//List<Vector3> height;//pillar heights
		List<Pillar> pillars = new List<Pillar>();
		//must be encircled for now
	}
	public class Roof
	{

	}
	public class Pillar//virtual pillar for datastructure
	{
		Vector3 pos;
		float height;
	}
	public class Column//mesh to represent pillar
	{
		
	}
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class Wall : MonoBehaviour {
		public Camera mainCam;
		MeshRenderer renderer;
		MeshFilter filter;
		public Mesh mesh;
		//public Vector3 nextPos;
		public List<Vector3> vertiList = new List<Vector3>();
		public List<Vector3> normalList = new List<Vector3>();
		public List<int> triList = new List<int>();
		public bool isDrawBack = true;
		public bool isEnclosed = false;
		void Start () {
			renderer = GetComponent<MeshRenderer>();
			filter = GetComponent<MeshFilter>();
			mesh = new Mesh();
			filter.mesh = mesh;
		}
		
		// Update is called once per frame
		void Update () 
		{
			RaycastHit hitInfo;
			if (Input.GetKeyDown(KeyCode.Space) && !isEnclosed)
			{
				if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward))
				{
					
					Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hitInfo);
					//vertices
					AddPillar(hitInfo.point);
					RewriteMesh();
				}
			}
			if (Input.GetKeyDown(KeyCode.Return))
			{
				EndWall();
				RewriteMesh();
				isEnclosed = true;
				Debug.Log("Wall : enclosed");
			}
		}
		public void CreateBuilding()
		{
			//zone constrain

		}
		public void AddPillar(Vector3 pos)
		{
			if (vertiList.Count == 0)//initiative
			{
				vertiList.Add(new Vector3(pos.x, 0f, pos.z));
				vertiList.Add(new Vector3(pos.x, 5f, pos.z));
			}
			else
			{
				vertiList.Add(new Vector3(pos.x, 0f, pos.z));
				vertiList.Add(new Vector3(pos.x, 5f, pos.z));
				vertiList.Add(new Vector3(pos.x, 0f, pos.z));
				vertiList.Add(new Vector3(pos.x, 5f, pos.z));
			}
			//normal
			if (vertiList.Count <= 2)
			{
				normalList.Add(Vector3.up);
				normalList.Add(Vector3.up);
			}
			else
			{
				//BUG!! wrong triangle seq, problems may occur when it is not a quad
				normalList.Add(Vector3.Cross(vertiList[vertiList.Count - 3-2] - vertiList[vertiList.Count - 2-2],
					vertiList[vertiList.Count - 4-2] - vertiList[vertiList.Count - 2-2]).normalized);
				normalList.Add(Vector3.Cross(vertiList[vertiList.Count - 3-2] - vertiList[vertiList.Count - 2-2],
					vertiList[vertiList.Count - 4-2] - vertiList[vertiList.Count - 2-2]).normalized);

				normalList.Add(Vector3.up);
				normalList.Add(Vector3.up);
				if (vertiList.Count >= 6)
				{
					normalList[vertiList.Count - 6] = Vector3.Cross(vertiList[vertiList.Count - 4] - vertiList[vertiList.Count - 6],
						vertiList[vertiList.Count - 5] - vertiList[vertiList.Count - 6]).normalized;
					normalList[vertiList.Count - 5] = Vector3.Cross(vertiList[vertiList.Count - 4] - vertiList[vertiList.Count - 5],
						vertiList[vertiList.Count - 3] - vertiList[vertiList.Count - 5]).normalized;
				}

			}
			//triangle
			if (vertiList.Count >= 6)
			{
				if (isDrawBack)
				{
					triList.AddRange(new List<int>() {

						vertiList.Count - 1 - 2, vertiList.Count - 3 - 2, vertiList.Count - 4 - 2,
						vertiList.Count - 1 - 2, vertiList.Count - 4 - 2, vertiList.Count - 2 - 2,

						vertiList.Count - 4 - 2, vertiList.Count - 3 - 2, vertiList.Count - 1 - 2,
						vertiList.Count - 2 - 2, vertiList.Count - 4 - 2, vertiList.Count - 1 - 2

					});
				}
				else
				{
					triList.AddRange(new List<int>() {

						vertiList.Count - 1 - 2, vertiList.Count - 3 - 2, vertiList.Count - 4 - 2,
						vertiList.Count - 1 - 2, vertiList.Count - 4 - 2, vertiList.Count - 2 - 2

					});
				}
				/*
							normalList.Add(Vector3.Cross(vertiList[vertiList.Count - 1] - vertiList[vertiList.Count - 1],
								vertiList[vertiList.Count - 2] - vertiList[vertiList.Count - 1]).normalized);*/
			}

		}
		void EndWall()
		{
			if (vertiList.Count > 6)
			{
				vertiList.Add(vertiList[0]);
				vertiList.Add(vertiList[1]);

				normalList.Add(Vector3.Cross(vertiList[vertiList.Count - 3] - vertiList[vertiList.Count - 1],//second las vertice
					vertiList[vertiList.Count - 4] - vertiList[vertiList.Count - 1]).normalized);
				normalList.Add(Vector3.Cross(vertiList[vertiList.Count - 1] - vertiList[vertiList.Count - 2],//last vertice
					vertiList[vertiList.Count - 4] - vertiList[vertiList.Count - 2]).normalized);

				normalList[normalList.Count-4] = Vector3.Cross(vertiList[vertiList.Count - 3] - vertiList[vertiList.Count - 1],//second las vertice
					vertiList[vertiList.Count - 4] - vertiList[vertiList.Count - 1]).normalized;
				normalList[normalList.Count-3] = Vector3.Cross(vertiList[vertiList.Count - 1] - vertiList[vertiList.Count - 2],//last vertice
					vertiList[vertiList.Count - 4] - vertiList[vertiList.Count - 2]).normalized;
				//correct
				triList.AddRange(new List<int>() {
					vertiList.Count - 2, vertiList.Count - 1, vertiList.Count - 4,
					vertiList.Count - 4, vertiList.Count - 1, vertiList.Count - 3
				});
				if (isDrawBack)
				{
					triList.AddRange(new List<int>() {
						vertiList.Count - 4, vertiList.Count - 1, vertiList.Count - 2,
						vertiList.Count - 3, vertiList.Count - 1, vertiList.Count - 4
					});
				}
			}
			else
			{
				Debug.Log("Wall : no enough vertices to encircle a wall!");
			}
			//encircle
		}
		public void RewriteMesh()
		{
			mesh.Clear();
			mesh.SetVertices(vertiList);
			mesh.SetNormals(normalList);
			mesh.SetTriangles(triList,0);
		}
		void OnDrawGizmos()
		{
			for (int i = 0; i < normalList.Count; ++i)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawRay(vertiList[i], normalList[i]);
			}
		}	
	}
}