using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class MathUtils
{
	/// <summary>
	///     Generates a random number of a specified numeric type between a specified minimum and maximum value, with a bias towards certain values
	///     based on the bias parameter.
	/// </summary>
	/// <param name="min"> The minimum value to generate. </param>
	/// <param name="max"> The maximum value to generate. </param>
	/// <param name="bias">
	///     The degree of bias towards certain values. A value of 0.5 indicates no bias. A value of 0 indicates a bias towards the
	///     minimum value. A value of 1 indicates a bias towards the maximum value.
	/// </param>
	/// <returns>
	///     A random number of the specified type between the specified minimum and maximum values, biased towards certain values based on
	///     the bias parameter.
	/// </returns>
	public static float GenerateRandomNumber(float min, float max, float bias)
	{
		if (bias is < 0 or > 1) throw new ArgumentOutOfRangeException(nameof(bias), "Bias must be between 0 and 1.");
		if (min > max) throw new ArgumentOutOfRangeException(nameof(min), "Min must be less than or equal to max.");

		if (bias == 0) return min;

		const double tolerance = 0.0000005f;
		if (Math.Abs(bias - 1) < tolerance) return max;
		if (Math.Abs(min - max) < tolerance) return min;
		if (Math.Abs(bias - 0.5f) < tolerance) return Random.Range(min, max);

		var range = max - min;
		var mean = bias * range + min;
		var standardDev = (1 - bias) * range / 3;

		var x1 = Random.value;
		var x2 = Random.value;
		var r = Mathf.Sqrt(-2 * Mathf.Log(x1)) * Mathf.Cos(2 * Mathf.PI * x2);

		var value = mean + standardDev * r;
		return Mathf.Clamp(value, min, max);
	}
}
