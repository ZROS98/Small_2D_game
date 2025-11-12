using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [field: SerializeField] private Button PlayButton { get; set; }
    [field: SerializeField] private Button ExitButton { get; set; }

    [field: SerializeField] private string GameplayScene { get; set; } = "Gameplay";

    private void OnEnable()
    {
        if (PlayButton != null)
            PlayButton.onClick.AddListener(LoadGameplay);

        if (ExitButton != null)
            ExitButton.onClick.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        if (PlayButton != null)
            PlayButton.onClick.RemoveListener(LoadGameplay);

        if (ExitButton != null)
            ExitButton.onClick.RemoveListener(QuitGame);
    }

    private async void LoadGameplay()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GameplayScene, LoadSceneMode.Single);

        while (!asyncLoad.isDone)
        {
            await System.Threading.Tasks.Task.Yield();
        }

        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}