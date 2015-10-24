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
		lineRender.SetWidth(0.1f,0.1f);
        lineRender.SetColors(color, color);
	}
	
	
	
	public void RemoveLine(){
		if(vertices != null){
			Debug.Log ("Hi");
			lineRender.SetVertexCount(0);
		}
	}
	 
}
