using UnityEngine;

public class Movement : MonoBehaviour
{
    private readonly string MouseX = "Mouse X";
    private readonly string MouseY = "Mouse Y";
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _limitRotationDown = -90f;
    [SerializeField] private float _limitRotationUp = 90f;
    [SerializeField] private Transform _camera;

    private float _xRotation = 0f;

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis(Horizontal), 0f, Input.GetAxis(Vertical));
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        float mouseY = Input.GetAxis(MouseY) * _rotationSpeed * Time.deltaTime;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, _limitRotationDown, _limitRotationUp);
        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(_rotationSpeed * Input.GetAxis(MouseX) * Time.deltaTime * Vector3.up);
    }
}
