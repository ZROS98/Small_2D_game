using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [HideInInspector] public Transform player;
    public bool Stopped => stopped;

    [field: SerializeField] private float Speed { get; set; } = 2f;
    [field: SerializeField] private float FleeDistance { get; set; } = 2f;
    [field: SerializeField] private float Bounds { get; set; } = 0.5f;
    [field: SerializeField] private float UpdateInterval { get; set; } = 0.02f;

    [field: SerializeField] private SpriteRenderer spriteRenderer { get; set; }

    private Vector2 screenBounds;
    private bool stopped = false;

    private void Start()
    {
        Camera cam = Camera.main;
        Vector3 screen = new Vector3(Screen.width, Screen.height, cam.transform.position.z);
        Vector3 bounds = cam.ScreenToWorldPoint(screen);
        screenBounds = new Vector2(bounds.x - Bounds, bounds.y - Bounds);

        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(UpdateInterval);

        while (true)
        {
            if (!stopped && player != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);
                if (distanceToPlayer <= FleeDistance)
                {
                    Vector3 dir = (transform.position - player.position).normalized;
                    transform.position += dir * Speed * Time.deltaTime;

                    transform.position = new Vector3(
                        Mathf.Clamp(transform.position.x, -screenBounds.x, screenBounds.x),
                        Mathf.Clamp(transform.position.y, -screenBounds.y, screenBounds.y),
                        transform.position.z
                    );
                }
            }

            yield return wait;
        }
    }

    public void Stop()
    {
        stopped = true;
        if (spriteRenderer != null)
            spriteRenderer.color = Color.black;
    }
}