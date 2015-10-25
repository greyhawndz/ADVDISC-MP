using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class LineManager : MonoBehaviour {
	private LineRenderer lineRender;
	private Vector3[] vertices;

    public Vector3[] Vertices
    {
        get
        {
            return vertices;
        }
    }


	// Use this for initialization
	void Start () {
		lineRender = GetComponent<LineRenderer>();
	}
	
	
	
	public void RenderLine(Vector3[] data, Color color){
		vertices = data;
		lineRender.SetVertexCount(vertices.Length);
		for(int i = 0; i < vertices.Length; i++){
			Vector3 pos = vertices[i];
			lineRender.SetPosition(i,pos);
		}
		lineRender.SetWidth(0.2f,0.2f);
        lineRender.SetColors(color, color);
	}
	
	public void RenderEllipse(int segments, float xradius, float yradius, Vector3 vertex){
		float x;
		float y;
		float z = 0f;
		vertices = new Vector3[segments+1];
		float angle = 20f;
		lineRender.SetVertexCount(segments + 1);
		for (int i = 0; i < (segments + 1); i++)
		{
			x = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius + vertex.x;
			y = Mathf.Cos (Mathf.Deg2Rad * angle) * yradius + vertex.y;
			vertices[i] = new Vector3(x,y,z);
			Debug.Log(vertices[i]);
			lineRender.SetPosition (i,new Vector3(x,y,z) );
			
			angle += (360f / segments);
		}
		
		lineRender.SetWidth(0.2f,0.2f);
	}
	
	public void RenderParabola(int segments, int magnitude, Vector3 vertex){
		Debug.Log ("Hi");
		vertices = new Vector3[segments * 2];
		int temp = 0;
		int i = 0;
		int x = (segments-1) / 2;
		
		float y;
		lineRender.SetVertexCount(segments - 1);
		/*for(int i = 50; i > vertex.x; i--){
			y = magnitude * Mathf.Pow(i - vertex.x, 2) + vertex.y;
			vertices[i] = new Vector3(i,y,0);
			Debug.Log ("Pos: " +vertices[i]);
			lineRender.SetPosition(i, new Vector3(i,y,0));
		} */
		
		while(x > 0){
			y = magnitude * Mathf.Pow(x - vertex.x, 2) + vertex.y;
			Debug.Log ("When x = " +x +" , y = " +y);
			vertices[i] = new Vector3(x,y,0);
			Debug.Log ("Pos: " +vertices[i]);
			lineRender.SetPosition (i,new Vector3(x,y,0) );
			i++;
			x--;
		
		}
		while(x < (segments-1) / 2){
			y = magnitude * Mathf.Pow(-x - vertex.x, 2) + vertex.y;
			Debug.Log ("When x = " +x +" , y = " +y);
			vertices[i] = new Vector3(-x,y,0);
			Debug.Log ("Pos: " +vertices[i]);
			lineRender.SetPosition (i,new Vector3(-x,y,0) );
			i++;
			x++;
		}
		lineRender.SetWidth(0.2f,0.2f);
		
	}
	
	
	
	public void RemoveLine(){
		if(vertices != null){
			Debug.Log ("Hi");
			lineRender.SetVertexCount(0);
		}
	}
	 
}
