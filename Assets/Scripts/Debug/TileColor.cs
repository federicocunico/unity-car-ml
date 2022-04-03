using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColor : MonoBehaviour
{
    public Material ObjectOnMaterial;
    public Material ObjectOffMaterial;

    private Renderer _renderer;
    private List<Collider> _collidedObjects = new List<Collider>();

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_collidedObjects.Contains(collision.collider))
        {
            _collidedObjects.Add(collision.collider);
        }
        _renderer.material = ObjectOnMaterial;
    }

    private void OnCollisionExit(Collision collision)
    {
        _collidedObjects.Remove(collision.collider);
        if (_collidedObjects.Count < 1)
        {
            _renderer.material = ObjectOffMaterial;
        }
    }

}
