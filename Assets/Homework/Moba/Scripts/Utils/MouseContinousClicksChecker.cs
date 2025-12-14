using UnityEngine;

class MouseContinousClicksChecker
{
    private float _timeBetweenMouseButtonClicks = 0;

    public bool IsMouseButtonPressEverySecond(float periodBetweenButtonPressed)
    {
        _timeBetweenMouseButtonClicks += Time.deltaTime;

        if (Input.GetMouseButton(0) && _timeBetweenMouseButtonClicks >= periodBetweenButtonPressed)
        {
            _timeBetweenMouseButtonClicks = 0;
            return true;
        }
        else if (Input.GetMouseButtonDown(0)) //когда мы быстро тыкаем по мышке, вместа зажатия
        {
            return true;
        }

        return false;
    }
}