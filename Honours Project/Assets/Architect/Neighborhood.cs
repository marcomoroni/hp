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
		public ParticleSystemRenderer wind;

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




			// Set Y pos of bridges (now that you know the max pos possible)
			for (int s = 0; s < structures.Count; s++)
			{
				for (int b = 0; b < structures[s].Item2.blocks.Count; b++)
				{
					Block block = structures[s].Item2.blocks[b].Item2;

					if (block.properties.category == BlockCategory.Bridge)
					{
						// Get left / right heights
						int heightLeft = structures[s - 1].Item2.Height - 100;
						int heightLeftRoof = structures[s - 1].Item2.blocks.Last().Item2.properties.height;
						int heightRight = structures[s + 1].Item2.Height - 100;
						int heightRightRoof = structures[s + 1].Item2.blocks.Last().Item2.properties.height;

						int maxHeight = Mathf.Min(heightLeft - heightLeftRoof, heightRight - heightRightRoof) - block.properties.height;
						int newHeight = Mathf.Max(0, UnityEngine.Random.Range(0, maxHeight + 1)); // 0 -> bridge can be on ground

						//Debug.Log(heightLeft + " " + heightLeft + " " + maxHeight + " " + newHeight);

						Vector2Int newPos = structures[s].Item2.blocks[b].Item1.With(y: newHeight);
						structures[s].Item2.blocks[b] = (newPos, structures[s].Item2.blocks[b].Item2);
					}
				}
			}




			// Merge textures
			var t = MergeTextures();
			var sr = GetComponent<SpriteRenderer>();
			sr.sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));

			// Disable single sprites
			DisableMergedSprites();

			// Set correct sorting layer
			sr.sortingOrder = - Mathf.FloorToInt(transform.position.z);



			// Set wind sorting lay
			wind.sortingOrder = sr.sortingOrder - 1;



			// Align to center of neighborhood origin
			transform.position = transform.position + new Vector3(-(float)Width / 2 / 100, 0, 0);
		}

		private (GameObject, Structure) CreateStructure(StructureType type, int pos)
		{
			GameObject go = Instantiate(structurePrefab, transform.position.With(x: transform.position.x + (float)pos / 100), transform.rotation);
			go.name = structurePrefab.name;
			go.transform.parent = gameObject.transform;
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

		private void DisableMergedSprites()
		{
			foreach (var structure in structures)
			{
				foreach (var block in structure.Item2.blocks)
				{
					block.Item2.gameObject.GetComponent<SpriteRenderer>().enabled = false;
				}
			}
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