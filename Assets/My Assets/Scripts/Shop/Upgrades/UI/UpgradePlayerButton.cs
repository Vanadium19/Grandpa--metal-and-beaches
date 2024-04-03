using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerStats))]
internal class UpgradePlayerButton : MonoBehaviour
{
    private readonly string _max = "max";

    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Animator _buttonAnimator;
    [SerializeField] private Image _bar;
    [SerializeField] private AdvertisingButton _advertisingButton;

    private PlayerStats _playerStats;

    private void Awake() => _playerStats = GetComponent<PlayerStats>();

    private void OnEnable()
    {
        _upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);

        if (_upgradeButton.interactable)        
            _advertisingButton.Initialize(_playerStats.CurrentLevel != _playerStats.MaxLevel, _wallet, _playerStats.GetPrice());        
    }

    private void Start() => UpdateDisplay();

    private void OnDisable() => _upgradeButton.onClick.RemoveListener(OnUpgradeButtonClicked);

    private void OnUpgradeButtonClicked()
    {
        var price = _playerStats.GetPrice();

        if (_wallet.CanBuy(price))        
            Buy(price);        
        else
            _buttonAnimator.SetTrigger(AnimatorNames.NoMoney);        
    }

    private void Buy(float price)
    {
        _wallet.RemoveMoney(price);
        _buttonAnimator.SetTrigger(AnimatorNames.Buy);
        _playerStats.UpdateLevel();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (_playerStats.CurrentLevel == _playerStats.MaxLevel)
            OffButton();
        else
            _price.text = _playerStats.GetPrice().ToString();

        _bar.fillAmount = Convert.ToSingle(_playerStats.CurrentLevel) / _playerStats.MaxLevel;
    }

    private void OffButton()
    {
        _price.text = _max;
        _upgradeButton.interactable = false;
    }
}
