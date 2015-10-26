﻿using UnityEngine;
using System.Collections;

public class ParticleGrapher : MonoBehaviour {
	[Range(10,200)]
	public int resolution = 10;
	private ParticleSystem.Particle[] points;
	private ParticleSystem ps;
	private int currentResolution;
	
    public ParticleSystem.Particle[] Points
    {
        get
        {
            return points;
        }
    }

	public ParticleSystem PS
    {
        get
        {
            return ps;
        }
    }
	
	public void RenderPoint(Vector3 vertex ,Color32 c){
		points = new ParticleSystem.Particle[1];
		points[0].position = vertex;
		points[0].color = c;
		points[0].size = 1f;
		ps = this.GetComponent<ParticleSystem>();
		ps.SetParticles(points, points.Length);
	}
	
	
	public void Delete(){
		ps.Clear();
		points = null;
	}
}
