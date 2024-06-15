using UnityEngine;

public class ZoomInOut : MonoBehaviour
{
     Camera camera;
    int zoomSpeed = 300;
    ChageMap chageMap;
    private void Awake()
    {
        camera = GetComponent<Camera>();
    }
    void Update()
    {
        chageMap = FindObjectOfType<ChageMap>();
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        // Orthographic 카메라의 크기를 조정합니다.
        Camera.main.orthographicSize -= scrollWheel * zoomSpeed * Time.deltaTime * 2;

        // 크기를 최소값과 최대값 사이로 제한합니다.
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2f, 15.2f);
        if (chageMap.map == ChageMap.mapType.Home)
        {
            moveCamera();
        }

    }

    void moveCamera()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + Time.deltaTime * 4, transform.position.y, -1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - Time.deltaTime * 4, transform.position.y, -1);
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 47.37f, 59.6f), transform.position.y, transform.position.z);
    }
}
