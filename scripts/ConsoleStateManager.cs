using UnityEngine;

// @FIXME: Incorrect behaviour on startup: Shouldn't animate the initial closed state.
public class ConsoleStateManager : MonoBehaviour
{
    public enum ConsoleState
    {
        Closed,
        OpenSmall,
        OpenBig
    }

    public class Console
    {
        public bool IsOpen { get; set; }
        public ConsoleState State { get; set; }
    }

    private Console console = new Console 
    {
        IsOpen = false,
        State = ConsoleState.Closed,
    };

    private float animationDuration = 0.3f;
    private float timer;
    private float startY;
    private float targetY;
    private RectTransform rectTransform;

    private ConsoleState currentConsoleState;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startY = -rectTransform.rect.height;
        UpdateTargetPosition();
        timer = 0f;
    }

    void Update()
    {
        checkInput();

        if (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float t = timer / animationDuration;
            float currentY = Mathf.Lerp(startY, targetY, t);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, currentY);
        }
    }

    // @TODO: Set up Input System.
    public void checkInput() {
        if (Input.GetKeyDown(KeyCode.J)) {
            switch (currentConsoleState) {
                case ConsoleState.Closed:
                    ToggleOpenSmall();
                    break;
                case ConsoleState.OpenSmall:
                    ToggleClosed();
                    break;
                case ConsoleState.OpenBig:
                    ToggleClosed();
                    break;
            }
        } else if (Input.GetKeyDown(KeyCode.K)) {
            switch (currentConsoleState) {
                case ConsoleState.Closed:
                    ToggleOpenBig();
                    break;
                case ConsoleState.OpenSmall:
                    ToggleOpenBig();
                    break;
                case ConsoleState.OpenBig:
                    ToggleOpenSmall();
                    break;
            }
        }
    }

    private void SetConsoleState(ConsoleState newState)
    {
        console.State = newState;
        currentConsoleState = newState;
        startY = rectTransform.anchoredPosition.y;
        UpdateTargetPosition();
        timer = 0f;
    }

    public void ToggleOpenSmall()
    {
        if (currentConsoleState != ConsoleState.OpenSmall)
        {
            SetConsoleState(ConsoleState.OpenSmall);
        }
    }

    public void ToggleOpenBig()
    {
        if (currentConsoleState != ConsoleState.OpenBig)
        {
            SetConsoleState(ConsoleState.OpenBig);
        }
    }

    public void ToggleClosed()
    {
        if (currentConsoleState != ConsoleState.Closed)
        {
            SetConsoleState(ConsoleState.Closed);
        }
    }

    private void UpdateTargetPosition()
    {
        switch (console.State)
        {
            case ConsoleState.Closed:
                targetY = rectTransform.rect.height;
                break;
            case ConsoleState.OpenSmall:
                targetY = rectTransform.rect.height * 0.70f;
                break;
            case ConsoleState.OpenBig:
                targetY = rectTransform.rect.height * 0.03f;
                break;
        }
    }
}
