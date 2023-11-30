using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;
using Zenject;

public class PreGameSettings : MonoBehaviour
{
    [Inject] private GameManager gameManager;
    [Inject] private GameModeManager gameModeManager;
    public TextMeshProUGUI textRank;
    public SpriteResolver sendShip;
    public SpriteRenderer colorSendShips;

    public UnityEngine.UI.Slider sliderRank;

    private void Start()
    {
        UpdateRankValueText();
    }
    public void OnEnable()
    {
        CheckSkinUnits();
        CheckColorUnits();
        CheckRank();
    }

    public void UpdateRankValueText()
    {
        if (sliderRank.value == 1) textRank.text = "Мичман";
        if (sliderRank.value == 2) textRank.text = "Капитан";
        if (sliderRank.value == 3) textRank.text = "Адмирал";

        gameManager.ChangeDifficult((int)sliderRank.value);
    }

    private void CheckRank()
    {
        int indexRank = (int)gameModeManager.currentDifficulty;
        sliderRank.value = indexRank;
    }

    public void OnSliderRankValueChanged()
    {
        UpdateRankValueText();
    }
    private void CheckSkinUnits()
    {
        int index;
        index = gameManager.skinUnits;
        if (index == 1) OnImage1Click();
        if (index == 2) OnImage2Click();
        if (index == 3) OnImage3Click();
        if (index == 4) OnImage4Click();
        if (index == 5) OnImage5Click();
    }
    private void CheckColorUnits()
    {
        colorSendShips.color = gameManager.colorPlayer;
    }
    public void OnImage1Click()
    {
        gameManager.ChangeSkin(1);
        ChangeSprite("ShowShips", "ShowShip1");
    }
    public void OnImage2Click()
    {
        gameManager.ChangeSkin(2);
        ChangeSprite("ShowShips", "ShowShip2");
    }
    public void OnImage3Click()
    {
        gameManager.ChangeSkin(3);
        ChangeSprite("ShowShips", "ShowShip3");
    }

    public void OnImage4Click()
    {
        gameManager.ChangeSkin(4);
        ChangeSprite("ShowShips", "ShowShip4");
    }
    public void OnImage5Click()
    {
        gameManager.ChangeSkin(5);
        ChangeSprite("ShowShips", "ShowShip5");
    }
    private void ChangeSprite(string category, string label)
    {
        sendShip.SetCategoryAndLabel(category, label);
    }
    /*    public void SetColorUnits1()
        {
            Color buttonColor = showColor1.color;
            gameManager.ChangeColor(buttonColor);
            CheckColorUnits();
        }
        public void SetColorUnits2()
        {
            Color buttonColor = showColor2.color;
            gameManager.ChangeColor(buttonColor);
            CheckColorUnits();
        }
        public void SetColorUnits3()
        {
            Color buttonColor = showColor3.color;
            gameManager.ChangeColor(buttonColor);
            CheckColorUnits();
        }
        public void SetColorUnits4()
        {
            Color buttonColor = showColor4.color;
            gameManager.ChangeColor(buttonColor);
            CheckColorUnits();
        }
        public void SetColorUnits5()
        {
            Color buttonColor = showColor5.color;
            gameManager.ChangeColor(buttonColor);
            CheckColorUnits();
        }
        public void SetColorUnits6()
        {
            Color buttonColor = showColor6.color;
            gameManager.ChangeColor(buttonColor);
            CheckColorUnits();
        }
        public void SetColorUnits7()
        {
            Color buttonColor = showColor7.color;
            gameManager.ChangeColor(buttonColor);
            CheckColorUnits();
        }*/
/*    public void SetColorUnits8()
    {
        Color buttonColor = showColor8.color;
        gameManager.ChangeColor(buttonColor);
        CheckColorUnits();
    }*/

    public void GetColor(Button button)
    {
        button.TryGetComponent(out Image image);
        gameManager.ChangeColor(image.color);
        CheckColorUnits();
    }

}
