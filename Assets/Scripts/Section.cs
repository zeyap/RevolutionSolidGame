using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Section : MonoBehaviour {

	public Image image;
	public int panelIndex;
	public int polygonIndex;

	public static float imgRes=550;
	public static Sprite[] polygonSprites=new Sprite[4];
	public static int[] vacantPanels=new int[]{0,0,0,0};
	//points on the correct axis
	//in pixel relative to original 550x550 image //increment from leftbottom

	/*
	public List<Vector3> axisVert;
	*/

	//candidate 

	public Section(int newPanelIndex,int corresPolygonIndex){
		image = GameObject.Find ("section"+newPanelIndex.ToString()).GetComponent<Image>();
		panelIndex = newPanelIndex;
		polygonIndex = corresPolygonIndex;

		polygonSprites [polygonIndex] = image.sprite;
	}


	public void Hide(){
		image.gameObject.SetActive (false);
		vacantPanels [panelIndex] = 1;//1==isVacant
		
	}
	public void Show(){
		image.gameObject.SetActive (true);
		image.sprite=polygonSprites[polygonIndex];
		vacantPanels [panelIndex] = 0;
	}

}
