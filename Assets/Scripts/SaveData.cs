using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SaveData : MonoBehaviour
{
    [Inject] private ProgressPlayer player;

    public void SaveDataCampaign()
    { 
        player.SaveDataCampaign();

    }
}
