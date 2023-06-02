using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    [SerializeField]
    private float _speed, _conveyerSpeed;
    [SerializeField]
    private Vector3 _direction;
    [SerializeField]
    private List<GameObject> _onBelt;

    private Material _material;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        for(int i = 0;  i < _onBelt.Count; i++)
        {
            _onBelt[i].GetComponent<Rigidbody>().AddForce(_speed * _direction);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _onBelt.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        _onBelt.Remove(collision.gameObject);
    }
}
