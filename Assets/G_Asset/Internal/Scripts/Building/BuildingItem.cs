using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BuildingItem : MonoBehaviour
{
    [SerializeField] protected bool isWalkable = false;
    [SerializeField] protected bool useRequiredMask = false;
    [SerializeField] protected LayerMask requiredMask;
    [SerializeField] protected LayerMask colliderMask;
    protected List<Collider2D> colliders = new();
    protected List<Collider2D> requires = new();
    protected bool isBuilding = false;
    [SerializeField] protected SpriteRenderer spriteRender;

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
    public virtual void BuildItemInit(bool isTrigger = true)
    {
        colliders?.Clear();
        requires?.Clear();
        isBuilding = true;
        GetComponent<Collider2D>().isTrigger = isTrigger;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<BuildingItem>().enabled = false;
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
