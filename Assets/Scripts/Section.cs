using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Section : MonoBehaviour {
	
	public GameObject image;
	public Vector3 point1;
	public Vector3 point2;

	public Section(int newIndex, Vector3 newPoint1, Vector3 newPoint2){
		image = GameObject.Find ("section0");
		point1 = newPoint1;
		point2 = newPoint2;
	}
}
