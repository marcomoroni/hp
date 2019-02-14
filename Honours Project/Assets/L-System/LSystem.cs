using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LSystem<S> : IEnumerable<S>
{
	List<S> production = new List<S>();
	public int Count { get { return production.Count; } }
	public S this[int i] { get { return production[i]; } }

	List<S> symbols;
	Dictionary<S, Func<List<S>>> rules = new Dictionary<S, Func<List<S>>>();

	public LSystem(List<S> symbols, List<S> axioms)
	{
		this.symbols = symbols;
		production.AddRange(axioms);
	}

	public void AddRule(S symbol, Func<List<S>> rule)
	{
		// Add or update
		rules[symbol] = rule;
	}

	// Expand once
	public void Expand()
	{
		List<S> newProduction = new List<S>();

		foreach (var symbol in production)
		{
			// If there's a rule expand the symbol, else add the same symbol
			if (rules.ContainsKey(symbol))
			{
				newProduction.AddRange(rules[symbol]());
			}
			else
			{
				newProduction.Add(symbol);
			}
		}

		production = new List<S>(newProduction);
	}

	public IEnumerator<S> GetEnumerator() { return production.GetEnumerator(); }

	IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}
