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

    private int _day;

    private bool _isDay;

    public bool IsDay => _isDay;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        if (DataController.main.LoadFlag(DataController.DataTypeBool.startGame))
        {
            StartGame();
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

    private void StartGame()
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

        OnStartDay?.Invoke();
    }

    public void StartNight()
    {
        _isDay = false;

        DataController.main.SaveFlag(DataController.DataTypeBool.isDay, _isDay);

        Bootastrap.main.Player.Restart(_playerPosition.transform.position, _isDay);

        DataController.main.LoadInventory(_playerInventoryChess.Inventory);

        OnStartNight?.Invoke();
    }
}
