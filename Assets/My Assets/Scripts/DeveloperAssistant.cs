using Lean.Localization;
using UnityEngine;

public class DeveloperAssistant : MonoBehaviour
{
    [ContextMenu("AddLittleMoney")]
    public void AddLittleMoney()
    {
        PlayerPrefs.SetFloat(GameSaver.Money, PlayerPrefs.GetFloat(GameSaver.Money) + 55f);
        PlayerPrefs.Save();
    }

    [ContextMenu("Restart")]
    public void Restart()
    {
        GameSaver.RestartProgress();
    }

    [ContextMenu("AddMoney")]
    public void AddMoney()
    {
        PlayerPrefs.SetFloat(GameSaver.Money, 5000f);
        PlayerPrefs.Save();
    }

    [ContextMenu("SetSpeed")]
    public void SetSpeed()
    {
        PlayerPrefs.SetFloat(GameSaver.Speed, 15);
        PlayerPrefs.Save();
    }

    [ContextMenu("SetEnglish")]
    public void SetEnglish() => LeanLocalization.SetCurrentLanguageAll("English");

    [ContextMenu("SetTurkish")]
    public void SetTurkish() => LeanLocalization.SetCurrentLanguageAll("Turkish");

    [ContextMenu("SetRussian")]
    public void SetRussian() => LeanLocalization.SetCurrentLanguageAll("Russian");
}
