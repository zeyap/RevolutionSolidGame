using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxisDrawing: MonoBehaviour {

	private LineRenderer line;
	private bool isLineInstantiated;
	private int vertexCount;
	public GameObject linePrefab;

	protected List<Vector3> linePath;

	private float alphaScale;

	private Vector3 mousePos;
	protected float wx, wy;
	protected float sectionScale=0.2f;//depend on the gameObject empty

	protected List<Section> sections;

	Text text;

	/*
	public delegate int Grade(List<Vector3> path);
	public static Grade grade;
	*/

	void Awake(){
		text = GameObject.Find ("Text").GetComponent<Text> ();
	}
	// Use this for initialization
	void Start () {
		wx = Camera.main.pixelRect.center.x;
		wy = Camera.main.pixelRect.center.y;

		isLineInstantiated = false;
		InitSections ();
		linePath = new List<Vector3> ();

		StartCoroutine("RecordLinePath");
		/*
		grade += LeastSquareMethod;*/
	}
	
	// Update is called once per frame
	void Update () {
		if (wx != Camera.main.pixelRect.center.x || wy != Camera.main.pixelRect.center.y) {
			wx = Camera.main.pixelRect.center.x;
			wy = Camera.main.pixelRect.center.y;
		}
		mousePos = Input.mousePosition;
		mousePos.x =mousePos.x- wx;
		mousePos.y =mousePos.y- wy;

		if (Input.GetMouseButtonDown (0)) {
			//destroy existing one and instantiate new
			if (isLineInstantiated) {
				//DisplayScore (grade(linePath));
				DisplayScore (Grading(linePath));
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
		line.SetPosition (vertexCount - 1, Camera.main.ScreenToWorldPoint (new Vector3 (mousePos.x+wx, mousePos.y+wy, 0)));
	}

	void AxisFadeOut(){
		line.material.SetFloat ("_AlphaScale",Mathf.Clamp(alphaScale-=0.05f,0.0f,1.0f));
	}

	IEnumerator RecordLinePath(){
		while (true) {
			if (Mathf.Abs (mousePos.x) < 200 && Mathf.Abs (mousePos.y) < 200) {
				linePath.Add (mousePos);
			}
			yield return new WaitForSeconds (0.01f);
		}
	}

	void DisplayScore(int bestMatchCand){
		switch(bestMatchCand) {
		case 0:
			text.text = "fantastic!";
			break;
		case 2:
			text.text = "left edge";
			break;
		case 4:
			text.text = "right edge";
			break;
		case 6:
			text.text = "bottom edge";
			break;
		case 8:
			text.text = "top edge";
			break;
		case 10:
			text.text = "diagonal /";
			break;
		case 12:
			text.text = "diagonal \\";
			break;
		default:
			text.text = "try again!";
			break;
		}
	}

	void InitSections(){
		sections = new List<Section> ();//constructor
		sections.Add(new Section(0,new Vector3(120,0,0),new Vector3(120,1,0)));//bottomleft be origin
		sections.Add(new Section(1,new Vector3(400,0,0),new Vector3(400,1,0)));
		sections.Add(new Section(2,new Vector3(155,444,0),new Vector3(497,180,0)));
		sections.Add(new Section(3,new Vector3(115,0,0),new Vector3(115,1,0)));
	}


	//Grading methods

	public int BestMatchCandidate (List<Vector3> path,int panelIndex){
		int bestMatchCandidateNo=-1;
		float tempSquare;
		float minSquare=9999;
		for (int i = 0; i <= sections [panelIndex].axisCandVert.Count - 1; i += 2) {
			tempSquare = LeastSquare (path, panelIndex, i);
			//Debug.Log (i);
			//Debug.Log (tempSquare);
			if (tempSquare < minSquare) {
				minSquare = tempSquare;
				bestMatchCandidateNo = i;
			}
		}
		return bestMatchCandidateNo;
	}

	float LeastSquare(List<Vector3> path,int panelIndex,int candNo){
		float avgSqrsSum=0;
		float x1, y1, x2, y2;
		x1 = sections [panelIndex].axisCandVert[candNo].x*sectionScale + sections[panelIndex].image.transform.position.x-wx;
		y1 = sections [panelIndex].axisCandVert[candNo].y*sectionScale + sections[panelIndex].image.transform.position.y-wy;
		x2 = sections [panelIndex].axisCandVert[candNo+1].x*sectionScale + sections[panelIndex].image.transform.position.x-wx;
		y2 = sections [panelIndex].axisCandVert[candNo+1].y*sectionScale + sections[panelIndex].image.transform.position.y-wy;
		for (int i = 0; i < path.Count; i++) {
			avgSqrsSum += Mathf.Pow ((y2 - y1) * path [i].x + (x1 - x2) * path [i].y + (x2 * y1 - x1 * y2), 2) / ((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));
		}
		avgSqrsSum /= path.Count;
		return avgSqrsSum;
	}

	const float thres=0.0f;
	public int Grading(List<Vector3> path){
		//calculate start/end vertices & slope

		Vector3 start=path[0];
		Vector3 end=path[path.Count-1];
		Debug.Log (Vector3.Distance (start, end));
		if (Vector3.Distance (start, end) < 50) {
			return -1;
		}

		//float slope=(end.y-start.y)/(end.x-start.x);

		int panelIndex=0;//sectionIndex
		//panel position 0-UR,1-UL,2-BL,3-BR
		if (end.x > -thres && end.y > -thres) {
			panelIndex = 0;
		} else if (end.x > -thres && end.y < thres) {
			panelIndex = 3;
		}else if (end.x < thres && end.y > -thres) {
			panelIndex = 1;
		}else if (end.x < thres && end.y < thres) {
			panelIndex = 2;
		}
		//Debug.Log (panelIndex);
		if (PolygonControl.polygons [panelIndex].isKilled == false) {
			if (BestMatchCandidate (path, panelIndex) == 0) {
				PolygonControl.polygons [panelIndex].isKilled = true;
			} 
		}
		return BestMatchCandidate (path, panelIndex);
	}
}
