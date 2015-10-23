using UnityEngine;
using System.Collections;

public class LineManager : MonoBehaviour {
	public GameObject linePrefab;
	private LineRenderer lineRenderer;
	// Use this for initialization
	void Start () {
		lineRenderer = linePrefab.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void RenderLine(){
		
	}
	
	 
}
