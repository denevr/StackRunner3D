using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _body;

    [SerializeField]
    private float moveSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _body.GetComponent<Animator>().applyRootMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
            SceneManager.LoadScene(1);
        if (transform.position.x <= -25)
            SceneManager.LoadScene(1);
    }

    void FixedUpdate()
    {
        _body.MovePosition(transform.position - Vector3.right * moveSpeed * Time.deltaTime);
    }
}
