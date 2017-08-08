using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Section : MonoBehaviour {
	
	public Image image;
	public static float imgRes=550;
	//points on the correct axis
	//in pixel relative to original 550x550 image //increment from leftbottom

	/*
	public List<Vector3> axisVert;
	*/

	public int[] axisKernel;

	//candidate 

	public Section(int newIndex, int[]newAxisKernel){
		image = GameObject.Find ("section"+newIndex.ToString()).GetComponent<Image>();
		/*
		axisVert=new List<Vector3> ();
		AddVertex (axisVert,newPoint1);
		AddVertex (axisVert,newPoint2);//first two are right axis
		*/
		axisKernel= newAxisKernel;
	}

	/*
	void AddVertex(List<Vector3> vertexList, Vector3 newPoint){
		Vector3 temp=newPoint;
		temp.x -= imgRes/2;
		temp.y -= imgRes/2;
		vertexList.Add (temp);
	}
	*/

}
