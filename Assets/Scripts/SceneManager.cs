using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        SoundManager.Instance.GoNoise();
    }
}