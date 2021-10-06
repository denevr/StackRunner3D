using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnCubeSpawned = delegate { };
    public Camera mainCamera;
    private CubeSpawner[] spawners;
    private CubeSpawner currentSpawner;
    private int spawnerIndex;

    private void Awake()
    {
        spawners = FindObjectsOfType<CubeSpawner>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (MovingCube.CurrentCube != null)
                MovingCube.CurrentCube.Stop();

            spawnerIndex = spawnerIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnerIndex];
            //currentSpawner = spawners[0];

            currentSpawner.SpawnCube();
            OnCubeSpawned();
        }

        mainCamera.transform.position -= transform.right * Time.deltaTime * 0.5f;
    }
}
