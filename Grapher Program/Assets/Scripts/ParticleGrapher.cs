using UnityEngine;
using System.Collections;

public class ParticleGrapher : MonoBehaviour {
	[Range(10,100)]
	public int resolution = 10;
	private ParticleSystem.Particle[] points;
	private ParticleSystem ps;
	private int currentResolution;
	Color c = new Color32(19,255,0,255);
	
	
	
	
	
	public void RenderParabola(Vector3 vertex, int magnitude){
		if (currentResolution != resolution || points == null) {
			CreatePoints();
		}
		
		for (int i = 0; i < resolution; i++) {
			Vector3 p = points[i].position;
			Debug.Log("X = " +p.x);
			p.y = Parabola(p.x,vertex, magnitude);
			Debug.Log("P = " +p);
			points[i].position = p;
			points[i].color = c;
			
		}
		ps.SetParticles(points, points.Length);
	}
	
	private void CreatePoints () {
		
		currentResolution = resolution;
		points = new ParticleSystem.Particle[resolution];
		/* Now that we have the points it's time to position them along the X axis.
		The first point should be placed at 0 and the last should be placed at 1. 
		All other points should be placed in between. 
		So the distance, or X increment, between two points is 1 / (resolution - 1).*/
		float increment = 2f / (resolution - 1);
		for (int i = 0; i < resolution; i++) {
			float x = i * increment;
			points[i].position = new Vector3(x, 0, 0f);
			points[i].size = 0.3f;
		}
		
		ps = this.GetComponent<ParticleSystem>();
	}
	
	private static float Parabola(float x, Vector3 vertex, int magnitude){
		float y;
		float temp = x - vertex.x;
		y = magnitude * Mathf.Pow(temp, 2) + vertex.y;
		return y;
	}
}
