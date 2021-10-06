using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private MovingCube cubePrefab;

    [SerializeField]
    private MoveDirection moveDirection;

    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCube()
    {
        if (MovingCube.LastCube.transform.position.x >= -24)
        {
            var cube = Instantiate(cubePrefab);

            if (MovingCube.LastCube != null && MovingCube.LastCube != GameObject.Find("StartCube"))
            {
                float x = MovingCube.LastCube.transform.position.x - cubePrefab.transform.localScale.x;
                float z = transform.position.z;
                cube.transform.position = new Vector3(x, MovingCube.LastCube.transform.position.y, z);
            }
            else
                cube.transform.position = transform.position;

            cube.MoveDirection = moveDirection;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    //}
}
