using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Architect
{
	[AddComponentMenu("Architect/Neighborhood")]
	public class Neighborhood : MonoBehaviour
	{
		public NeighborhoodProperties properties;

		[Header("Prefabs")]
		public GameObject structurePrefab;

		// <pos, structure>
		private List<(Vector2Int, Structure)> structures = new List<(Vector2Int, Structure)>();

		public int Width
		{
			get { return structures.Sum(s => s.Item2.Width); }
		}

		// Highest structure
		public int Height
		{
			get { return structures.Max(s => s.Item2.Height); }
		}

		// Generate as a Markov chain
		public void Generate()
		{
			MarkovChain<StructureType> structureChain = new MarkovChain<StructureType>(properties.transitionMatrix);

			// Generate until lenght is surpassed and has valid ending
			do
			{
				StructureType newStructureType = structureChain.GenerateNext(); // Not using this for now. Will affect CreateStructure()

				// Ignore StructureType.Start
				if (newStructureType != StructureType.Start)
				{
					var newStructure = CreateStructure(newStructureType, Width);
					//structures.Add(newStructure.Item2.properties);
				}
			}
			while (Width <= properties.width || properties.cannotEndWith.Contains(structureChain[structureChain.Count - 1]));





			var t = MergeTextures();
			var sr = GetComponent<SpriteRenderer>();
			sr.sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
		}

		private (GameObject, Structure) CreateStructure(StructureType type, int pos)
		{
			GameObject go = Instantiate(structurePrefab, transform.position.With(x: transform.position.x + (float)pos / 100), transform.rotation);
			go.name = structurePrefab.name;
			Structure structure = go.GetComponent<Structure>();
			structure.properties = new StructureProperties(properties, type);
			structure.Generate(); // TODO: Change parameters depending on Structure Type OR add generation info to prameters

			// Add to list
			structures.Add((new Vector2Int(pos, 0), structure));

			return (go, structure);
		}
		
		private Texture2D MergeTextures()
		{
			Texture2D finalTexture = new Texture2D(Width, Height);

			// Start wit transparent texture
			{
				Color[] initialColours = new Color[finalTexture.width * finalTexture.height];
				for (int i = 0; i < initialColours.Length; i++)
				{
					initialColours[i] = new Color(0, 0, 0, 0);
				}
				finalTexture.SetPixels(initialColours);
			}

			// Merge textures
			foreach (var structure in structures)
			{
				Vector2Int structurePos = new Vector2Int(structure.Item1.x, structure.Item1.y);

				foreach (var block in structure.Item2.blocks)
				{
					Vector2Int blockPos = structurePos + new Vector2Int(block.Item1.x, block.Item1.y);

					Sprite blockSprite = block.Item2.gameObject.GetComponent<SpriteRenderer>().sprite; // Spaghetti alert
					Rect blockRect = blockSprite.rect;
					int bX = Mathf.FloorToInt(blockRect.x);
					int bY = Mathf.FloorToInt(blockRect.y);
					int bWidth = Mathf.FloorToInt(blockRect.width);
					int bHeight = Mathf.FloorToInt(blockRect.height);
					Color[] blockColors = blockSprite.texture.GetPixels(bX, bY, bWidth, bHeight);
					finalTexture.SetPixels(blockPos.x, blockPos.y, bWidth, bHeight, blockColors); // TODO: USE NormalBlend() !
				}
			}

			// Set final texture and apply
			finalTexture.Apply();

			// Texture settings -> MAY CHANGE THEM
			finalTexture.wrapMode = TextureWrapMode.Clamp;
			//finalTexture.filterMode = FilterMode.Point;

			return finalTexture;
		}

		// TEMP: Make this a static function
		private Color NormalBlend(Color a, Color b)
		{
			float bAlpha = b.a;
			float aAlpha = (1 - bAlpha) * a.a;

			Color aLayer = a * aAlpha;
			Color bLayer = b * bAlpha;

			return aLayer + bLayer;
		}

		private void OnDrawGizmos()
		{
			if (properties != null)
			{
				float makeBigger = 0.2f;

				Gizmos.color = new Color(1, 0.5f, 0);

				float actualWidth = (float)properties.width / 100 + makeBigger;
				float actualMinHeight = (float)properties.minHeight / 100 + makeBigger;
				float actualMaxHeight = (float)properties.maxHeight / 100 + makeBigger;

				Gizmos.DrawWireCube(transform.position + new Vector3(actualWidth / 2, actualMinHeight / 2) - new Vector3(makeBigger / 2, makeBigger / 2), new Vector3(actualWidth, actualMinHeight, 0));
				Gizmos.DrawWireCube(transform.position + new Vector3(actualWidth / 2, actualMaxHeight / 2) - new Vector3(makeBigger / 2, makeBigger / 2), new Vector3(actualWidth, actualMaxHeight, 0));
			}
		}
	}
}