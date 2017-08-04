using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonControl : MonoBehaviour {

	delegate void PolygonBehaviour(int polygonIndex);
	PolygonBehaviour polygonBehaviour;
	public static List<Polygon> polygons;
	private Vector3 midPos;
	// Use this for initialization
	void Awake(){
		midPos = GameObject.Find ("middle").transform.position;
	}
	void Start () {
		polygons = new List<Polygon> ();//constructor

		StartCoroutine("AddPolygon");

		//polygonBehaviour += FadeInOrOut;
		polygonBehaviour += Rotate;
		polygonBehaviour += Move;

	}
	
	// Update is called once per frame
	void Update () {
		if (polygonBehaviour != null) {
			for (int i = 0; i < polygons.Count; i++) {
				if (polygons [i].isKilled == false) {
					polygonBehaviour (i);
				}
				FadeInOrOut (i);
			}
		}
	}

	IEnumerator AddPolygon(){
		polygons.Add(new Polygon(0,new Vector3(10.0f,-5.0f,0)));
		yield return new WaitForSeconds (2);
		polygons.Add(new Polygon(1,new Vector3(-10.0f,-5.0f,0)));
		yield return new WaitForSeconds (2);
		polygons.Add(new Polygon(2,new Vector3(10.0f,5.0f,0)));
		yield return new WaitForSeconds (2);
		polygons.Add(new Polygon(3,new Vector3(-10.0f,5.0f,0)));
		yield return new WaitForSeconds (2);

	}

	void Rotate(int polygonIndex){
		polygons [polygonIndex].gameObject.transform.Rotate (0.5f,0.5f,0.5f);
	}

	void Move(int polygonIndex){
		if (polygons [polygonIndex].gameObject.transform.position != midPos) {
			polygons [polygonIndex].gameObject.transform.position=Vector3.MoveTowards(polygons [polygonIndex].gameObject.transform.position,midPos,Time.deltaTime*0.3f);
		}
	}

	void FadeInOrOut(int polygonIndex){
		if (!polygons [polygonIndex].isKilled&&polygons [polygonIndex].alphaScale!=1.0f) {
			polygons [polygonIndex].gameObject.GetComponent<MeshRenderer> ().material.SetFloat ("_AlphaScale",Mathf.Clamp(polygons [polygonIndex].alphaScale+=0.05f,0.0f,1.0f));
		}

		if (polygons [polygonIndex].isKilled) {
			polygons [polygonIndex].gameObject.GetComponent<MeshRenderer> ().material.SetFloat ("_AlphaScale",Mathf.Clamp(polygons [polygonIndex].alphaScale-=0.2f,0.0f,1.0f));
			if (polygons [polygonIndex].alphaScale == 0) {
			}
		}
	}
		
}
