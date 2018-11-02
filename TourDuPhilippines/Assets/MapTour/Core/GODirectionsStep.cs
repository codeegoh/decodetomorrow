using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using DigiMap;
using DigiShared;

namespace DigiDirections
{

    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class GODirectionsStep : MonoBehaviour
    {

		public Int64 distance;
		public Int64 duration;
		public string htmlInstructions;
		public GODirections.GOTravelModes travelMode;

		public GOTransitDetails transitDetails;

		[HideInInspector]
        public List<Vector3> _verts;

		public GODirectionsRoute route;

		#region INIT

		public void InitStep (IDictionary stepDict, GODirectionsRendering [] renderingOptions, GODirectionsRoute route_, bool useElevation) {
		
			route = route_;

			distance = (Int64)((IDictionary)stepDict["distance"])["value"];
			duration = (Int64)((IDictionary)stepDict["duration"])["value"];

			htmlInstructions = (string)stepDict["html_instructions"];
			route.htmlDirections.Add (htmlInstructions);

			gameObject.name = ((string)stepDict["travel_mode"]).ToLower();
			travelMode = GODirectionsRendering.StringToTravelMode(gameObject.name);

			if (!stepDict.Contains ("steps") || !route.directions.useSubsteps) {

				if (stepDict.Contains ("transit_details")) {
				
					transitDetails = new GOTransitDetails ();
					transitDetails.InitDetails (stepDict,this, useElevation);
				}

				//Get the rendering options
				GODirectionsRendering rendering = GODirections.GetRenderingOptionsForTravelMode (renderingOptions, travelMode);

				string encodedPolyline = (string)((IDictionary)stepDict ["polyline"]) ["points"];
				List<Coordinates> directionCoordinates = GOPolylineConverter.Decode (encodedPolyline).ToList ();
				var coords = new List<Vector3> ();
				foreach (Coordinates coord in directionCoordinates) {
					
					Vector3 v = coord.convertCoordinateToVector ((float)coord.altitude);
					#if GOLINK
					v.y = GoTerrain.GOTerrain.RaycastAltitudeForVector(v);
					#endif
					coords.Add(v);

				}

				Initialize (gameObject, coords, rendering.sidesMaterial, rendering.material, rendering.width, rendering.height,rendering.distanceFromFloor,route.directions.useColliders);

				if (stepDict.Contains ("steps")) {
					IList steps = (IList)stepDict["steps"];
					foreach (IDictionary stepDictInner in steps) {
						string htmlInstructionsInner = (string)stepDictInner["html_instructions"];
						route.htmlDirections.Add (htmlInstructionsInner);
					}
				}

			} else {

				IList steps = (IList)stepDict["steps"];
				foreach (IDictionary stepDictInner in steps) {
					GameObject s = new GameObject ();
					GODirectionsStep step = s.AddComponent<GODirectionsStep>();
					s.transform.parent = gameObject.transform.parent;
					step.InitStep (stepDictInner, renderingOptions,route, useElevation);
				}

				GameObject.Destroy (gameObject);
			}
		}

		#endregion

		#region BUILDER

		public void Initialize(GameObject go, List<Vector3> verts, Material sides, Material top, float width, float height,float y, bool colliders)
        {
            _verts = verts;

			if (sides == null)
				sides = top;

			BuildLineMesh (go, verts, sides, width, height, 0f, colliders);

			GameObject roof = new GameObject ();
			roof.name = "top";
			roof.transform.parent = go.transform;
			BuildLineMesh (roof, verts, top, width, 0f, height+0.1f,colliders);

			go.transform.position = go.transform.position + new Vector3 (0, y, 0);

        }
			
		public GameObject BuildLineMesh (GameObject go, List<Vector3> verts, Material material, float width, float height, float yPosition, bool colliders) {

			GOLinearMesh line = new GOLinearMesh ();

			#if GOLINK
			Vector3[] vertices = _verts.ToArray();
			#else
			Vector3[] vertices = _verts.Select(x => new Vector3(x.x, yPosition, x.z)).ToArray();
			#endif

			line.verts = vertices;
			line.width = width;

			line.load (go);

			if (height > 0) {
				go.GetComponent<MeshFilter>().mesh = SimpleExtruder.Extrude (go.GetComponent<MeshFilter>().mesh, go,height);
			}

			if (colliders) {
				go.AddComponent<MeshCollider> ().sharedMesh = go.GetComponent<MeshFilter> ().mesh;
			}

			Vector3 position = transform.position;
			position.y = 0;
			transform.position = position;

			go.GetComponent<Renderer>().material = material;

			return go;
		}

		#endregion

    }

}
