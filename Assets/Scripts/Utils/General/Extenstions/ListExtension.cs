using System;
using System.Linq;
using System.Collections.Generic;

namespace ListExtensions
{
	public static class ListExtension
	{
		public static Dictionary<TKey, TValue> ShallowCopy<TKey, TValue>(this Dictionary<TKey, TValue> dict)
		{
			return dict.Keys.ToDictionary(key => key, key => dict[key]);
		}
		public static string JoinToString<T>(this IEnumerable<IEnumerable<T>> list, string separator = ",", string separator2 = "\n")
		{
			if (list == null)
				return "";

			return list.Select(x => x.JoinToString(separator)).JoinToString(separator2);
		}
		public static string JoinToString<T>(this IEnumerable<List<T>> list, string separator = ",", string separator2 = "\n")
		{
			if (list == null)
				return "";

			return list.Select(x => x.JoinToString(separator)).JoinToString(separator2);
		}
		public static string JoinToString<T>(this IEnumerable<T> list, string separator = ",")
		{
			if (list == null)
				return "";

			return string.Join(separator, list.Select(x => (x == null) ? "" : x.ToString()).ToArray());
		}
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (var item in source)
				action(item);
		}
		public static List<List<T>> GetPermutations<T>(this IEnumerable<T> list)
		{
			if (list == null)
				return null;

			var perms = new List<List<T>>();
			for (var i = list.Count(); i > 0; i--)
			{
				perms.AddRange(GetPermutations(list, i).Select(x => x.ToList()));
			}
			//UnityEngine.Debug.Log("perms: " + perms.Select(t => t.JoinToString(",")).JoinToString("\n"));
			return perms;
		}
		static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
		{
			if (length == 1) return list.Select(t => (IEnumerable<T>)new T[] { t });
			return GetPermutations(list, length - 1)
				.SelectMany(t => list.Where(o => !t.Contains(o)),
					(t1, t2) => t1.Concat(new T[] { t2 }));
		}
		public static List<T> Subtract<T>(this IEnumerable<T> list, IEnumerable<T> elems)
		{
			var m = elems.ToList();
			var res = new List<T>();
			foreach (var val in list)
			{
				if (m.Contains(val))
					m.RemoveAt(m.FindIndex(x => EqualityComparer<T>.Default.Equals(x, val)));
				else
					res.Add(val);
			}
			return res;
		}
		public static T Pop<T>(this List<T> list)
		{
			if (list.Count == 0)
				throw new IndexOutOfRangeException("Can't Pop(): List does not contain any elements!");

			var m = list.Last();
			list.RemoveAt(list.Count - 1);
			return m;
		}
		public static void AddMany<T>(this List<T> list, params T[] elements)
		{
			list.AddRange(elements);
		}
	}
}