using UnityEngine;
using Zenject;

public class StateInitialize : MonoBehaviour
{
    [Inject] private GameModeManager gmManager;

    [SerializeField] private GameObject sandboxGameObject;
    [SerializeField] private GameObject campaignGameObject;

    public GameState GetState()
    {
        switch (gmManager.currentState)
        {
            case GameModeManager.State.Campaign:
                return OnCampaignState();

            case GameModeManager.State.SandBox:
                return OnSandBoxState();

            default: return null;
        }
    }

    private GameState OnSandBoxState()
    {
        sandboxGameObject.SetActive(true);
        return sandboxGameObject.GetComponent<SandBox>();
    }

    private GameState OnCampaignState()
    {
        campaignGameObject.SetActive(true);
        return campaignGameObject.GetComponent<Campaign>();
    }
}
