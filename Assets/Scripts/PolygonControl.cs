using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonControl : MonoBehaviour {

	delegate void PolygonBehaviour(int polygonIndex);
	PolygonBehaviour polygonBehaviour;
	public static List<Polygon> polygons;
	// Use this for initialization
	void Start () {
		polygons = new List<Polygon> ();//constructor
		Vector3 newPos=new Vector3(0.2f,-1.1f,-0.4f);
		polygons.Add(new Polygon(0,newPos));

		polygonBehaviour += FadeInOrOut;
		polygonBehaviour += Rotate;
		polygonBehaviour += Move;

		//StartCoroutine("FadeInOrOut");

	}
	
	// Update is called once per frame
	void Update () {
		if (polygonBehaviour != null) {
			for (int i = 0; i < polygons.Count; i++) {
				polygonBehaviour (i);
			}
		}
	}

	void Rotate(int polygonIndex){
		polygons [polygonIndex].gameObject.transform.Rotate (1.5f,1.5f,1.5f);
	}

	void Move(int polygonIndex){
		polygons [polygonIndex].gameObject.transform.position+=new Vector3(-0.01f,0,0);
	}

	void FadeInOrOut(int polygonIndex){
		if (!polygons [polygonIndex].isKilled&&polygons [polygonIndex].alphaScale!=1.0f) {
			polygons [polygonIndex].gameObject.GetComponent<MeshRenderer> ().material.SetFloat ("_AlphaScale",Mathf.Clamp(polygons [polygonIndex].alphaScale+=0.05f,0.0f,1.0f));
		}

		if (polygons [polygonIndex].isKilled) {
			polygons [polygonIndex].gameObject.GetComponent<MeshRenderer> ().material.SetFloat ("_AlphaScale",Mathf.Clamp(polygons [polygonIndex].alphaScale-=0.2f,0.0f,1.0f));
			if (polygons [polygonIndex].alphaScale == 0) {
				//destroy??
			}
		}
	}
		
}
