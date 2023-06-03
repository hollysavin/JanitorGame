using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Vector3 _direction;

    private Rigidbody _body;
    private bool isRunning;

    private void Awake()
    {
        GameManager.OnGameStateChange += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.IntensityLow && isRunning == false)
        {
            isRunning = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isRunning)
        {
            Vector3 pos = _body.position;
            _body.position += _direction * _speed * Time.deltaTime;
            _body.MovePosition(pos);
        }
    }

}
