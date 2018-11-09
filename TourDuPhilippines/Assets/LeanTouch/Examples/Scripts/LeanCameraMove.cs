using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to track & pedestral this GameObject (e.g. Camera) based on finger drags
	public class LeanCameraMove : MonoBehaviour
	{
		[Tooltip("The camera the movement will be done relative to")]
		public Camera Camera;

		[Tooltip("Ignore fingers with StartedOverGui?")]
		public bool IgnoreGuiFingers = true;

		[Tooltip("Ignore fingers if the finger count doesn't match? (0 = any)")]
		public int RequiredFingerCount;

		[Tooltip("The distance from the camera the world drag delta will be calculated from (this only matters for perspective cameras)")]
		public float Distance = 1.0f;
		Quaternion target;
		[Tooltip("The sensitivity of the movement, use -1 to invert")]
		public float Sensitivity = 1.0f;
		private Vector3 rotationValue; 
		private float turnValue = 0.0f; 
		private float turnVal { 
			get { return turnValue; } 
			set { turnValue = value; 
				//if (turnValue >= 360f) turnValue = 0.0f; 
			} 
		}


		protected virtual void LateUpdate()
		{
			// Make sure the camera exists
			if (LeanTouch.GetCamera(ref Camera, gameObject) == true)
			{
				// Get the fingers we want to use
				var fingers = LeanTouch.GetFingers(IgnoreGuiFingers, RequiredFingerCount);

				// Get the world delta of all the fingers
				var worldDelta = LeanGesture.GetWorldDelta(fingers, Distance, Camera);

				turnVal += Time.deltaTime;

				rotationValue = new Vector3 (Camera.main.transform.rotation.eulerAngles.x+worldDelta.y, 
					Camera.main.transform.rotation.eulerAngles.y + worldDelta.x, 0);
				transform.rotation = Quaternion.Euler (rotationValue);


			}
		}
	}
}