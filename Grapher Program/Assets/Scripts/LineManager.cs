using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class LineManager : MonoBehaviour {
	private LineRenderer lineRender;
	// Use this for initialization
	void Start () {
		lineRender = GetComponent<LineRenderer>();
	}
	
	
	
	public void RenderLine(Vector3[] data){
		Vector3[] vertices = data;
		lineRender.SetVertexCount(vertices.Length);
		for(int i = 0; i < vertices.Length; i++){
			Vector3 pos = vertices[i];
			lineRender.SetPosition(i,pos);
		}
		lineRender.SetWidth(0.1f,0.1f);
	}
	 
}
