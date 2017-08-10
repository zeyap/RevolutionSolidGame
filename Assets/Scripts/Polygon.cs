using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour {
	public GameObject gameObject;
	public int index;
	private Material mat;
	public Shader alphaShader;

	public Polygon(int newIndex){
		index = newIndex;

		gameObject = GameObject.Find ("poly" + index.ToString());

		if (gameObject.GetComponent<MeshRenderer>().material !=null) {
			mat = gameObject.GetComponent<MeshRenderer>().material;
			if (alphaShader != null) {
				mat.shader = alphaShader;
			} else {
				alphaShader = mat.shader;
				//Debug.Log (shader);
			}
			
		} else {
			gameObject.GetComponent<MeshRenderer> ().material = new Material (alphaShader);
			mat=gameObject.GetComponent<MeshRenderer>().material;
		}

	}
		
}
