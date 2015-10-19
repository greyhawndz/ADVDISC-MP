using UnityEngine;
using System.Collections;

public class Grapher1 : MonoBehaviour {
	[Range(10,100)]
	public int resolution = 10;
	private ParticleSystem.Particle[] points;
	private ParticleSystem ps;
	private int currentResolution;
	public enum FunctionOption {
		Linear,
		Exponential,
		Parabola,
		Sine
	}
	
	private delegate float FunctionDelegate (float x);
	private static FunctionDelegate[] functionDelegates = {
		Linear,
		Exponential,
		Parabola,
		Sine
	};
	
	public FunctionOption function;
	
	// Update is called once per frame
	void Update () {
		if (currentResolution != resolution || points == null) {
			CreatePoints();
		}
		FunctionDelegate f = functionDelegates[(int) function];
		for (int i = 0; i < resolution; i++) {
			Vector3 p = points[i].position;
			p.y = 3 * f(p.x);
			points[i].position = p;
			Color c = points[i].color;
			c.g = p.y;
			points[i].color = c;
		}
		ps.SetParticles(points, points.Length);
	}
	
	private void CreatePoints () {
		if (resolution < 10 || resolution > 100) {
			Debug.LogWarning("Grapher resolution out of bounds, resetting to minimum.", this);
			resolution = 10;
		}
		currentResolution = resolution;
		points = new ParticleSystem.Particle[resolution];
		/* Now that we have the points it's time to position them along the X axis.
		The first point should be placed at 0 and the last should be placed at 1. 
		All other points should be placed in between. 
		So the distance, or X increment, between two points is 1 / (resolution - 1).*/
		float increment = 1f / (resolution - 1);
		for (int i = 0; i < resolution; i++) {
			float x = i * increment;
			points[i].position = new Vector3(x, 0, 0f);
			points[i].color = new Color(x, 0, 0f);
			points[i].size = 0.3f;
		}
		
		ps = this.GetComponent<ParticleSystem>();
	}
	
	private static float Linear (float x) {
		return 2 * Mathf.Pow(x,2) + x + 3;
	}
	
	private static float Exponential (float x) {
		return x * x;
	}
	
	private static float Parabola (float x){
		x = 2f * x - 1f;
		return x * x;
	}
	
	private static float Sine (float x){
		return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x);
	}
}
