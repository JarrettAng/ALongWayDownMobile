using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class WorldSpaceCameraAssigner : MonoBehaviour {

	private void Start() {
		GetComponent<Canvas>().worldCamera = Camera.main;
	}
}
