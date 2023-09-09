using Game;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;


public class PreGameSettings : MonoBehaviour
{
    public TextMeshProUGUI textRank;
    public SpriteResolver sendShip;
    public SpriteRenderer colorSendShips;

    public UnityEngine.UI.Slider sliderRank;

    public UnityEngine.UI.Image showColor1;
    public UnityEngine.UI.Image showColor2;
    public UnityEngine.UI.Image showColor3;
    public UnityEngine.UI.Image showColor4;
    public UnityEngine.UI.Image showColor5;
    public UnityEngine.UI.Image showColor6;
    public UnityEngine.UI.Image showColor7;
    public UnityEngine.UI.Image showColor8;

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
        GameManager.Instance.ChangeRank((int)sliderRank.value);
    }
    private void CheckRank()
    {
        int indexRank = (int)GameManager.Instance.selectedDifficulty;
        sliderRank.value = indexRank;
    }
    public void OnSliderRankValueChanged()
    {
        UpdateRankValueText();
    }
    private void CheckSkinUnits()
    {
        int index;
        index = GameManager.Instance.skinUnits;
        if (index == 1) OnImage1Click();
        if (index == 2) OnImage2Click();
        if (index == 3) OnImage3Click();
        if (index == 4) OnImage4Click();
        if (index == 5) OnImage5Click();
    }
    private void CheckColorUnits()
    {
        colorSendShips.color = GameManager.Instance.colorUnits;
    }
    public void OnImage1Click()
    {
        GameManager.Instance.skinUnits = 1;
        ChangeSprite("ShowShips", "ShowShip1");
    }
    public void OnImage2Click()
    {
        GameManager.Instance.skinUnits = 2;
        ChangeSprite("ShowShips", "ShowShip2");
    }
    public void OnImage3Click()
    {
        GameManager.Instance.skinUnits = 3;
        ChangeSprite("ShowShips", "ShowShip3");
    }

    public void OnImage4Click()
    {
        GameManager.Instance.skinUnits = 4;
        ChangeSprite("ShowShips", "ShowShip4");
    }
    public void OnImage5Click()
    {
        GameManager.Instance.skinUnits = 5;
        ChangeSprite("ShowShips", "ShowShip5");
    }
    private void ChangeSprite(string category, string label)
    {
        sendShip.SetCategoryAndLabel(category, label);
        GameManager.Instance.ChangeSkinUnits();

    }
    public void SetColorUnits1()
    {
        Color buttonColor = showColor1.color;
        GameManager.Instance.ChangeColornUnits(buttonColor);
        CheckColorUnits();
    }
    public void SetColorUnits2()
    {
        Color buttonColor = showColor2.color;
        GameManager.Instance.ChangeColornUnits(buttonColor);
        CheckColorUnits();
    }
    public void SetColorUnits3()
    {
        Color buttonColor = showColor3.color;
        GameManager.Instance.ChangeColornUnits(buttonColor);
        CheckColorUnits();
    }
    public void SetColorUnits4()
    {
        Color buttonColor = showColor4.color;
        GameManager.Instance.ChangeColornUnits(buttonColor);
        CheckColorUnits();
    }
    public void SetColorUnits5()
    {
        Color buttonColor = showColor5.color;
        GameManager.Instance.ChangeColornUnits(buttonColor);
        CheckColorUnits();
    }
    public void SetColorUnits6()
    {
        Color buttonColor = showColor6.color;
        GameManager.Instance.ChangeColornUnits(buttonColor);
        CheckColorUnits();
    }
    public void SetColorUnits7()
    {
        Color buttonColor = showColor7.color;
        GameManager.Instance.ChangeColornUnits(buttonColor);
        CheckColorUnits();
    }
    public void SetColorUnits8()
    {
        Color buttonColor = showColor8.color;
        GameManager.Instance.ChangeColornUnits(buttonColor);
        CheckColorUnits();
    }
}
