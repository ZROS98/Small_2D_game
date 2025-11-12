using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EscapeHandler : MonoBehaviour
{
    [field: SerializeField] private string MainMenuScene { get; set; } = "MainMenu";

    private InputAction escapeAction;

    private string EscBinding = "<Keyboard>/escape";

    private void Awake()
    {
        escapeAction = new InputAction(type: InputActionType.Button, binding: EscBinding);
        escapeAction.performed += ctx => LoadMenu();
    }

    private void OnEnable()
    {
        escapeAction.Enable();
    }

    private void OnDisable()
    {
        escapeAction.Disable();
    }

    private async void LoadMenu()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(MainMenuScene, LoadSceneMode.Single);

        while (!asyncLoad.isDone)
        {
            await System.Threading.Tasks.Task.Yield();
        }

        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
}