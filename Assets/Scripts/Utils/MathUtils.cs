using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MathUtils {
	/// <summary>
	///     Generates a random number of a specified numeric type between a specified minimum and maximum value, with a bias towards certain values
	///     based on the bias parameter.
	/// </summary>
	/// <typeparam name="T"> The numeric type of the generated value. </typeparam>
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
	public static T GenerateRandomNumber<T>(T min, T max, float bias) where T : struct, IComparable, IConvertible, IFormattable {
		// Convert the minimum and maximum values to floats.
		var floatMin = Convert.ToSingle(min);
		var floatMax = Convert.ToSingle(max);

		// Calculate the range of values to generate.
		var range = floatMax - floatMin;

		// Calculate the midpoint of the range.
		var midpoint = floatMin + range / 2f;

		// Calculate the adjustment factor based on the bias parameter.
		var adjustment = Mathf.Pow(2f * (1f - bias), 2f) - 1f;

		// Generate a random value between -1 and 1, adjusted by the bias parameter.
		var rand = Random.Range(-1f, 1f) * adjustment;

		// Calculate the deviation from the midpoint based on the adjusted random value.
		var deviation = range * rand / 2f;

		// Calculate the biased random value by adding the deviation to the midpoint.
		var floatNum = midpoint + deviation;

		// Convert the biased random value back to the specified numeric type.
		var num = (T) Convert.ChangeType(floatNum, typeof(T));

		// Ensure that the generated number is within the specified range.
		return num.CompareTo(min) < 0
			? min
			: num.CompareTo(max) > 0
				? max
				: num;
	}
}
