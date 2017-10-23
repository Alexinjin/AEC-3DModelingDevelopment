using UnityEngine;
using System.Collections;

public class BuildingCreate : MonoBehaviour {
	public float width=1f;
	public float height=1f;
	public float length=1f;
	public float spikeHeight= 2f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		

		
		if (!(length < 1 || width < 1 || height < 1))
		{
			if (Input.GetKey(KeyCode.Q))
				width += 0.1f;
			if (Input.GetKey(KeyCode.A))
				width -= 0.1f;
			if (Input.GetKey(KeyCode.W))
				length += 0.1f;
			if (Input.GetKey(KeyCode.S))
				length -= 0.1f;
			if (Input.GetKey(KeyCode.E))
			{
				height += 0.1f;
				//spikeHeight += 0.1f;
			}
			if (Input.GetKey(KeyCode.D))
			{
				height -= 0.1f;
				//spikeHeight -= 0.1f;
			}

			if (width < 1f)
				width = 1f;
			if (height < 1f)
				height = 1f;
			if (length < 1f)
				length = 1f;
			
		}
	}
	void OnGUI()
	{
		//if(Input.GetKey(KeyCode.Space))
		Draw(width,height,length);
	}
	void Draw(float w,float h,float l)
	{


		//BASE
		GL.PushMatrix();
		//GL.LoadPixelMatrix();
		//width dir


		GL.Begin(GL.QUADS);    
		GL.Color(Color.white);    
		GL.Vertex3(w,0,l);    
		GL.Vertex3(-w,0,l);  
		GL.Vertex3(-w,h,l);   
		GL.Vertex3(w,h,l);    
		GL.End();  
		//length dir
		GL.Begin(GL.QUADS);    
		GL.Color(Color.white);    
		  
		GL.Vertex3(w,0,l);    
		GL.Vertex3(w,0,-l);  
		GL.Vertex3(w,h,-l);    
		GL.Vertex3(w,h,l);  
		GL.End();  
		GL.Begin(GL.QUADS);    
		GL.Color(Color.white);    

		GL.Vertex3(-w,0,l);    
		GL.Vertex3(-w,0,-l);  
		GL.Vertex3(-w,h,-l);    
		GL.Vertex3(-w,h,l);    
		GL.End();   

		GL.Begin(GL.QUADS);    
		GL.Color(Color.white);    
		GL.Vertex3(w,0,-l);    
		GL.Vertex3(-w,0,-l);  
		GL.Vertex3(-w,h,-l);    
		GL.Vertex3(w,h,-l);  
		GL.End();   



		//ROOF
		GL.Begin(GL.TRIANGLES);    
		GL.Color(Color.white);       
		GL.Vertex3(0,h+spikeHeight,0);    
		GL.Vertex3(-w,h,l);  
		GL.Vertex3(w,h,l);    
		GL.End();   
		GL.Begin(GL.TRIANGLES);    
		GL.Color(Color.white);       
		GL.Vertex3(0,h+spikeHeight,0);    
		GL.Vertex3(w,h,l);  
		GL.Vertex3(w,h,-l);    
		GL.End();   
		GL.Begin(GL.TRIANGLES);    
		GL.Color(Color.white);       
		GL.Vertex3(0,h+spikeHeight,0);    
		GL.Vertex3(w,h,-l);  
		GL.Vertex3(-w,h,-l);    
		GL.End();   
		GL.Begin(GL.TRIANGLES);    
		GL.Color(Color.white);       
		GL.Vertex3(0,h+spikeHeight,0);    
		GL.Vertex3(-w,h,-l);  
		GL.Vertex3(-w,h,l);    
		GL.End();   


		GL.PopMatrix();
	}
}
