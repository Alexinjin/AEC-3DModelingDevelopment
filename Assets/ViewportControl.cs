using UnityEngine;
using System.Collections;

namespace UCIAPEP
{
	[RequireComponent(typeof(EditorManager))]
	public class ViewportControl: MonoBehaviour {
		public EditorManager manager;
		public float moveRate = 5f;
		public float rotRate = 5f;
		float pitch = 0f;
		float yaw = 0f;
		// Use this for initialization
		void Start () {
			manager = GetComponent<EditorManager>();
		}
		
		// Update is called once per frame
		void FixedUpdate () {
			yaw += Input.GetAxis("Mouse X");
			pitch -= Input.GetAxis("Mouse Y");
			if (pitch > 90f)
				pitch = 90f;
			if (pitch < -90f)
				pitch = -90f;
		
			//transform.Rotate(new Vector3(0f,, 0f)*Time.deltaTime*rate,Space.World);
			//transform.Rotate(new Vector3(,0f,0f)*Time.deltaTime*rate,Space.World);
			//transform.rotation *= Quaternion.Euler(Input.GetAxis("Mouse Y"),-Input.GetAxis("Mouse X"),0f);
			transform.eulerAngles = new Vector3(pitch*rotRate*Time.fixedDeltaTime,yaw*rotRate*Time.fixedDeltaTime,0f);
			if (Input.GetKey(KeyCode.W))
				transform.position += transform.forward * moveRate * Time.fixedDeltaTime;
			if (Input.GetKey(KeyCode.S))
				transform.position -= transform.forward * moveRate * Time.fixedDeltaTime;
			if (Input.GetKey(KeyCode.A))
				transform.position -= transform.right * moveRate * Time.fixedDeltaTime;
			if (Input.GetKey(KeyCode.D))
				transform.position += transform.right * moveRate * Time.fixedDeltaTime;
		}
		void Update()
		{
			/*
			RaycastHit hitInfo;
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (Physics.Raycast(transform.position, transform.forward))
				{

					Physics.Raycast(transform.position, transform.forward, out hitInfo);
					//vertices
					manager.buildModifer.AddPillar(hitInfo.point);
				}
			}	
			*/
		}
	}
}
