using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonControl : MonoBehaviour {

	delegate void ObjectBehaviour(int objectIndex);
	ObjectBehaviour objectBehaviour;

	public static List<Polygon> polygons;
	public static List<ActiveObject> activeObjects;
	private Vector3 midPos;
	public static int MaxPolygonNum=7;
	public static int MaxPanelNum=4;



	// Use this for initialization
	void Awake(){
		midPos = GameObject.Find ("middle").transform.position;
	}

	void Start () {

		InitPolygon ();
		ActivateObjects ();
		StartCoroutine("RecoverObjects");

		//polygonBehaviour += FadeInOrOut;
		objectBehaviour += Rotate;
		objectBehaviour += Move;

	}
	
	// Update is called once per frame
	void Update () {
		if (objectBehaviour != null) {
			for (int i = 0; i < activeObjects.Count; i++) {
				objectBehaviour (i);
				FadeInOrOut (i);
			}
		}
	}
		
	void InitPolygon(){
		polygons = new List<Polygon> ();//constructor
		for (int i = 0; i < MaxPolygonNum; i++) {
			if (i < MaxPanelNum) {
				polygons.Add (new Polygon (i));
			} else {
				polygons.Add (new Polygon (i));
			}
			//yield return new WaitForSeconds (2);
		}
	}

	void ActivateObjects(){
		activeObjects = new List<ActiveObject> ();//constructor
		for (int i = 0; i < MaxPanelNum; i++) {
			activeObjects.Add (new ActiveObject (i,i));
			//yield return new WaitForSeconds (2);
		}
	}

	IEnumerator RecoverObjects(){
		while (true) {
			//recover & shift sectionPanel-polygon correspondence
			for(int i=0;i<MaxPanelNum;i++){
				if (activeObjects [i].isKilled == true) {
					//replace with one of the polygons(that is currently not on screen
					int k;
					while(true){
						k = Mathf.FloorToInt (Random.value * MaxPolygonNum);
						int j;
						for (j = 0; j < MaxPanelNum; j++) {
							if (activeObjects [j].polygonIndex == k)
								break;//for
						}
						if (j == MaxPanelNum) {
							break;//while
						}
					}
					activeObjects [i].polygonIndex = k;
					activeObjects [i].Refresh ();
					/*
					Debug.Log (i);
					Debug.Log ("+1");*/
				}
			}
			yield return new WaitForSeconds (2);

		}
	}
		

	void Rotate(int objIndex){
		if (activeObjects [objIndex].isKilled == false) {
			activeObjects [objIndex].gameObject.transform.Rotate (0.5f, 0.5f, 0.5f);
		}
	}

	void Move(int objIndex){
		if (activeObjects [objIndex].isKilled == false && activeObjects [objIndex].gameObject.transform.position != midPos) {
			activeObjects [objIndex].gameObject.transform.position=Vector3.MoveTowards(activeObjects [objIndex].gameObject.transform.position,midPos,Time.deltaTime*0.3f);
		}
	}

	void FadeInOrOut(int objIndex){
		if (!activeObjects [objIndex].isKilled) {
			activeObjects [objIndex].gameObject.GetComponent<MeshRenderer> ().material.SetFloat ("_AlphaScale",1.0f);
		}

		if (activeObjects [objIndex].isKilled) {
			activeObjects [objIndex].gameObject.GetComponent<MeshRenderer> ().material.SetFloat ("_AlphaScale",Mathf.Clamp(activeObjects [objIndex].alphaScale-=0.2f,0.0f,1.0f));
			activeObjects [objIndex].gameObject.transform.position = new Vector3 (-100,-100,0);
		}
	}
		
}
