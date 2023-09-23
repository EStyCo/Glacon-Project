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
		public Color colorUnits;
		public Difficulty selectedDifficulty = Difficulty.Medium;

		public SpriteResolver skinUnitsPrefab;
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
       /* private void Update()
        {
            if (Screen.width != 1920 || Screen.height != 1080) 
			{
				Screen.SetResolution(1920, 1080, false);
			}
        }*/
        private void Start()
		{
			/*Screen.SetResolution(1920, 1080, false);*/

			Color startingColor = new Color(1f, 1f, 1f, 1f);
			ChangeSkinUnits();
			ChangeColornUnits(startingColor);
		}
		public void ChangeSkinUnits()
		{
			if (skinUnits == 1)
			{
				skinUnitsPrefab.SetCategoryAndLabel("Ships", "Ship1");
			}
			if (skinUnits == 2)
			{
				skinUnitsPrefab.SetCategoryAndLabel("Ships", "Ship2");
			}
			if (skinUnits == 3)
			{
				skinUnitsPrefab.SetCategoryAndLabel("Ships", "Ship3");
			}
			if (skinUnits == 4)
			{
				skinUnitsPrefab.SetCategoryAndLabel("Ships", "Ship4");
			}
			if (skinUnits == 5)
			{
				skinUnitsPrefab.SetCategoryAndLabel("Ships", "Ship5");
			}
		}
		public void ChangeColornUnits(Color color)
		{
			unitPrefab.color = color;
			colorUnits = color;
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
