using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    private CharacterController _controller;
    private Vector3 _velocity;
    private float _groundDistance = 0.4f;
    private float _gravity = -9.81f;
    private float _attractionForce = -150f;
    private bool _isGrounded;


    public void Initialize()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Gravity();
    }



    private void Gravity()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = _attractionForce;
        }

        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

}
