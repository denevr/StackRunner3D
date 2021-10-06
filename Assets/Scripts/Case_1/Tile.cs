using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
	private SpriteRenderer render;
	private BoxCollider2D collider;
	private Vector2[] unitVectors = new Vector2[] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

	void Awake()
	{
		render = GetComponent<SpriteRenderer>();
		collider = GetComponent<BoxCollider2D>();
		Physics2D.queriesStartInColliders = false; //if raycast is hitting itself, fix it
	}

	void Start()
	{
	}

	void Update()
	{
		if (render.sprite == GridManager.instance.x_sprite)
			StartCoroutine(CheckAdjacentAndDestroy());	
	}

    void OnMouseDown()
	{
        if (render.sprite == null || render.sprite == GridManager.instance.x_sprite)
            return;
        else
            render.sprite = GridManager.instance.x_sprite;
	}

	IEnumerator CheckAdjacentAndDestroy()
    {
        List<GameObject> matchingTiles = FindAdjacentTiles();
        yield return new WaitForSeconds(0.001f);
        ClearMatches(matchingTiles);
    }

    private void ClearMatches(List<GameObject> matchingTiles)
    {
        if (matchingTiles.Count >= 2)
        {
            for (int i = 0; i < matchingTiles.Count; i++)
            {
                matchingTiles[i].GetComponent<SpriteRenderer>().sprite = GridManager.instance.shapes[0];
            }
            render.sprite = GridManager.instance.shapes[0];
            matchingTiles.Clear();
        }
    }

    private List<GameObject> FindAdjacentTiles()
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        for (int i = 0; i < GetAllAdjacentTiles().Count; i++)
        {
            if (GetAllAdjacentTiles()[i] != null && GetAllAdjacentTiles()[i].GetComponent<SpriteRenderer>().sprite == GridManager.instance.x_sprite)
            {
                matchingTiles.Add(GetAllAdjacentTiles()[i].gameObject);
            }
        }
        return matchingTiles;
    }

    private GameObject GetAdjacent(Vector2 direction)
    {
        collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        if (hit.collider != null)
        {
            collider.enabled = true;
            return hit.collider.gameObject;
        }
        else
        {
            collider.enabled = true;
            return null;
        }
    }

    private List<GameObject> GetAllAdjacentTiles()
    {
        List<GameObject> adjacentTiles = new List<GameObject>();
        for (int i = 0; i < unitVectors.Length; i++)
        {
            adjacentTiles.Add(GetAdjacent(unitVectors[i]));
        }
        return adjacentTiles;
    }

    //public void FindMatchesAndEliminate()
    //   {
    //	List<GameObject> matchingTiles = new List<GameObject>();
    //	for (int i = 0; i < unitVectors.Length; i++)
    //	{
    //           RaycastHit2D hit = Physics2D.Raycast(transform.position, unitVectors[i]);
    //		while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == GridManager.instance.x_sprite)
    //		{
    //			matchingTiles.Add(hit.collider.gameObject);
    //			hit = Physics2D.Raycast(hit.collider.transform.position, unitVectors[i]);
    //		}
    //	}
    //	if (matchingTiles.Count >= 2)
    //	{
    //		for (int i = 0; i < matchingTiles.Count; i++)
    //           {
    //			matchingTiles[i].gameObject.GetComponent<SpriteRenderer>().sprite = GridManager.instance.shapes[0];
    //		}
    //		render.sprite = GridManager.instance.shapes[0];
    //	}
    //}
}

