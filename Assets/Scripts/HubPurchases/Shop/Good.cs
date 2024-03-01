using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GoodRenderer))]
internal class Good : MonoBehaviour
{
    private readonly string _htmlColor = "#d9CECE";
    private readonly int _defaultIndex = 1;

    [SerializeField] private Image _soldGoodPrefab;
    [SerializeField] private GoodInfo _goodInfo;
    [SerializeField] private Button _sellButton;
    [SerializeField] private AdvertisingButton _advertisingButton;

    private GoodRenderer _goodRenderer;
    private Transform _canvas;
    private Wallet _wallet;

    public bool IsSold => Convert.ToBoolean(PlayerPrefs.GetInt(_goodInfo.Name, 0));

    private void Awake()
    {
        _goodRenderer = GetComponent<GoodRenderer>();
    }

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnSellButtonClicked);
        _advertisingButton.Initialize(!IsSold, _wallet, _goodInfo.Price);
    }

    private void Start()
    {
        _goodRenderer.Render(_goodInfo);

        if (IsSold)
            _sellButton.interactable = false;
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnSellButtonClicked);
    }

    public void Initialize(Wallet wallet, Transform canvas)
    {
        _wallet = wallet;
        _canvas = canvas;
    }

    public void Create()
    {
        Image soldGood = Instantiate(_soldGoodPrefab, _canvas);
        soldGood.name = _goodInfo.Name;
        Install(soldGood.rectTransform);
        TuneImage(soldGood);
    }

    private void Install(RectTransform rectTransform)
    {
        rectTransform.SetSiblingIndex(_defaultIndex);
        rectTransform.sizeDelta = _goodInfo.Size;
        rectTransform.anchorMin = _goodInfo.MinAnchors;
        rectTransform.anchorMax = _goodInfo.MaxAnchors;
        rectTransform.localPosition = _goodInfo.Position;
    }

    private void TuneImage(Image soldGood)
    {
        soldGood.sprite = _goodInfo.Icon;
        ColorUtility.TryParseHtmlString(_htmlColor, out Color color);
        soldGood.color = color;
    }

    private void OnSellButtonClicked()
    {
        if (_wallet.TryBuy(_goodInfo.Price))
            Buy();
    }

    private void Buy()
    {
        Create();
        _sellButton.interactable = false;
        PlayerPrefs.SetInt(_goodInfo.Name, Convert.ToInt16(true));
        PlayerPrefs.Save();
    }
}
