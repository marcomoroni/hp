﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Architect
{
	[System.Serializable]
	public class StructureProperties
	{
		public int height;
		public int width;

		// L-System properties
		public List<SLS_Symbol> axioms = new List<SLS_Symbol>();
		public Dictionary<Type, Func<List<SLS_Symbol>>> rules = new Dictionary<Type, Func<List<SLS_Symbol>>>();

		public StructureProperties(NeighborhoodProperties neighborhoodProperties, StructureType structureType)
		{
			height = UnityEngine.Random.Range(neighborhoodProperties.minHeight, neighborhoodProperties.maxHeight + 1);
			//width = UnityEngine.Random.Range(1, 3 + 1);
			if (structureType == StructureType.Empty)
			{
				width = UnityEngine.Random.Range(10, 30 + 1); // TEMP
			}
			else
			{
				int[] possibleWidths = ArchitectTools.PossibleWidths;
				width = possibleWidths[UnityEngine.Random.Range(0, possibleWidths.Length - 1)];
			}

			// Create L-System properties
			switch (structureType)
			{
				case StructureType.Generic:
					axioms.Add(new SLS_InsertGeneric());
					axioms.Add(new SLS_F_Roof(this));
					break;

				case StructureType.Bridge:
					axioms.Add(new SLS_T_Empty(70));
					axioms.Add(new SLS_F_Generic(this));
					break;
			}

			// Create rules
			rules[typeof(SLS_InsertGeneric)] = () =>
			{
				return new List<SLS_Symbol>
				{
					new SLS_F_Generic(this), new SLS_InsertGeneric()
				};
			};
		}
	}
}