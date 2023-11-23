using System;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] GameModeManager gameModeManager;
    [Inject] ProgressPlayer player;
    public int level;
    public int points;

    public int difficult;
    public int planets;
    public int skinUnits;
    public Color colorPlayer;
    public bool isPaused = false;

    private void Start()
    {
        LoadSkin();
        LoadColor();
        LoadDifficult();
        LoadPlanets();
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

    public void ChangeDifficult(int index)
    {
        difficult = index;
        SaveDifficult(difficult);
        gameModeManager.ChangeDifficulty(difficult);
    }

    public void ChangePlanets(int count)
    { 
        planets = count;
        SavePlanets(planets);
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

    private void SaveDifficult(int index)
    {
        PlayerPrefs.SetInt("Difficult", index);
    }

    private void LoadDifficult()
    {
        if (PlayerPrefs.HasKey("Difficult"))
        {
            difficult = PlayerPrefs.GetInt("Difficult");
        }
        else difficult = 2;

        gameModeManager.ChangeDifficulty(difficult);
    }

    private void SavePlanets(int count)
    {
        PlayerPrefs.SetInt("Planets", count);
    }

    private void LoadPlanets()
    {
        if (PlayerPrefs.HasKey("Planets"))
        {
            planets = PlayerPrefs.GetInt("Planets");
        }
        else planets = 15;
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
        LoadDifficult();
        LoadPlanets();

        player.LoadData();
    }
}

