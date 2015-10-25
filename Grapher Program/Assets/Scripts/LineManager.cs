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
			lineRender.SetPosition (i,new Vector3(x,y,z) );
			
			angle += (360f / segments);
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
