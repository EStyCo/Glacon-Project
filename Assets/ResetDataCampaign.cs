using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResetDataCampaign : MonoBehaviour
{
    [Inject] private ProgressPlayer player;

    public void ResetData()
    {
        player.ResetDataCampaign();
    }
}
