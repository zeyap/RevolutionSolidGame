using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveObject : MonoBehaviour {
	//constant
	public int panelIndex;
	public Image image;

	//alterable
	public bool isKilled;
	public int polygonIndex;
	public GameObject gameObject;
	public float alphaScale;

	public Vector3 initialPos;
	public float speed;

	public ActiveObject(int newPanelIndex,int newPolygonIndex){
		
		panelIndex = newPanelIndex;
		image =  GameObject.Find ("section"+panelIndex.ToString()).GetComponent<Image>();

		polygonIndex = newPolygonIndex;
		isKilled = false;
		gameObject = PolygonControl.polygons [polygonIndex].gameObject;
		initialPos = GenRandomPos ();
		speed = GenRandomSpeed ();
		gameObject.transform.position = initialPos;

		alphaScale = 1.0f;
	}
	public void Refresh(){
		image.gameObject.SetActive (true);
		image.sprite = AxisDrawing.sections [polygonIndex].imgSprite;

		isKilled = false;
		gameObject = PolygonControl.polygons [polygonIndex].gameObject;

		initialPos = GenRandomPos ();
		speed = GenRandomSpeed ();

		gameObject.transform.position = initialPos;
		alphaScale = 1.0f;
	}

	Vector3 GenRandomPos(){
		Vector3 newPos = new Vector3 (-10.0f, 0.0f, 0);
		int rand = Mathf.FloorToInt(Random.value*8);
		switch (rand) {
		case 0:
			newPos = new Vector3 (10.0f, -5.0f, 0);
			break;
		case 1:
			newPos = new Vector3 (-10.0f, -5.0f, 0);
			break;
		case 2:
			newPos = new Vector3 (10.0f, 5.0f, 0);
			break;
		case 3:
			newPos = new Vector3 (-10.0f, 5.0f, 0);
			break;
		case 4:
			newPos = new Vector3 (10.0f, 2.5f, 0);
			break;
		case 5:
			newPos = new Vector3 (-10.0f, 2.5f, 0);
			break;
		case 6:
			newPos = new Vector3 (10.0f, -2.5f, 0);
			break;
		case 7:
			newPos = new Vector3 (-10.0f, -2.5f, 0);
			break;
		}

		return newPos;
	}

	float GenRandomSpeed(){
		float spd;
		spd=Mathf.Clamp(Random.value*1.0f,0.1f,1.0f);
		return spd;
	}

}
