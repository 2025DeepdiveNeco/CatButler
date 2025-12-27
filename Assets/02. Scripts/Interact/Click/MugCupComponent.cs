using UnityEngine;

public class MugCupComponent : BaseInteractComponent
{
    public float pushDistance = 2f;
    public float pushSpeed = 8f;
    public Transform player;

    void OnEnable()
    {
        OnInteractEnd += StartPush;
    }

    private void OnDisable()
    {
        OnInteractEnd -= StartPush;
    }

    void StartPush() => StartCoroutine(Push());
    System.Collections.IEnumerator Push()
    {
        Vector2 start = transform.position;

        Vector2 dir = ((Vector2)transform.position - (Vector2)player.transform.position).normalized;
        Vector2 end = start + dir * pushDistance;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * pushSpeed;
            transform.position = Vector2.Lerp(start, end, t);
            yield return null;
        }
    }
}
