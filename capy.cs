using UnityEngine;

public class capy : MonoBehaviour
{
    

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _force;
    [SerializeField] float _yBound;

    

    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _rigidbody.position.y < _yBound)
        {
            Flap();
        }
    }
    
    private void Flap()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(Vector2.up * _force);
    }
}
