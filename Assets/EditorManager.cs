using UnityEngine;
using System.Collections;

namespace UCIAPEP
{
	public class Zone
	{
		public Vector3 center;
		public float x;
		//public float y;
		public float z;
	}
	public class EditorManager : MonoBehaviour 
	{
		GameObject[] UIGroup;
		public Building buildModifer;
		public ViewportControl viewCtrl;
		bool isSingleEdit = false;//

		// Use this for initialization
		void Awake () {
			viewCtrl = GetComponent<ViewportControl>();
		}
		
		// Update is called once per frame
		void Update () {
		
		}
		void CreateZone(Vector3 center,float x,float z)
		{
			
		}
		void DeleteZone(Zone zone)
		{
			
		}
		void TryCreateBuilding()
		{
			
		}
		void CreateBuilding(Zone zone)
		{
			//enter single mode
		}
		void DeleteBuilding(Zone zone)
		{
			
		}
		void EnterSingleMode(Zone zone)
		{
			
		}
		void ExitSingleMode()
		{
			
		}
	}
}
