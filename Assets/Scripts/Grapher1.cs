﻿using UnityEngine;

public class Grapher1 : MonoBehaviour {

	//List for Math Graph
	public enum FunctionOption {
		Linear,
		Exponential,
		Parabola,
		Sine
	}

	/// <summary>
	/// The function delegates.
	/// </summary>
	private delegate float FunctionDelegate (float x);
	private static FunctionDelegate [] functionDelegates = {
		Linear,
		Exponential,
		Parabola,
		Sine

	};

	public FunctionOption function;



	[Range(10,100)]
	public int resolution = 10;
	private int currentResolution;

	//declaring particlesystem
	private ParticleSystem.Particle [] points;


	void CreatePoints (){
		if (resolution < 10 || resolution > 100){
			Debug. LogWarning ("resolution out of range, reset now", this);
			resolution = 10;
		}
		currentResolution = resolution;
		points = new ParticleSystem.Particle [resolution];

		float increment = 1f/ (resolution - 1);
		for (int i = 0; i < resolution; i++) {
			//DO SOMETHING
			float x = i * increment; 
			points [i].position = new Vector3 (x,0f,0f);
			points [i].color = new Color (x,0f,0f);
			points [i].size = 0.1f;
		}
	}


	void Update (){
		if (currentResolution != resolution || points == null){
			CreatePoints ();
		}

		FunctionDelegate f = functionDelegates[(int) function];
		//writing out y = x graph, coloring it in. 
		for (int i = 0; i <resolution; i++){
			Vector3 p = points[i].position;
			p.y = f(p.x);
			points[i].position = p;

			Color c = points[i].color;
			c.g = p.y;
			points[i].color = c;
		} 

		particleSystem.SetParticles (points,points.Length);
	}

	private static float Linear (float x){
		return x;
	}

	private static float Exponential(float x){
		return x * x;
	}

	private static float Parabola(float x){
		x = 2f * x - 1f;
		return x * x;
	}

	private static float Sine(float x){
		return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x +Time.timeSinceLevelLoad);
	}




}
