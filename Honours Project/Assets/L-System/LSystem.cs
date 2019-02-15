using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LSystem<S> : IEnumerable<S>
{
	List<S> derivation = new List<S>();
	public int Count { get { return derivation.Count; } }
	public S this[int i] { get { return derivation[i]; } }

	List<S> symbols;
	Dictionary<S, Func<List<S>>> rules = new Dictionary<S, Func<List<S>>>();

	public LSystem(List<S> symbols, List<S> axioms)
	{
		this.symbols = symbols;
		derivation.AddRange(axioms);
	}

	public void AddRule(S symbol, Func<List<S>> rule)
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
			if (rules.ContainsKey(symbol))
			{
				newProduction.AddRange(rules[symbol]());
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
			if (rules.ContainsKey(symbol))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerator<S> GetEnumerator() { return derivation.GetEnumerator(); }

	IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}
