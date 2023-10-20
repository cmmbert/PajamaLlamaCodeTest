using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingPlatform : MonoBehaviour
{
    public bool CanGrow = true;
    [Range(2,10)]
    public int CurrentNrOfTiles = 3;

    [Range(2, 10)]
    public int MaxNrOfTiles = 5;
    [Range(2, 10)]
    public int MinNrOfTiles = 3;

    [Range(0.001f, 10)]
    public float TileChangeSpeed = 2;
    private float _timeSinceLastTileChange = 500;

    [Range(-1,1)]
    public int Direction = 1;


    public GameObject TilesParent;

    [SerializeField]
    private GameObject _leftTile;
    [SerializeField]
    private GameObject _middleTile;
    [SerializeField]
    private GameObject _rightTile;

    private const float _tileSize = 0.4f;

    private BoxCollider2D _collider;
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        ChangeNrOfTiles(CurrentNrOfTiles);
    }


    void Update()
    {
        if(CanGrow)
            HandleTileChange();
    }

    public void HandleTileChange()
    {
        _timeSinceLastTileChange += Time.deltaTime;
        if (_timeSinceLastTileChange > 1 / TileChangeSpeed)
        {
            _timeSinceLastTileChange = 0;
            if(CurrentNrOfTiles <= MinNrOfTiles)
                Direction = Mathf.Abs(Direction);
            if(CurrentNrOfTiles >= MaxNrOfTiles)
                Direction = -Mathf.Abs(Direction);

            var newTileNr = CurrentNrOfTiles + Direction;
            ChangeNrOfTiles(newTileNr);
        }
    }

    public void ChangeNrOfTiles(int newNumber)
    {
        foreach (Transform child in TilesParent.transform)
        {
            Destroy(child.gameObject);
        }
        CurrentNrOfTiles = newNumber;
        var currentXOffset = 0.0f;
        for (int i = 0; i < CurrentNrOfTiles; i++)
        {
            GameObject tile = _middleTile;
            if(i == 0)
                tile = _leftTile;
            if (i == CurrentNrOfTiles-1)
                tile = _rightTile;
            var newTile = Instantiate(tile);
            newTile.transform.SetParent(TilesParent.transform);
            newTile.SetActive(true);
            newTile.transform.localPosition = new Vector3(currentXOffset, 0, 0);
            currentXOffset += _tileSize;
        }
        var totalSize = CurrentNrOfTiles * _tileSize;
        _collider.size = new Vector2(totalSize - 0.1f, _tileSize * 0.75f);
        _collider.offset = new Vector2(_collider.size.x / 2 - 0.15f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<KinematicObject>()
                .Bounce((transform.position - collision.transform.position).normalized); //Makes sure the player does not get stuck inside the platform
        }
    }


}



