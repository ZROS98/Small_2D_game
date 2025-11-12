using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public EnemyController[] EnemiesList;

    [field: SerializeField] private float Speed { get; set; } = 5f;
    [field: SerializeField] private float StopDistance { get; set; } = 0.5f;
    [field: SerializeField] private float Bounds { get; set; } = 0.5f;

    private Vector2 screenBounds;

    private void Start()
    {
        Camera cam = Camera.main;
        Vector3 screen = new Vector3(Screen.width, Screen.height, cam.transform.position.z);
        Vector3 bounds = cam.ScreenToWorldPoint(screen);
        screenBounds = new Vector2(bounds.x - Bounds, bounds.y - Bounds);

        StartCoroutine(CheckEnemiesCollisionCoroutine());
    }

    private void FixedUpdate()
    {
        HandleKeyboardMovement();
        ClampPosition();
    }

    private void HandleKeyboardMovement()
    {
        Vector2 moveInput = Vector2.zero;

        if (UnityEngine.InputSystem.Keyboard.current != null)
        {
            var keyboard = UnityEngine.InputSystem.Keyboard.current;
            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) moveInput.y += 1;
            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) moveInput.y -= 1;
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) moveInput.x -= 1;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) moveInput.x += 1;
        }

        transform.position += (Vector3)(moveInput.normalized * Speed * Time.deltaTime);
    }
    
    private void ClampPosition()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -screenBounds.x, screenBounds.x),
            Mathf.Clamp(transform.position.y, -screenBounds.y, screenBounds.y),
            transform.position.z
        );
    }

    private IEnumerator CheckEnemiesCollisionCoroutine()
    {
        var wait = new WaitForSeconds(0.05f);
        while (true)
        {
            foreach (EnemyController enemy in EnemiesList)
            {
                if (!enemy.gameObject.activeSelf || enemy.Stopped) continue;

                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance <= StopDistance)
                {
                    enemy.Stop();
                }
            }

            yield return wait;
        }
    }
}