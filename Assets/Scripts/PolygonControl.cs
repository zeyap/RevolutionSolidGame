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
				polygonBehaviour (i);
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

		StartCoroutine("RecoverPolygon");
	}

	IEnumerator RecoverPolygon(){
		while (true) {
			//count vacant section positions

			//recover & shift sectionPanel-polygon correspondence
			for (int i = 0; i < polygons.Count; i++) {
				if (polygons [i].isKilled == true) {
					polygons [i].gameObject.transform.position=RandomPos();

					//assign to one of vacant section panels
					for (int j = 3; j >= 0; j--) {
						if(Section.vacantPanels[j]==1){
							AxisDrawing.sections [j].polygonIndex = i;
						}
					}

					polygons [i].isKilled = false;
					AxisDrawing.sections [i].Show ();
				}
				yield return new WaitForSeconds (3);
			}
		}
	}

	Vector3 RandomPos(){
		Vector3 newPos;
		float rand = Random.value;
		if (rand <= 1.0f / 4) {
			newPos = new Vector3 (10.0f, -5.0f, 0);
		} else if (rand <= 2.0f / 4) {
			newPos = new Vector3 (-10.0f, -5.0f, 0);
		} else if (rand <= 3.0f / 4) {
			newPos = new Vector3 (10.0f, 5.0f, 0);
		} else {
			newPos = new Vector3 (-10.0f, 5.0f, 0);
		}
		return newPos;
	}
		

	void Rotate(int polygonIndex){
		if (polygons [polygonIndex].isKilled == false) {
			polygons [polygonIndex].gameObject.transform.Rotate (0.5f, 0.5f, 0.5f);
		}
	}

	void Move(int polygonIndex){
		if (polygons [polygonIndex].isKilled == false && polygons [polygonIndex].gameObject.transform.position != midPos) {
			polygons [polygonIndex].gameObject.transform.position=Vector3.MoveTowards(polygons [polygonIndex].gameObject.transform.position,midPos,Time.deltaTime*0.3f);
		}
	}

	void FadeInOrOut(int polygonIndex){
		if (!polygons [polygonIndex].isKilled) {
			polygons [polygonIndex].gameObject.GetComponent<MeshRenderer> ().material.SetFloat ("_AlphaScale",1.0f);
		}

		if (polygons [polygonIndex].isKilled) {
			polygons [polygonIndex].gameObject.GetComponent<MeshRenderer> ().material.SetFloat ("_AlphaScale",Mathf.Clamp(polygons [polygonIndex].alphaScale-=0.2f,0.0f,1.0f));
			if (polygons [polygonIndex].alphaScale <= 0.5f) {
				AxisDrawing.sections [polygonIndex].Hide ();
			}
		}
	}
		
}
