using UnityEngine;

public class ConsoleStateManager : MonoBehaviour
{
    private enum State
    {
        Closed,
        OpenSmall,
        OpenBig
    }

    private float animationDuration = 0.3f;
    private State currentState = State.Closed;
    private float timer;
    private float startY;
    private float targetY;
    private RectTransform rectTransform;

    // @FIXME: Starts with an animation, instead set it to inactive.
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startY = -rectTransform.rect.height;
        UpdateTargetPosition(); // redundant?
        timer = 0f; // redundant?
    }

    void Update()
    {
        // @TODO:
        // - Use 'Input System'.
        // - Also apply the console state logic.
        if (Input.GetKeyDown(KeyCode.A))
        {
            ToggleClosed();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleOpenSmall();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleOpenBig();
        }

        if (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float t = timer / animationDuration;
            float currentY = Mathf.Lerp(startY, targetY, t);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, currentY);
        }
    }

    public void ToggleOpenSmall()
    {
        if (currentState != State.OpenSmall)
        {
            SetState(State.OpenSmall);
        }
    }

    public void ToggleOpenBig()
    {
        if (currentState != State.OpenBig)
        {
            SetState(State.OpenBig);
        }
    }

    public void ToggleClosed()
    {
        if (currentState != State.Closed)
        {
            SetState(State.Closed);
        }
    }

    private void SetState(State newState)
    {
        currentState = newState;
        startY = rectTransform.anchoredPosition.y;
        UpdateTargetPosition();
        timer = 0f;
    }

    private void UpdateTargetPosition()
    {
        switch (currentState)
        {
            case State.Closed:
                targetY = rectTransform.rect.height;
                break;
            case State.OpenSmall:
                targetY = rectTransform.rect.height * 0.70f;
                break;
            case State.OpenBig:
                targetY = rectTransform.rect.height * 0.05f;
                break;
        }
    }
}
