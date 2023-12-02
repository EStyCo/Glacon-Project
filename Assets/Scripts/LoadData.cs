using UnityEngine;
using Zenject;

public class LoadData : MonoBehaviour
{
    [Inject] private GameModeManager gmManager;

    [Inject] private ProgressPlayer player;
    [Inject] private ProgressEnemy1 enemy1;
    [Inject] private ProgressEnemy2 enemy2;
    [Inject] private ProgressEnemy3 enemy3;

    public bool isSandbox;
    public bool isCampaign;

    void Start()
    {
        ResetProgressEnemy();

        if (isSandbox)
        {
            gmManager.currentState = GameModeManager.State.SandBox;
            player.LoadDataSandbox();
            return;
        }
        else if (isCampaign)
        {
            gmManager.currentState = GameModeManager.State.Campaign;
            player.LoadDataCampaign();
            return;
        }
    }

    public void ResetProgressEnemy()
    {
        enemy1.ResetProgress();
        enemy2.ResetProgress();
        enemy3.ResetProgress();
    }
}
