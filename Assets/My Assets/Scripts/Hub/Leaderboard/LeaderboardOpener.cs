using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
internal class LeaderboardOpener : MonoBehaviour
{
    [SerializeField] private YandexLeaderboardFiller _yandexLeaderboardFiller;
    [SerializeField] private GameObject _leaderboardPanel;

    private Button _leaderboardButton;

    private void Awake() => _leaderboardButton = GetComponent<Button>();

    private void OnEnable() => _leaderboardButton.onClick.AddListener(OnLeaderboardButtonClick);

    private void OnDisable() => _leaderboardButton.onClick.RemoveListener(OnLeaderboardButtonClick);

    private void OnLeaderboardButtonClick() => OpenLeaderboard();

    private void OpenLeaderboard()
    {
        if (PlayerAccount.IsAuthorized == false)        
            PlayerAccount.Authorize();            

        if (PlayerAccount.IsAuthorized)        
            PlayerAccount.RequestPersonalProfileDataPermission();         

        if (PlayerAccount.IsAuthorized == false)
            return;

        _yandexLeaderboardFiller.Fill();
        _leaderboardPanel.SetActive(true);
    }
}