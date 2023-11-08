using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DaysController : MonoBehaviour
{
    public static DaysController main;

    [SerializeField] private Transform _playerPosition;
    [SerializeField] private ModeSwitcher _modeSwitcher;
    [SerializeField] private InventoryObject _playerInventoryChess;

    public Action OnStartDay;
    public Action OnStartNight;

    [SerializeField] private int _day;

    [SerializeField] private bool _isDay;

    public bool IsDay => _isDay;

    [SerializeField] private List<GameObject> _activeDayObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> _activeNightObjects = new List<GameObject>();
    [SerializeField] private List<DaysUpdaterObject> _activeDayObjectsInterfaces = new List<DaysUpdaterObject>();
    [SerializeField] private List<DaysUpdaterObject> _activeNightObjectsInterfaces = new List<DaysUpdaterObject>();

    private void Awake()
    {
        main = this;
        foreach(GameObject day in _activeDayObjects)
        {
            _activeDayObjectsInterfaces.Add(day.GetComponent<DaysUpdaterObject>());
        }

        foreach (GameObject day in _activeNightObjects)
        {
            _activeNightObjectsInterfaces.Add(day.GetComponent<DaysUpdaterObject>());
        }
    }

    private void Start()
    {
        if (DataController.main.LoadFlag(DataController.DataTypeBool.startGame))
        {
            StartNewGame();
        }
        else
        {
            _day = DataController.main.LoadFlag(DataController.DataTypeInt.days);
            _isDay = DataController.main.LoadFlag(DataController.DataTypeBool.isDay);

            if (_isDay)
                StartDay();
            else
                StartNight();
        }
    }

    private void StartNewGame()
    {
        _day = 1;
        _isDay = true;

        DataController.main.SaveFlag(DataController.DataTypeInt.days, _day);
        DataController.main.SaveFlag(DataController.DataTypeBool.isDay, _isDay);

        StartDay();
    }

    public void NextDay()
    {
        _day++;
        StartDay();
    }

    private void StartDay()
    {
        _isDay = true;

        DataController.main.SaveFlag(DataController.DataTypeInt.days, _day);
        DataController.main.SaveFlag(DataController.DataTypeBool.isDay, _isDay);

        Bootastrap.main.Player.Restart(_playerPosition.transform.position, _isDay);

        _playerInventoryChess.Inventory.Clear();
        DataController.main.LoadInventory(PlayerInventory.Inventory);

        foreach (DaysUpdaterObject obj in _activeDayObjectsInterfaces)
        {
            obj.DayUpdate();
        }
        PullsController.main.ClearAll();

        _modeSwitcher.State = _isDay ? 0 : 1;

        OnStartDay?.Invoke();
    }

    public void StartNight()
    {
        _isDay = false;

        DataController.main.SaveFlag(DataController.DataTypeBool.isDay, _isDay);

        Bootastrap.main.Player.Restart(_playerPosition.transform.position, _isDay);

        PlayerInventory.Inventory.Clear();
        DataController.main.LoadInventory(_playerInventoryChess.Inventory);

        foreach (DaysUpdaterObject obj in _activeNightObjectsInterfaces)
        {
            obj.DayUpdate();
        }
        PullsController.main.ClearAll();

        _modeSwitcher.State = _isDay ? 0 : 1;

        OnStartNight?.Invoke();
    }
}
