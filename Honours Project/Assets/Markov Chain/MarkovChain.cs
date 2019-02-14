using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkovChain<T> // T has to be an enum
{
	public List<T> elements;
	public List<List<double>> transitionMatrix; // The probabilty of each row must sum to 1.0

	public List<T> chain = new List<T>();

	public MarkovChain() { }

	public MarkovChain(List<T> elements, List<List<double>> transitionMatrix)
	{
		this.elements = elements;
		this.transitionMatrix = transitionMatrix;
	}

	public void GenerateNext(int elementsToAdd = 1)
	{
		for (int i = 0; i < elementsToAdd; i++)
		{
			if (chain.Count == 0)
			{
				chain.Add(elements[0]);
			}
			else
			{
				T previous = chain[chain.Count - 1];

				// Generate a random value between 0 and 1
				double rand = Random.Range(0.0f, 1.0f);

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

				Debug.LogError("Problem in GetNextElement.");
			}
		}
	}
}
