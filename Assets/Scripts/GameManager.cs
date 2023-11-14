using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    public int skinUnits;
    public Color colorPlayer;

    private void Start()
    {
        LoadSkin();
        LoadColor();
    }

    public void ChangeColor(Color color)
    {
        colorPlayer = color;
        SaveColor(colorPlayer);
    }

    public void ChangeSkin(int index)
    {
        skinUnits = index;
        SaveSkin(skinUnits);
    }

    private void SaveColor(Color color)
    {
        PlayerPrefs.SetFloat("ColorR", color.r);
        PlayerPrefs.SetFloat("ColorG", color.g);
        PlayerPrefs.SetFloat("ColorB", color.b);
        PlayerPrefs.SetFloat("ColorA", color.a);
    }

    private void LoadColor()
    {
        if (PlayerPrefs.HasKey("ColorR") && PlayerPrefs.HasKey("ColorG") && PlayerPrefs.HasKey("ColorB") && PlayerPrefs.HasKey("ColorA"))
        {
            float r = PlayerPrefs.GetFloat("ColorR");
            float g = PlayerPrefs.GetFloat("ColorG");
            float b = PlayerPrefs.GetFloat("ColorB");
            float a = PlayerPrefs.GetFloat("ColorA");

            colorPlayer = new Color(r, g, b, a);
        }
        else colorPlayer = new Color(1f, 1f, 1f, 1f);
    }

    private void SaveSkin(int index)
    {
        PlayerPrefs.SetInt("Skin", index);
    }

    private void LoadSkin()
    {
        if (PlayerPrefs.HasKey("Skin"))
        {
            skinUnits = PlayerPrefs.GetInt("Skin");
        }
        else skinUnits = 3;
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();

        LoadSkin();
        LoadColor();
    }
}

