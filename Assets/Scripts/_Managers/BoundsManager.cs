using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsManager : Singleton<BoundsManager>
{
	public float LeftExtent = 0;
	public float RightExtent = 0;

	private void Awake() {
		SetBounds();
	}

    private void SetBounds() {
		float horizontalExtent = Camera.main.orthographicSize * Screen.width / Screen.height;

		LeftExtent = Camera.main.transform.position.x - horizontalExtent;
		RightExtent = Camera.main.transform.position.x + horizontalExtent;
	}
}
