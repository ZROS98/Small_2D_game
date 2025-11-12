using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [field: SerializeField] private GameObject SettingsPanel { get; set; }
    [field: SerializeField] private Button SettingsButton { get; set; }
    [field: SerializeField] private Button BackButton { get; set; }

    private InputAction toggleAction;

    private string EscBinding = "<Keyboard>/escape";

    private void Awake()
    {
        toggleAction = new InputAction(type: InputActionType.Button, binding: EscBinding);
        toggleAction.performed += _ => HandleSettingsPanel(closeOnly: true);

        if (SettingsPanel != null && SettingsPanel.activeSelf)
            SettingsPanel.SetActive(false);
    }

    private void OnEnable()
    {
        toggleAction.Enable();

        if (SettingsButton != null)
            SettingsButton.onClick.AddListener(() => HandleSettingsPanel());

        if (BackButton != null)
            BackButton.onClick.AddListener(() => HandleSettingsPanel(closeOnly: true));
    }

    private void OnDisable()
    {
        toggleAction.Disable();

        if (SettingsButton != null)
            SettingsButton.onClick.RemoveListener(() => HandleSettingsPanel());

        if (BackButton != null)
            BackButton.onClick.RemoveListener(() => HandleSettingsPanel(closeOnly: true));
    }

    private void HandleSettingsPanel(bool closeOnly = false)
    {
        if (SettingsPanel == null) return;

        if (closeOnly)
        {
            if (SettingsPanel.activeSelf)
                SettingsPanel.SetActive(false);
        }
        else
        {
            SettingsPanel.SetActive(!SettingsPanel.activeSelf);
        }
    }
}