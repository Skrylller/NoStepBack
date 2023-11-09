using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedObject : MonoBehaviour
{
    [SerializeField] private GameObject UITextPressToSleep;
    [SerializeField] private GameObject UITextCantSleep;
    [SerializeField] private float _timer = 2;

    public void BedAction()
    {
        if (DaysController.main.IsPatrool)
        {
            DaysController.main.StartNight();
        }
        else
        {
            UITextPressToSleep.SetActive(false);
            UITextCantSleep.SetActive(true);
            StartCoroutine(YouCantSleepCourotine());
        }
    }

    private IEnumerator YouCantSleepCourotine()
    {
        yield return new WaitForSeconds(_timer);
        UITextPressToSleep.SetActive(true);
        UITextCantSleep.SetActive(false);
    }
}
