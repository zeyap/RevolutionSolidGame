using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Section : MonoBehaviour {

	public Sprite imgSprite;
	public int panelIndex;
	public int polygonIndex;
	public static int[][] axisKernels;
	public static int[][] candKernels;
	public static float imgRes=550;


	//points on the correct axis
	//in pixel relative to original 550x550 image //increment from leftbottom

	/*
	public List<Vector3> axisVert;
	*/

	//candidate 

	public Section(int newPanelIndex,int corresPolygonIndex){
		imgSprite = GameObject.Find ("section"+newPanelIndex.ToString()).GetComponent<Image>().sprite;
		panelIndex = newPanelIndex;
		polygonIndex = corresPolygonIndex;
	}



}
