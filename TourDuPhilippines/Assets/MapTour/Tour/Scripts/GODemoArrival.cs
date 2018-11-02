using UnityEngine;
using System.Collections;

namespace DigiDirections {

	public class GODemoArrival : MonoBehaviour {

		void OnCollisionEnter(Collision collision) {

			GODirections goDirections = transform.parent.parent.GetComponent<GODirections> ();
			goDirections.ClearRoute ();


		}
	}

}
