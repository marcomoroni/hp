using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MarkovChain<T> : IEnumerable<T> // T has to be an enum
{
	private List<T> elements;
	private List<List<int>> transitionMatrix;

	private List<T> chain = new List<T>();

	public T this[int i] { get { return chain[i]; } }
	public int Count { get { return chain.Count; } }

	public MarkovChain() { }

	public MarkovChain(List<List<int>> transitionMatrix)
	{
		this.elements = ((T[])Enum.GetValues(typeof(T))).ToList();
		this.transitionMatrix = transitionMatrix;
	}

	public T GenerateNext()
	{
		if (chain.Count == 0)
		{
			chain.Add(elements[0]);
		}
		else
		{
			T previous = chain[chain.Count - 1];

			// Weights sum
			int weightsSum = 0;
			for (int i = 0; i < transitionMatrix.Count; i++)
			{
				weightsSum += transitionMatrix[elements.IndexOf(previous)][i];
			}

			// Generate a random value between 0 and 1
			int rand = UnityEngine.Random.Range(0, weightsSum + 1);

			// The probability of the current element plus the probability of all the
			// elements before
			int cumulative = 0;

			for (int a = 0; a < elements.Count; a++)
			{
				// Add the probability of this element
				cumulative += transitionMatrix[elements.IndexOf(previous)][a];

				// Compare the cumulative probability
				if (rand < cumulative)
				{
					chain.Add(elements[a]);
					break;
				}
			}

			//Debug.LogError("Problem in GetNextElement.");
		}

		return chain[chain.Count - 1];
	}

	public List<T> GenerateNext(int newElements)
	{
		List<T> output = new List<T>();

		for (int i = 0; i < newElements; i++)
		{
			output.Add(GenerateNext());
		}

		return output;
	}

	public IEnumerator<T> GetEnumerator() { return chain.GetEnumerator(); }

	IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}
