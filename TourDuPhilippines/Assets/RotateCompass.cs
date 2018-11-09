using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCompass : MonoBehaviour {

	public GameObject cameraPos;

	void Update(){
		transform.localRotation =  Quaternion.Euler(0, 0, 360-cameraPos.transform.rotation.eulerAngles.y);
	}
}
