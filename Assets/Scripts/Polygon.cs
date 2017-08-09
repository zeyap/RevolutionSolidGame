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

	public int panelIndex;

	public Polygon(int newIndex,int newPanelIndex,Vector3 newInitialPos){
		index = newIndex;
		panelIndex = newPanelIndex;
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
		if (panelIndex ==-1) {
			isKilled=true;
		}
	}
		

	public void Recover(int panelIndex){
		isKilled = false;
		panelIndex = panelIndex;
	}

}
