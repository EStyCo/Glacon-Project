using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.U2D.Animation;

namespace Game
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance { get; private set; }

		public bool isPaused = false;
		public int skinUnits = 3;
		public Color colorPlanet;
		public SpriteRenderer playerPlanetPrefab;

		public SpriteRenderer unitPrefab;
		public GameObject enemyPrefab;

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}

        private void Start()
		{
			Color startingColor = new Color(1f, 1f, 1f, 1f);
			ChangeColorUnits(startingColor);
		}

		public void ChangeColorUnits(Color color)
		{
			colorPlanet = color;
			playerPlanetPrefab.color = colorPlanet;
		}
	}
}
