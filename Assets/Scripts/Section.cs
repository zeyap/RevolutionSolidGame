using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Section : MonoBehaviour {
	
	public Image image;
	const float imgRes=275;
	//points on the correct axis
	//in pixel relative to original 550x550 image //increment from leftbottom

	public List<Vector3> axisCandVert;

	//candidate 

	public Section(int newIndex, Vector3 newPoint1, Vector3 newPoint2){
		image = GameObject.Find ("section"+newIndex.ToString()).GetComponent<Image>();
		axisCandVert=new List<Vector3> ();
		AddVertex (axisCandVert,newPoint1);
		AddVertex (axisCandVert,newPoint2);//first two are right axis

		//later read from text files
		AddVertex (axisCandVert,new Vector3(9,0,0));//left edge
		AddVertex (axisCandVert,new Vector3(9,1,0));

		AddVertex (axisCandVert,new Vector3(488,0,0));//right edge
		AddVertex (axisCandVert,new Vector3(488,1,0));

		AddVertex (axisCandVert,new Vector3(0,10,0));//bottom edge
		AddVertex (axisCandVert,new Vector3(1,10,0));

		AddVertex (axisCandVert,new Vector3(0,540,0));//top edge
		AddVertex (axisCandVert,new Vector3(1,540,0));

		AddVertex (axisCandVert,new Vector3(9,10,0));//diagnonal/
		AddVertex (axisCandVert,new Vector3(488,540,0));

		AddVertex (axisCandVert,new Vector3(488,10,0));//diagnonal\
		AddVertex (axisCandVert,new Vector3(9,540,0));
	}

	void AddVertex(List<Vector3> vertexList, Vector3 newPoint){
		Vector3 temp=newPoint;
		temp.x -= imgRes;
		temp.y -= imgRes;
		vertexList.Add (temp);
	}
}
