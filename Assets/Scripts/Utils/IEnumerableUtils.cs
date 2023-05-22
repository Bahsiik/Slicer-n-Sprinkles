using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Utils
{
	public static class EnumerableUtils
	{
		[NotNull]
		public static IEnumerable<(T item, int index)> WithIndex<T>([NotNull] this IEnumerable<T> source) =>
			source.Select(static (item, index) => (item, index));
	}
}
