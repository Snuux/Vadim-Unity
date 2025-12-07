using System.Collections.Generic;
using UnityEngine;

public class MinesManager : MonoBehaviour
{
    [SerializeField] List<MineCharacter> _mines;

    List<Controller> _minesControllers;

    private void Awake()
    {
        _minesControllers = new List<Controller>();

        for (int i = 0; i < _mines.Count; i++)
        {
            Controller explosionController = new ExplosionController(
                _mines[i], 
                _mines[i].Radius, 
                _mines[i].SecondsToExplode);

            _minesControllers.Add(explosionController);
            explosionController.Enable();
        }
    }

    private void Update()
    {
        for (int i = 0; i < _minesControllers.Count; i++)
        {
            if (_mines[i].IsExploded)
                _minesControllers[i].Disable();

            _minesControllers[i].Update(Time.deltaTime);
        }
    }
}