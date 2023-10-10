using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.U2D.Animation;

namespace Game
{
	public class GameManager : MonoBehaviour
	{
		public enum Difficulty
		{
			Easy = 1,
			Medium = 2,
			Hard = 3
		}

		public static GameManager Instance { get; private set; }

		public bool isPaused = false;
		public int planetCount;
		public int skinUnits = 3;
		public Color colorPlanet;
		public SpriteRenderer playerPlanetPrefab;
		public Difficulty selectedDifficulty = Difficulty.Medium;

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
		public void ChangeRank(int value)
		{
			selectedDifficulty = (Difficulty)value;

			AIEasyController easy = enemyPrefab.GetComponent<AIEasyController>();
			AIMediumController medium = enemyPrefab.GetComponent<AIMediumController>();
			AIHardController hard = enemyPrefab.GetComponent<AIHardController>();

			easy.enabled = false;
			medium.enabled = false;
			hard.enabled = false;

			if (selectedDifficulty == Difficulty.Easy) easy.enabled = true;
			else if (selectedDifficulty == Difficulty.Medium) medium.enabled = true;
			else if (selectedDifficulty == Difficulty.Hard) hard.enabled = true;
		}
	}
}
