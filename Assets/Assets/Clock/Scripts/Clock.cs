using System;
using UnityEngine;

namespace Assets.Clock.Scripts
{
	public class Clock : MonoBehaviour
	{

		//-- set start time 00:00
		public int minutes;
		public int hour;
		public int seconds;
		public bool realTime = true;

		public GameObject pointerSeconds;
		public GameObject pointerMinutes;
		public GameObject pointerHours;

		//-- time speed factor
		// 1.0f = realtime, < 1.0f = slower, > 1.0f = faster
		public float clockSpeed = 1.0f;

		//-- internal vars
		private float _msecs;

		private void Start()
		{
			//-- set real time
			if (!realTime) return;
			hour = DateTime.Now.Hour;
			minutes = DateTime.Now.Minute;
			seconds = DateTime.Now.Second;
		}

		private void Update()
		{
			// foreach (Transform child in transform)
			// {
			// 	if (!child.GetComponent<MeshRenderer>().enabled) return;
			// }


			//-- calculate time
			_msecs += Time.deltaTime * clockSpeed;

			if (_msecs >= 1.0f)
			{
				_msecs -= 1.0f;
				seconds++;

				if (seconds >= 60)
				{
					seconds = 0;
					minutes++;

					if (minutes > 60)
					{
						minutes = 0;
						hour++;

						if (hour >= 24)
							hour = 0;
					}
				}
			}


			//-- calculate pointer angles
			var rotationSeconds = 360.0f / 60.0f * seconds;
			var rotationMinutes = 360.0f / 60.0f * minutes;
			var rotationHours = 360.0f / 12.0f * hour + 360.0f / (60.0f * 12.0f) * minutes;

			//-- draw pointers
			pointerSeconds.transform.localEulerAngles = new(0.0f, 0.0f, rotationSeconds);
			pointerMinutes.transform.localEulerAngles = new(0.0f, 0.0f, rotationMinutes);
			pointerHours.transform.localEulerAngles = new(0.0f, 0.0f, rotationHours);
		}
	}
}
