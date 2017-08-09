using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonControl : MonoBehaviour {

	delegate void PolygonBehaviour(int polygonIndex);
	PolygonBehaviour polygonBehaviour;
	public static List<Polygon> polygons;
	private Vector3 midPos;
	public static int MaxPolygonNum=7;
	public static int MaxPanelNum=4;
	// Use this for initialization
	void Awake(){
		midPos = GameObject.Find ("middle").transform.position;
	}
	void Start () {

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
		polygons = new List<Polygon> ();//constructor
		for (int i = 0; i < MaxPolygonNum; i++) {
			if (i < MaxPanelNum) {
				polygons.Add (new Polygon (i, i, RandomPos ()));
			} else {
				polygons.Add (new Polygon (i, -1, RandomPos ()));
			}
			//yield return new WaitForSeconds (2);
			yield return null;
		}

		StartCoroutine("RecoverPolygon");
	}

	IEnumerator RecoverPolygon(){
		while (true) {
			//count vacant section positions
			Debug.Log(Section.vacantPanels[0]);
			Debug.Log(Section.vacantPanels[1]);
			Debug.Log(Section.vacantPanels[2]);
			Debug.Log(Section.vacantPanels[3]);
			//recover & shift sectionPanel-polygon correspondence
			for(int i=0;i<MaxPanelNum;i++){
				if (Section.vacantPanels [i] == 1) {
					//replace with a currently inexistant one
					int k = Mathf.FloorToInt (Random.value * MaxPolygonNum);
					for(int j = 0; j < polygons.Count; j++){
						if (k >= polygons.Count) {
							k =k % polygons.Count;
						}
						if (polygons [k].isKilled == true) {
							AxisDrawing.sections [i].polygonIndex = k;
							polygons [k].Recover(i);
							AxisDrawing.sections [i].Show ();

							Debug.Log (i);
							Debug.Log ("+1");
							break;//(break if found)
						} else {
							k++;
						}
					}
				}
			}
			yield return new WaitForSeconds (2);

			/*
			for (int i = 0; i < polygons.Count; i++) {
				if (polygons [i].isKilled == true) {
					polygons [i].gameObject.transform.position=RandomPos();

					//assign to one of vacant section panels
					int k = Mathf.FloorToInt (Random.value * MaxPanelNum);
					for (int j = 0; j <MaxPanelNum; j++) {
						if (k < MaxPanelNum) {
							if (Section.vacantPanels [k] == 1) {
								AxisDrawing.sections [k].polygonIndex = i;
							}
						} else {
							k = k % MaxPanelNum;
						}
						k++;
					}

					polygons [i].isKilled = false;
					AxisDrawing.sections [i].Show ();
				}
				yield return new WaitForSeconds (3);
			}
			*/
		}
	}

	Vector3 RandomPos(){
		Vector3 newPos;
		int rand = Mathf.FloorToInt(Random.value*MaxPanelNum);
		if (rand ==0) {
			newPos = new Vector3 (10.0f, -5.0f, 0);
		} else if (rand ==1) {
			newPos = new Vector3 (-10.0f, -5.0f, 0);
		} else if (rand ==2) {
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
			if (polygons [polygonIndex].panelIndex != -1) {
				AxisDrawing.sections [polygons [polygonIndex].panelIndex].Hide ();
				Debug.Log ("hide");
				Debug.Log (polygons [polygonIndex].panelIndex);
			}
		}
	}
		
}
