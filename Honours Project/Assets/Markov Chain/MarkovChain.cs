using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MarkovChain<T> : IEnumerable<T> // T has to be an enum
{
	private List<T> elements;
	private List<List<double>> transitionMatrix; // The probabilty of each row must sum to 1.0

	private List<T> chain = new List<T>();

	public T this[int i] { get { return chain[i]; } }
	public int Count { get { return chain.Count; } }

	public MarkovChain() { }

	public MarkovChain(List<List<double>> transitionMatrix)
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

			// Generate a random value between 0 and 1
			double rand = UnityEngine.Random.Range(0.0f, 1.0f);

			// The probability of the current element plus the probability of all the
			// elements before
			double cumulative = 0.0f;

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
