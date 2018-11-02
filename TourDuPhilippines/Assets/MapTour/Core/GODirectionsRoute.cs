using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using DigiShared;

namespace DigiDirections
{

    public class GODirectionsRoute : MonoBehaviour
    {

		public string startAddress;
		public string endAddress;
		public Int64 distance;
		public Int64 duration;
		public List<string> htmlDirections;

		[HideInInspector]
		public GODirections directions;

		public void InitRoute (IList overviewCoordinates, IDictionary leg ,GODirectionsRendering [] renderingOptions, GODirections.GOTravelModes travelMode, bool useElevation)
		{

			var l = new List<Vector3>();
			foreach (Coordinates coord in overviewCoordinates)
			{

				Vector3 v = coord.convertCoordinateToVector ((float)coord.altitude);
				#if GOLINK
				v.y = GoTerrain.GOTerrain.RaycastAltitudeForVector(v);
				#endif
				l.Add(v);
			}
				
//			try
//			{

				distance = (Int64)((IDictionary)leg["distance"])["value"];
				duration = (Int64)((IDictionary)leg["duration"])["value"];
				startAddress = (string)leg["start_address"];
				endAddress = (string)leg["end_address"];

				htmlDirections = new List<string>();

				if (leg.Contains("steps")) {
					IList steps = (IList)leg["steps"];
					foreach (IDictionary stepDict in steps) {

						GameObject s = new GameObject ();
						GODirectionsStep step = s.AddComponent<GODirectionsStep>();
						s.transform.parent = gameObject.transform;
					step.InitStep (stepDict, renderingOptions,this, useElevation);
					}
				} else {
				
					GameObject s = new GameObject ();
					GODirectionsStep step = s.AddComponent<GODirectionsStep>();
					GODirectionsRendering rendering = GODirections.GetRenderingOptionsForTravelMode (renderingOptions,travelMode);
					step.Initialize(gameObject, l,rendering.sidesMaterial,rendering.material,rendering.width,rendering.height,rendering.distanceFromFloor,directions.useColliders);
				}
//			}
//			catch (Exception ex)
//			{
//				Debug.LogWarning ("[GODirections] - Error parsing the API data, debug for more information: " +ex);
//			}
		}
    }
}
