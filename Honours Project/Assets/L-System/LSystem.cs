using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LSystem<S> : IEnumerable<S>
{
	List<S> derivation = new List<S>();
	public int Count { get { return derivation.Count; } }
	public S this[int i] { get { return derivation[i]; } }

	// Note: Use Type (of S) instead of S in order to allow L-System to work
	// with INSTANCES of S
	Dictionary<Type, Func<List<S>>> rules = new Dictionary<Type, Func<List<S>>>();

	public LSystem(List<S> axioms, Dictionary<Type, Func<List<S>>> rules = null)
	{
		derivation.AddRange(axioms);
		if (rules != null) this.rules = rules;
	}

	public void AddRule(Type symbol, Func<List<S>> rule)
	{
		// Add or update
		rules[symbol] = rule;
	}

	/// <summary>
	/// Expand this derivation once.
	/// </summary>
	public void Expand()
	{
		List<S> newProduction = new List<S>();

		foreach (var symbol in derivation)
		{
			// If there's a rule expand the symbol, else add the same symbol
			if (rules.ContainsKey(symbol.GetType()))
			{
				newProduction.AddRange(rules[symbol.GetType()]());
			}
			else
			{
				newProduction.Add(symbol);
			}
		}

		derivation = new List<S>(newProduction);
	}

	/// <summary>
	/// Check whether the derivation has non-terminal symbols.
	/// </summary>
	public bool CanBeExpanded()
	{
		foreach (var symbol in derivation)
		{
			if (rules.ContainsKey(symbol.GetType()))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerator<S> GetEnumerator() { return derivation.GetEnumerator(); }

	IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}
