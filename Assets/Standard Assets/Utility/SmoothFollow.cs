using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{

		float mainSpeed  = 100f; //regular speed
		float shiftAdd = 250f; //multiplied by how long shift is held.  Basically running
		float maxShift = 1000f; //Maximum speed when holdin gshift
		float camSens  = 0.25f; //How sensitive it with mouse
		private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
		private float totalRun = 1.0f;

		private bool cameraFree = false;
		private bool Active = false;

		// The target we are following
		[SerializeField]
		private Transform target;
		// The distance in the x-z plane to the target
		[SerializeField]
		private float distance = 10.0f;
		// the height we want the camera to be above the target
		[SerializeField]
		private float height = 5.0f;

		[SerializeField]
		private float rotationDamping;
		[SerializeField]
		private float heightDamping;

		// Use this for initialization
		void Start() { }

		// Update is called once per frame
		void Update()
		{

			if (Input.GetKeyDown(KeyCode.F1) && !Active)
			{
				Active = true;
				cameraFree = !cameraFree;
			}

			if (Active && Input.GetKeyUp(KeyCode.F1))
				Active = false;

			if (cameraFree == false)
			{

				// Early out if we don't have a target
				if (!target)
					return;

				// Calculate the current rotation angles
				var wantedRotationAngle = target.eulerAngles.y;
				var wantedHeight = target.position.y + height;

				var currentRotationAngle = transform.eulerAngles.y;
				var currentHeight = transform.position.y;

				// Damp the rotation around the y-axis
				currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

				// Damp the height
				currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

				// Convert the angle into a rotation
				var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

				// Set the position of the camera on the x-z plane to:
				// distance meters behind the target
				transform.position = target.position;
				transform.position -= currentRotation * Vector3.forward * distance;

				// Set the height of the camera
				transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

				// Always look at the target
				transform.LookAt(target);
			}
			else
			{
				Cursor.lockState = CursorLockMode.Confined;
				Cursor.visible = false;
				lastMouse = Input.mousePosition - lastMouse;
				lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
				lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
				transform.eulerAngles = lastMouse;
				lastMouse = Input.mousePosition;
				//Mouse  camera angle done.  

				//Keyboard commands
				float f  = 0.0f;
				var p = GetBaseInput();
				if (Input.GetKey(KeyCode.LeftShift))
				{
					totalRun += Time.deltaTime;
					p = p * totalRun * shiftAdd;
					p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
					p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
					p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
				}
				else
				{
					totalRun = Mathf.Clamp(totalRun * 0.5f, 1, 1000);
					p = p * mainSpeed;
				}

				p = p * Time.deltaTime;
				if (Input.GetKey(KeyCode.Space))
				{ //If player wants to move on X and Z axis only
					f = transform.position.y;
					transform.Translate(p);
					transform.position = new Vector3(transform.position.x,f,transform.position.z);
				}
				else
				{
					transform.Translate(p);
				}
			}
		}

		Vector3 GetBaseInput(){
			Vector3 p_Velocity = Vector3.zero;
			if (Input.GetKey (KeyCode.W)){
				p_Velocity += new Vector3(0, 0 , 0.5f);
			}
			if (Input.GetKey (KeyCode.S)){
				p_Velocity += new Vector3(0, 0 , -0.5f);
			}
			if (Input.GetKey (KeyCode.A)){
				p_Velocity += new Vector3(-0.5f, 0 , 0);
			}
			if (Input.GetKey (KeyCode.D)){
				p_Velocity += new Vector3(0.5f, 0 , 0);
			}
			return p_Velocity;
		}
	}
}