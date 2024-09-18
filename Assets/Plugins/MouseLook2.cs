using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character alreadly turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook2 : MonoBehaviour {

	public bool onLMB = false;	//must press left mouse button?
	public Transform myObj;		//must select object? (if no, leave empty)
	private bool canRotate = false;

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;

	void Update () {
		if(onLMB == true) {
			if(myObj != null) {
				if(Input.GetMouseButtonDown(0)) {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;
					if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
						if(hit.transform == myObj) {
							canRotate = true;
						}
					}
				}
			}
			else {
				if(Input.GetMouseButton(0)) {
					canRotate = true;
				}
			}
			if(canRotate == true) {
				if (axes == RotationAxes.MouseXAndY) {
					float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
					
					rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
					rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
					
					transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
				}
				else if (axes == RotationAxes.MouseX) {
					transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
				}
				else {
					rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
					rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
					
					transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
				}
			}
			if(Input.GetMouseButtonUp(0)) {
				canRotate = false;
			}
		}
		else if(onLMB == false) {
			if (axes == RotationAxes.MouseXAndY) {
				float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
				
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
			}
			else if (axes == RotationAxes.MouseX) {
				transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
			}
			else {
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
			}
		}
	}
	
	void Start () {
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}
}