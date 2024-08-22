using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private Button speedControllerButton;
    private GameSpeedController gameSpeedController;

    private void Start()
    {
        gameSpeedController = new GameSpeedController();

        if (speedControllerButton != null)
        {
            speedControllerButton.onClick.AddListener(gameSpeedController.ChangeSpeed);
        }
    }

    private void OnDestroy()
    {
        if (speedControllerButton != null)
        {
            speedControllerButton.onClick.RemoveListener(gameSpeedController.ChangeSpeed);
        }
    }
}
