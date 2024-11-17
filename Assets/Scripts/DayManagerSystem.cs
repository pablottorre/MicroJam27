using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayManagerSystem : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawners;

    [SerializeField] private int amountToSpawn;
    [SerializeField] private int numberDay;

    [Header("UI")]
    [SerializeField] private GameObject startOfGamePanel;
    [SerializeField] private CanvasGroup endDayPanel;
    [SerializeField] private float timerPanel;
    [SerializeField] private TMP_Text endDaysText;

    [SerializeField] private CanvasGroup endGamePanel;
    [SerializeField] private TMP_Text endGameDaysText;


    private void Awake()
    {
        EventManager.SubscribeToEvent(EventNames._OnStartNewDay, StartNewDay);
        EventManager.SubscribeToEvent(EventNames._OnEndNewDay, EndTheDay);
        EventManager.SubscribeToEvent(EventNames._OnEndOfGame, EndOfGame);
    }

    public void StartNewDay(params object[] parameters)
    {
        enemySpawners.StartSpawningWalking(amountToSpawn);
    } 

    public void StartOfTheGame()
    {
        startOfGamePanel.SetActive(false);
        LeanTween.alphaCanvas(endDayPanel, 0, timerPanel);
        endDayPanel.blocksRaycasts = false;
        endDayPanel.interactable = false;
        EventManager.TriggerEvent(EventNames._OnStartNewDay);
    }

    public void ButtonStartNewDay()
    {
        Debug.Log(45545);
        LeanTween.alphaCanvas(endDayPanel, 0, timerPanel);
        endDayPanel.blocksRaycasts = false;
        endDayPanel.interactable = false;
        EventManager.TriggerEvent(EventNames._OnStartNewDay);
    }
    
    public void EndTheDay(params object[] parameters)
    {
        endDaysText.text = numberDay.ToString();
        LeanTween.alphaCanvas(endDayPanel,1, timerPanel);
        endDayPanel.blocksRaycasts = true;
        endDayPanel.interactable = true;
        amountToSpawn++;
        numberDay++;
    }
    
    public void EndOfGame(params object[] parameters)
    {
        endGameDaysText.text = numberDay.ToString();
        LeanTween.alphaCanvas(endGamePanel, 1, timerPanel);
        endGamePanel.blocksRaycasts = true;
        endGamePanel.interactable = true;
    }
}
