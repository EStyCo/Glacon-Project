using System;
using System.Drawing;
using UnityEngine;
using Zenject;
using Color = UnityEngine.Color;

public class GameManager : MonoBehaviour
{
    [Inject] GameModeManager gameModeManager;
    [Inject] ProgressPlayer player;

    public float volumeCount;

    public int difficult;
    public int planets;
    public int skinUnits;
    public Color colorPlayer;
    public bool isPaused = false;

    private void Start()
    {
        LoadVolume();
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

    public void SaveVolume(float count)
    {
        volumeCount = count;
        PlayerPrefs.SetFloat("VolumeCount", count);
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

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey("VolumeCount"))
        {
            volumeCount = PlayerPrefs.GetFloat("VolumeCount");
        }
        else volumeCount = 0.5f;
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
}

