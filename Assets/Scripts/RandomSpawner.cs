using UnityEngine;
using System;

using System.Collections.Generic;

public class RandomSpawner : MonoBehaviour
{
    public GameObject player, fire;
    public GameObject prefabBois, prefabTree;

    public GameObject prefabTent;

    public GameObject prefabMobBear, prefabMobOgre;



    public UnityEngine.Tilemaps.Tilemap tileMap;
    public UnityEngine.Tilemaps.TileBase tileSprite;

    private List<Vector2> treePositionList;

    void Start ()
    {
        this.treePositionList = new List<Vector2>();

        GameObject prefabTentInstance = Instantiate(prefabTent,
            new Vector3(UnityEngine.Random.Range(-50, 50), UnityEngine.Random.Range(-50, 50), 0),
            Quaternion.identity);
        prefabTentInstance.GetComponent<Tent>().player = player;

        GameObject prefabMobOgreInstance = Instantiate(prefabMobOgre,
            new Vector3(UnityEngine.Random.Range(-500, 500), UnityEngine.Random.Range(-500, 500), 0),
            Quaternion.identity);
        prefabMobOgreInstance.GetComponent<Ogres>().player = player;
        prefabMobOgreInstance.GetComponent<Ogres>().fire = fire;

        for (int i = 0; i < UnityEngine.Random.Range(4, 10); i++)
        {
            GameObject prefabMobBearInstance = Instantiate(prefabMobBear,
                new Vector3(UnityEngine.Random.Range(-500, 500), UnityEngine.Random.Range(-500, 500), 0),
                Quaternion.identity);
            prefabMobBearInstance.GetComponent<Ours>().player = player;
            prefabMobBearInstance.GetComponent<Ours>().tent = prefabTentInstance;
        }
    }

    void Update ()
    {
        float playerX = player.gameObject.transform.position.x;
        float playerY = player.gameObject.transform.position.y;

        float ratio = 1.5f;

        for (int xIterator = -50; xIterator < 50; xIterator++)
        {
            for (int yIterator = -50; yIterator < 50; yIterator++)
            {
                float objectX = playerX + xIterator;
                float objectY = playerY + yIterator;
                
                if (tileMap.GetTile(new Vector3Int((int)objectX, (int)objectY, 0)) == null)
                {
                    tileMap.SetTile(new Vector3Int((int)objectX, (int)objectY, 0), tileSprite);
                }

                Vector2 treePosition = new Vector2((int)objectX, (int)objectY);

                if (Mathf.PerlinNoise(objectX * ratio, objectY * ratio) > 0.95 &&
                    treePositionList.FindIndex(pos => ( pos.x == treePosition.x && pos.y == treePosition.y ) ) == -1 &&
                    Math.Abs(objectX) > 4 && Math.Abs(objectY) > 4)
                {
                    treePositionList.Add(treePosition);

                    float perlinNoise = Mathf.PerlinNoise(xIterator * ratio, yIterator * ratio);
                    float p = UnityEngine.Random.value;
                    GameObject g = Instantiate(p > 0.75 ? prefabTree : prefabBois, 
                        new Vector3(objectX, objectY, 0), 
                        p > 0.75 ? Quaternion.identity : Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
                    g.GetComponent<SpriteRenderer>().flipX = p > 0.80;
                    if (p > 0.75) g.GetComponent<TreeScript>().player = player.GetComponent<PlayerScript>();
                }
            }
        }
    }

}
