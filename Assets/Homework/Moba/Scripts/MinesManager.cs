using System.Collections.Generic;
using UnityEngine;

public class MinesManager : MonoBehaviour
{
    [SerializeField] List<MineCharacter> _mines;

    List<Controller> _minesControllers;

    [SerializeField] private bool _setupParameters;

    [SerializeField] private float _damage;

    [SerializeField] float _radius;
    [SerializeField] float _secondsToExplode;

    [SerializeField] private Spawner _explosionPrefab;

    private void Awake()
    {
        InitializeMines();

        _minesControllers = new List<Controller>();

        for (int i = 0; i < _mines.Count; i++)
        {
            Controller explosionController = new ExplosionSourceExplodeController(
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

    private void InitializeMines()
    {
        if (_setupParameters)
            foreach (var mine in _mines)
                mine.Initialize(_damage, _radius, _secondsToExplode);
    }
}