using UnityEngine;
using System.Collections;

public class ParticleGrapher : MonoBehaviour {
	[Range(10,200)]
	public int resolution = 10;
	private ParticleSystem.Particle[] points;
	private ParticleSystem ps;
	private int currentResolution;
	Color c = new Color32(19,255,0,255);
	Camera main;
	/*public void Start(){
		main = Camera.main;
	}
	
	public void Update(){
		main.transform.position = Camera.main.transform.position;
		this.transform.position = main.transform.position;
	}*/
	
	
	
	public void RenderEllipse(Vector3 vertex, int a, int b){
		if (currentResolution != resolution || points == null) {
			CreatePoints(vertex);
		}
		
		for (int i = 0; i < points.Length; i++) {
			Vector3 p = points[i].position;
			p.y = Ellipse(p.x,a,b, vertex);
			points[i].position = p;
			points[i].color = c;
			//Debug.Log ("Position: " +p);
			
		}
		
		ps.SetParticles(points, points.Length);
	}
	public void RenderParabola(Vector3 vertex, int magnitude){
		if (currentResolution != resolution || points == null) {
			CreatePoints(vertex);
		}
		
		for (int i = 0; i < points.Length; i++) {
			Vector3 p = points[i].position;
			p.y = Parabola(p.x,vertex, magnitude);
			points[i].position = p;
			points[i].color = c;
			Debug.Log ("Position: " +p);
			
		}
		
		
		ps.SetParticles(points, points.Length);
	}
	
	private void CreatePoints (Vector3 vertex) {
		int temp = 0;
		currentResolution = resolution;
		points = new ParticleSystem.Particle[resolution * 2];
		
		/* Now that we have the points it's time to position them along the X axis.
		The first point should be placed at 0 and the last should be placed at 1. 
		All other points should be placed in between. 
		So the distance, or X increment, between two points is 1 / (resolution - 1).*/
		float increment = vertex.x / (resolution - 1);
		if(vertex.x == 0){
			increment = (1f / (resolution));
			
		}
		if(vertex.x < 0){
			temp = (int) -vertex.x;
		}
		for (int i = (int)vertex.x + temp; i < points.Length; i++) {
			float x = i * increment;
			points[i].position = new Vector3(x, 0, 0f);
			Debug.Log("Position: " +points[i].position);
			points[i].size = 0.5f;
		}
		
		
			
		
		
		ps = this.GetComponent<ParticleSystem>();
		
		
	}
	
	private static float Parabola(float x, Vector3 vertex, int magnitude){
		float y;
		float temp = x - vertex.x;
		y = magnitude * Mathf.Pow(temp, 2) + vertex.y;
		return y;
	}
	
	private static float Ellipse(float x, int a, int b, Vector3 vertex){
		float y;
		y = (Mathf.Sqrt((1f - (Mathf.Pow(x - vertex.x,2)/ Mathf.Pow(a,2))) * Mathf.Pow(b,2))) + vertex.y;
		return y;
	}
	
	public void Delete(){
		ps.Clear();
		points = null;
	}
}
