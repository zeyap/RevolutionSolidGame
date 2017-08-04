using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour {
	public GameObject gameObject;
	public int index;
	public Vector3 initialPos;
	private Material mat;
	public Shader alphaShader;
	public float alphaScale;

	public bool isKilled;

	public Polygon(int newIndex,Vector3 newInitialPos){
		index = newIndex;
		initialPos = newInitialPos;

		gameObject = GameObject.Find ("poly" + index.ToString());
		gameObject.transform.position = initialPos;

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
		alphaScale = 0.0f;
		isKilled = false;
	}

}
