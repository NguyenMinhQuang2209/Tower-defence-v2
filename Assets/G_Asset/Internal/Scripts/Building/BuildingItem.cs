using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BuildingItem : MonoBehaviour
{
    [SerializeField] private bool isWalkable = false;
    [SerializeField] private bool useRequiredMask = false;
    [SerializeField] private LayerMask requiredMask;
    [SerializeField] private LayerMask colliderMask;
    private List<Collider2D> colliders = new();
    private List<Collider2D> requires = new();
    private bool isBuilding = false;
    [SerializeField] private SpriteRenderer spriteRender;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBuilding) { return; }
        int layer = collision.gameObject.layer;
        if (((1 << layer) & requiredMask) != 0)
        {
            requires.Add(collision);
        }

        if (((1 << layer) & colliderMask) != 0)
        {
            colliders.Add(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isBuilding) { return; }
        int layer = collision.gameObject.layer;
        if (((1 << layer) & requiredMask) != 0)
        {
            requires.Remove(collision);
        }

        if (((1 << layer) & colliderMask) != 0)
        {
            colliders.Remove(collision);
        }
    }
    public void BuildItem()
    {
        colliders?.Clear();
        requires?.Clear();
        isBuilding = true;
        GetComponent<Collider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
    public bool CanBuilding()
    {
        return colliders.Count == 0 && (!useRequiredMask || requires.Count > 0);
    }
    public bool IsWalkable()
    {
        return isWalkable;
    }
    public SpriteRenderer GetSpriteRender()
    {
        if (spriteRender == null)
        {
            spriteRender = GetComponent<SpriteRenderer>();
        }
        return spriteRender;
    }
}
