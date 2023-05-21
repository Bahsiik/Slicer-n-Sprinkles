using System;
using UnityEngine;

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
	public float clockSpeed = 1.0f; // 1.0f = realtime, < 1.0f = slower, > 1.0f = faster

	//-- internal vars
	private float msecs;

	private void Start()
	{
		//-- set real time
		if (realTime)
		{
			hour = DateTime.Now.Hour;
			minutes = DateTime.Now.Minute;
			seconds = DateTime.Now.Second;
		}
	}

	private void Update()
	{
		// foreach (Transform child in transform)
		// {
		// 	if (!child.GetComponent<MeshRenderer>().enabled) return;
		// }


		//-- calculate time
		msecs += Time.deltaTime * clockSpeed;

		if (msecs >= 1.0f)
		{
			msecs -= 1.0f;
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
