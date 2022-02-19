using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {
	public static Vector3 MousePos;

	private int mouseLayerMask;

	void Start() {
		this.mouseLayerMask = LayerMask.GetMask("MouseCollision");
	}

    void Update() {
		Ray camToMouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(camToMouseRay, out hit, 100, mouseLayerMask);
		MousePos = hit.point;
    }
}
