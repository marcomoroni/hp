using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public static class Extensions
{
	/// <summary>
	/// Change the values of x, y and/or z.
	/// </summary>
	/// <returns>The <c>Vector2</c> with the specified values.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector2 With(this Vector2 original, float? x = null, float? y = null)
	{
		return new Vector2(x ?? original.x, y ?? original.y);
	}

	/// <summary>
	/// Change the values of x, y and/or z.
	/// </summary>
	/// <returns>The <c>Vector2Int</c> with the specified values.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector2Int With(this Vector2Int original, int? x = null, int? y = null)
	{
		return new Vector2Int(x ?? original.x, y ?? original.y);
	}

	/// <summary>
	/// Change the values of x, y and/or z.
	/// </summary>
	/// <returns>The <c>Vector3</c> with the specified values.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
	{
		return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
	}

	/// <summary>
	/// Direction to another <c>Vector3</c>.
	/// </summary>
	/// <returns>The direction vector to <c>destination</c>.</returns>
	/// <param name="destination">Destination.</param>
	public static Vector3 DirectionTo(this Vector3 source, Vector3 destination)
	{
		return Vector3.Normalize(destination - source);
	}

	/// <summary>
	/// Change the values of x, y and/or z.
	/// </summary>
	/// <returns>The <c>Vector3Int</c> with the specified values.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3Int With(this Vector3Int original, int? x = null, int? y = null, int? z = null)
	{
		return new Vector3Int(x ?? original.x, y ?? original.y, z ?? original.z);
	}

	/// <summary>
	/// Change the values of x, y, z and/or w.
	/// </summary>
	/// <returns>The <c>Vector4</c> with the specified values.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	/// <param name="w">The w coordinate.</param>
	public static Vector4 With(this Vector4 original, float? x = null, float? y = null, float? z = null, float? w = null)
	{
		return new Vector4(x ?? original.x, y ?? original.y, z ?? original.z, w ?? original.w);
	}

	/// <summary>
	/// Direction to another <c>Transform</c>.
	/// </summary>
	/// <returns>The direction vector to <c>destination</c>.</returns>
	/// <param name="destination">Destination.</param>
	public static Vector3 DirectionTo(this Transform source, Transform destination)
	{
		return source.position.DirectionTo(destination.position);
	}

	/// <summary>
	/// Shuffle the list using the Fisher-Yates method.
	/// </summary>
	public static void Shuffle<T>(this IList<T> list)
	{
		System.Random rng = new System.Random();
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	/// <summary>
	/// Returns a random item from the list.
	/// Sampling with replacement.
	/// </summary>
	/// <returns>A random item from the list.</returns>
	public static T GetRandom<T>(this IList<T> list)
	{
		if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list.");
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	/// <summary>
	/// Removes a random item from the list, returning that item.
	/// Sampling without replacement.
	/// </summary>
	/// <returns>The item removed.</returns>
	public static T GetAndRemoveRandom<T>(this IList<T> list)
	{
		if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot remove a random item from an empty list.");
		int index = UnityEngine.Random.Range(0, list.Count);
		T item = list[index];
		list.RemoveAt(index);
		return item;
	}

	public static T GetLast<T>(this IList<T> list)
	{
		if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot get item from an empty list.");
		return list[list.Count - 1];
	}

	public static T GetFirst<T>(this IList<T> list)
	{
		if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot get item from an empty list.");
		return list[0];
	}

	public static T GetAndRemoveLast<T>(this IList<T> list)
	{
		if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot remove item from an empty list.");
		T item = list[list.Count - 1];
		list.Remove(item);
		return item;
	}

	public static T GetAndRemoveFirst<T>(this IList<T> list)
	{
		if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot remove item from an empty list.");
		T item = list[0];
		list.Remove(item);
		return item;
	}

	/// <summary>
	/// Changes the direction of a rigidbody without changing its speed.
	/// </summary>
	/// <param name="direction">New direction.</param>
	public static void ChangeDirection(this Rigidbody rigidbody, Vector3 direction)
	{
		rigidbody.velocity = direction * rigidbody.velocity.magnitude;
	}
}

public static class UnityUtil
{
	public static string GetSelectedPathOrFallback()
	{
		string path = "Assets";

		foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
		{
			path = AssetDatabase.GetAssetPath(obj);
			if (!string.IsNullOrEmpty(path) && File.Exists(path))
			{
				path = Path.GetDirectoryName(path);
				break;
			}
		}
		return path;
	}
}