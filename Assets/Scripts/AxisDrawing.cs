using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxisDrawing: MonoBehaviour {

	private LineRenderer line;
	private bool isLineInstantiated;
	private int vertexCount;
	public GameObject linePrefab;

	List<Vector3> linePath;

	private float alphaScale;

	private Vector3 mousePos;
	private float wx, wy;

	List<Section> sections;

	Text text;

	void Awake(){
		text = GameObject.Find ("Text").GetComponent<Text> ();
	}
	// Use this for initialization
	void Start () {
		wx = Camera.main.pixelRect.center.x;
		wy = Camera.main.pixelRect.center.y;
		isLineInstantiated = false;

		sections = new List<Section> ();//constructor
		sections.Add(new Section(0,new Vector3(328,1,0),new Vector3(328,-1,0)));

		linePath = new List<Vector3> ();

		StartCoroutine("RecordLinePath");
	}
	
	// Update is called once per frame
	void Update () {
		mousePos = Input.mousePosition;
		if (Input.GetMouseButtonDown (0)) {
			//destroy existing one and instantiate new
			if (isLineInstantiated) {
				DisplayScore (Grading (linePath,0));
				DestroyAxis ();
				linePath.Clear ();
			}
			if (isLineInstantiated==false) {
				InitAxis ();
			}
		}

		if (Input.GetMouseButton (0)) {
			DrawAxis ();
			//trembling prevention(?
		}
		if (isLineInstantiated) {
			AxisFadeOut ();
		}
	}

	void DestroyAxis(){
		GameObject.Destroy (line.gameObject);
		isLineInstantiated = false;
	}
	void InitAxis(){ 
		line = GameObject.Instantiate (linePrefab, linePrefab.transform.position, transform.rotation).GetComponent<LineRenderer> ();
		line.SetWidth (0.05f, 0.15f);
		vertexCount = 0;
		alphaScale = 1.0f;
		isLineInstantiated = true;
	}

	void DrawAxis(){
		//if (mousePos.x-wx && mousePos.y-wy) 
		vertexCount++;
		line.SetVertexCount (vertexCount);
		line.SetPosition (vertexCount - 1, Camera.main.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, 0)));
		//Debug.Log (mousePos.x);
		//Debug.Log (mousePos.y);
	}

	void AxisFadeOut(){
		line.material.SetFloat ("_AlphaScale",Mathf.Clamp(alphaScale-=0.05f,0.0f,1.0f));
	}

	IEnumerator RecordLinePath(){
		while (true) {
			if (Mathf.Abs (mousePos.x - wx) < 100 && Mathf.Abs (mousePos.y - wy) < 100) {
				linePath.Add (mousePos);
			}
			yield return new WaitForSeconds (0.1f);
		}
	}

	float Grading (List<Vector3> path,int sectionIndex){
		float avgSqrsSum=0;
		float x1, y1, x2, y2;
		x1 = sections [sectionIndex].point1.x;
		y1 = sections [sectionIndex].point1.y;
		x2 = sections [sectionIndex].point2.x;
		y2 = sections [sectionIndex].point2.y;
		for(int i=0;i<path.Count;i++){
			avgSqrsSum += Mathf.Pow((y2-y1)*path[i].x+(x1-x2)*path[i].y+(x2*y1-x1*y2),2)/((y2-y1)*(y2-y1)+(x2-x1)*(x2-x1));
			//Debug.Log (Mathf.Pow((y2-y1)*path[i].x+(x1-x2)*path[i].y+(x2*y1-x1*y2),2)/((y2-y1)*(y2-y1)+(x2-x1)*(x2-x1)));
		}
		avgSqrsSum /= path.Count;
		//Debug.Log (avgSqrsSum);//100 200
		if (avgSqrsSum <= 150) {
			PolygonControl.polygons [sectionIndex].isKilled = true;
		}
		return avgSqrsSum;
	}

	void DisplayScore(float score){
		if (score <= 100) {
			text.text = "fantastic!";
		} else if (score <= 150) {
			text.text = "not bad";
		} else{
			text.text="let's try again..";
		}
	}
}
