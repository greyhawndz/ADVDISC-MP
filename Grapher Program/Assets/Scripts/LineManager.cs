using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class LineManager : MonoBehaviour {
	public LineManager hyperbolicRenderer;
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
	
	public void RenderHyperbola(Vector3 vertex, int a, int b){
		Vector3 vertex1;
		Vector3 vertex2;
		float xOffset = Mathf.Sqrt((float) a);
		float yOffset = Mathf.Sqrt((float) b);
		vertex1 = new Vector3(vertex.x, vertex.y + yOffset,0);
		vertex2 = new Vector3(vertex.x, vertex.y - yOffset, 0);
		
		RenderParabola(51,1, vertex1);
		hyperbolicRenderer.RenderParabola(51,-1,vertex2);
	}
	
	public void RenderHyperbolaHorizontal(Vector3 vertex, int a, int b){
		Vector3 vertex1;
		Vector3 vertex2;
		float xOffset = Mathf.Sqrt((float) a);
		float yOffset = Mathf.Sqrt((float) b);
		vertex1 = new Vector3(vertex.x + xOffset, vertex.y, 0);
		vertex2 = new Vector3(vertex.x - xOffset, vertex.y, 0);
		
		RenderParabolaHorizontal(51,1, vertex1);
		hyperbolicRenderer.RenderParabolaHorizontal(51,-1,vertex2);
	}
	
	public void RenderParabola(int segments, int magnitude, Vector3 vertex){
		Debug.Log ("Hi");
		vertices = new Vector3[segments * 2];
		int i = 0;
		int x = (segments-1) / 2;
		
		float y;
		lineRender.SetVertexCount(segments - 1);
		
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
	
	public void RenderParabolaHorizontal(int segments, int magnitude, Vector3 vertex){
		Debug.Log ("Hi");
		vertices = new Vector3[segments * 2];
		int i = 0;
		int y = (segments-1) / 2;
		
		float x;
		lineRender.SetVertexCount(segments - 1);
		
		while(y > 0){
			x = magnitude * Mathf.Pow(y - vertex.y, 2) + vertex.x;
			Debug.Log ("When x = " +x +" , y = " +y);
			vertices[i] = new Vector3(x,y,0);
			Debug.Log ("Pos: " +vertices[i]);
			lineRender.SetPosition (i,new Vector3(x,y,0) );
			i++;
			y--;
			
		}
		while(y < (segments-1) / 2){
			x = magnitude * Mathf.Pow(-y - vertex.y, 2) + vertex.x;
			Debug.Log ("When x = " +x +" , y = " +y);
			vertices[i] = new Vector3(x,-y,0);
			Debug.Log ("Pos: " +vertices[i]);
			lineRender.SetPosition (i,new Vector3(x,-y,0) );
			i++;
			y++;
		}
		lineRender.SetWidth(0.2f,0.2f);
		
	}
	
	
	
	public void RemoveLine(){
		if(vertices != null){
			Debug.Log ("Hi");
			lineRender.SetVertexCount(0);
			if(hyperbolicRenderer != null){
				hyperbolicRenderer.GetComponent<LineRenderer>().SetVertexCount(0);
			}
		}
	}
	 
}
