using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : Singleton<CameraController>
{
    private const float m_rotationSpeed = 500f;

    private float m_cameraRotationX = 0;
    private float m_cameraRotationY = 0;

    private bool m_isMovingCamera = false;

    private void Start()
    {
    }

    private void Update()
    {
        HandleMoveDetecting();
        HandleMoving();
    }

    private void HandleMoveDetecting()
    {
        if (!m_isMovingCamera && !EventSystem.current.IsPointerOverGameObject() && Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_isMovingCamera = true;
        }

        if (m_isMovingCamera && Input.GetKeyUp(KeyCode.Mouse0))
        {
            m_isMovingCamera = false;
        }
    }

    private void HandleMoving()
    {
        if (m_isMovingCamera && (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0))
        {
            float deltaX = Input.GetAxisRaw("Mouse Y");
            float deltaY = Input.GetAxisRaw("Mouse X");
            m_cameraRotationX -= deltaX * m_rotationSpeed * Time.deltaTime;
            m_cameraRotationY += deltaY * m_rotationSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            m_cameraRotationX -= m_rotationSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            m_cameraRotationX += m_rotationSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            m_cameraRotationY -= m_rotationSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_cameraRotationY += m_rotationSpeed * Time.deltaTime;
        }

        m_cameraRotationX = Mathf.Clamp(m_cameraRotationX, -45, 90);

        transform.eulerAngles = new Vector3(m_cameraRotationX, m_cameraRotationY, 0);
    }

    public void SetRotation(Vector3 pointPos)
    {
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 direction = Vector3.Normalize(pointPos - cameraPos);
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

        // Extract the Euler angles from the rotation
        Vector3 eulerAngles = rotation.eulerAngles;

        // Set the camera rotation angles
        m_cameraRotationX = eulerAngles.z;
        m_cameraRotationY = eulerAngles.y + ViewPointManager.Instance.m_currentViewPoint.m_rotation;
    }
}