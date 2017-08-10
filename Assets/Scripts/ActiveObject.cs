using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveObject : MonoBehaviour {

	public bool isKilled;
	public int panelIndex;
	public int polygonIndex;
	public GameObject gameObject;
	public Image image;

	public Vector3 initialPos;
	public float alphaScale;

	public ActiveObject(int newPanelIndex,int newPolygonIndex){
		
		isKilled = false;
		panelIndex = newPanelIndex;
		polygonIndex = newPolygonIndex;

		gameObject = PolygonControl.polygons [polygonIndex].gameObject;
		image =  GameObject.Find ("section"+newPanelIndex.ToString()).GetComponent<Image>();

		initialPos = GenRandomPos ();
		alphaScale = 1.0f;
	}
	public void Refresh(){
		isKilled = false;

		gameObject = PolygonControl.polygons [polygonIndex].gameObject;
		initialPos = GenRandomPos ();
		gameObject.transform.position = initialPos;

		image.sprite = AxisDrawing.sections [polygonIndex].imgSprite;

		alphaScale = 1.0f;
	}

	Vector3 GenRandomPos(){
		Vector3 newPos;
		int rand = Mathf.FloorToInt(Random.value*PolygonControl.MaxPanelNum);
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

}
