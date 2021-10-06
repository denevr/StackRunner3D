using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;
    public List<Sprite> shapes = new List<Sprite>();
    public Sprite x_sprite;
    public GameObject tile;
    public int n;
    public Camera mainCamera;
    private GameObject[,] tiles;
    private Vector3 cameraPos;

    void Start()
    {
        instance = GetComponent<GridManager>();
        cameraPos = mainCamera.transform.position;
        //Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(n, 0.385f, 0.385f);
    }

    void LateUpdate()
    {
        //if (Input.deviceOrientation == DeviceOrientation.Portrait)
        //{
        //    float currentAspect = (float)Screen.height / (float)Screen.width;
        //    Camera.main.orthographicSize = currentAspect * n / 5;
        //}
        //else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        //{
        //    float currentAspect = (float)Screen.width / (float)Screen.height;
        //    Camera.main.orthographicSize = currentAspect * n / 5;
        //}
        //else
        //{
        //    float currentAspect = (float)Screen.width / (float)Screen.height;
        //    Camera.main.orthographicSize = currentAspect * n / 7;
        //}
    }

    private void CreateBoard(int n, float offset_x, float offset_y)
    {
        tiles = new GameObject[n, n];
        float origin_x = transform.position.x;
        float origin_y = transform.position.y;

        for (int y = 0; y < n; y++)
        {
            for (int x = 0; x < n; x++)
            {
                GameObject newTile = Instantiate(tile, new Vector3(origin_x + (offset_x * x), origin_y + (offset_y * y), 0), tile.transform.rotation);
                tiles[y, x] = newTile;
                newTile.transform.parent = transform;
                Sprite newSprite = shapes[0];
                newTile.GetComponent<SpriteRenderer>().sprite = newSprite;
            }
        }

        cameraPos.x = tiles[n - 1, n - 1].gameObject.transform.position.x / 2;
        cameraPos.y = tiles[n - 1, n - 1].gameObject.transform.position.y / 2;
        mainCamera.transform.position = cameraPos;

        float currentAspect = (float)Screen.height / (float)Screen.width;
        Camera.main.orthographicSize = currentAspect * n / 5;
    }
}
