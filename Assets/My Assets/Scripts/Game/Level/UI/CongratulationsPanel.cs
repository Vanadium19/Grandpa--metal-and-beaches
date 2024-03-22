using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CongratulationsPanel : MonoBehaviour
{
    private readonly float _closePanelDelay = 2f;
    private readonly float _additionValueDelay = 0.01f;

    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;

    public bool IsFinished { get; private set; }

    public void Activate(float targetValue)
    {
        gameObject.SetActive(true);
        StartCoroutine(Congratulate(targetValue));
    }

    private IEnumerator Congratulate(float targetValue)
    {
        var delay = new WaitForSeconds(_additionValueDelay);
        float value = 0;

        while (value <= targetValue)
        {
            _image.fillAmount = value / targetValue;
            _text.text = $"{value}/{targetValue}";
            value++;

            yield return delay;
        }

        yield return new WaitForSeconds(_closePanelDelay);
        IsFinished = true;        
    }
}