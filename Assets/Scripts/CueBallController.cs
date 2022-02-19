using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBallController : MonoBehaviour
{
	private bool isDragging;
	private LineRenderer lineRenderer;
	private Rigidbody rb;
	
	public float HitSensitivity = 1;

	private void Start() {
		this.isDragging = false;
		this.lineRenderer = GetComponent<LineRenderer>();
		this.rb = GetComponentInChildren<Rigidbody>();
	}

	private void Update() {
		Vector3 cueBallPos = transform.Find("Ball").position;

		// did player click the cue ball?
		if(Input.GetMouseButtonDown(0)) {
			int cueBallMask = LayerMask.GetMask("CueBall");
			Ray camToMouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(camToMouseRay, out hit, 100, cueBallMask)) {
				this.isDragging = true;
			}
		}

		if(Input.GetMouseButtonUp(0) && isDragging) {
			this.isDragging = false;
			
			// shoot the ball!
			Vector3 raisedMousePos = MouseController.MousePos + new Vector3(0, cueBallPos.y, 0);
			rb.velocity = HitSensitivity * (cueBallPos - raisedMousePos);
		}

		// while dragging, we want to draw a line behind the cue ball
		if(isDragging) {
			lineRenderer.positionCount = 2;
			var points = new Vector3[2];
			points[0] = MouseController.MousePos;
			points[1] = cueBallPos;
			// the Y of the mouse pos point should be on the same level as the ball...
			points[0].y = cueBallPos.y;
			lineRenderer.SetPositions(points);
		} else {
			lineRenderer.positionCount = 0;	// don't render a line anymore
		}

	}

}
