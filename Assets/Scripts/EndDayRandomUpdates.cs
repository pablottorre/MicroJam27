using UnityEngine;

public class EndDayRandomUpdates : MonoBehaviour
{
    [SerializeField] private GameObject[] _redUpgrades;
    [SerializeField] private GameObject[] _blueUpgrades;
    [SerializeField] private GameObject[] _yellowUpgrades;
    
    void Awake()
    {
        EventManager.SubscribeToEvent(EventNames._OnEndNewDay, SelectRandomUpgrades);
    }

    private void SelectRandomUpgrades(params object[] parameters)
    {
        foreach (var obj in _redUpgrades)
        {
            obj.SetActive(false);
        }
        
        _redUpgrades[Random.Range(0, _redUpgrades.Length)].SetActive(true);
        
        foreach (var obj in _blueUpgrades)
        {
            obj.SetActive(false);
        }
        
        _blueUpgrades[Random.Range(0, _blueUpgrades.Length)].SetActive(true);
        
        foreach (var obj in _yellowUpgrades)
        {
            obj.SetActive(false);
        }
        
        _yellowUpgrades[Random.Range(0, _yellowUpgrades.Length)].SetActive(true);
    }
}
