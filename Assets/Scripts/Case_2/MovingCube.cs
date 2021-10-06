using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }
    public MoveDirection MoveDirection { get; set; }

    [SerializeField]    
    private float moveSpeed = 0.5f;
    public float pitchAmount;

    void OnEnable()
    {
        if (LastCube == null)
        {
            LastCube = GameObject.Find("StartCube").GetComponent<MovingCube>();
        }
        CurrentCube = this;
        GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }

    void Start()
    {
    }

    void Update()
    {
        if (MoveDirection == MoveDirection.posZ)
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        else
            transform.position -= transform.forward * Time.deltaTime * moveSpeed;
    }

    internal void Stop()
    {
        moveSpeed = 0;
        float hangover = GetHangover();

        if (Mathf.Abs(hangover) >= LastCube.transform.localScale.z)
        {
            LastCube = null;
            CurrentCube = null;
            SceneManager.LoadScene(1);
        }

        if (Mathf.Abs(hangover) <= 0.1f)
        {
            transform.position = new Vector3(LastCube.transform.position.x - transform.localScale.x, LastCube.transform.position.y, LastCube.transform.position.z);
            SoundManager.instance.PlayMatchingSound(SoundManager.instance.pitchAmount);
            SoundManager.instance.pitchAmount += 0.1f;
        }

        else
        {
            float direction = hangover > 0 ? 1f : -1f;
            SplitCubeOnZ(hangover, direction);
            SoundManager.instance.pitchAmount = 1f;
            SoundManager.instance.PlayFallingSound(SoundManager.instance.pitchAmount);
        }

        LastCube = this;
    }

    private float GetHangover()
    {
        return transform.position.z - LastCube.transform.position.z;
    }

    private void SplitCubeOnZ(float hangover, float direction)
    {

        float newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;
        
        float newZPosition = LastCube.transform.position.z + (hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2f * direction;

        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition); //check
        
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(cube.gameObject, 1f);
    }
}
