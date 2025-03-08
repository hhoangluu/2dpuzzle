using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erase : MonoBehaviour
{
    private SpriteRenderer spriteRend;
    private Collider2D targetCollider;
    private RaycastHit2D hit;
    private Texture2D tex;
    private Color zeroAlpha = new Color(0,0,0,0);

    [SerializeField]
    GameObject target;
    [SerializeField]
    private int erSize = 30;

    private bool erasing = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = target.GetComponent<SpriteRenderer>();
        targetCollider = target.GetComponent<Collider2D>();
    }

    void Update()
    {
      
        if (Input.GetMouseButton(0) && erasing)
        {
            UpdateTexture();
        }
    }

    public void UpdateTexture()
    {
      
        tex = CopyTexture(spriteRend.sprite.texture);
        string tempName = spriteRend.sprite.name;
        spriteRend.sprite = Sprite.Create(tex, spriteRend.sprite.rect, new Vector2(0.5f, 0.5f));
        spriteRend.sprite.name = tempName;
    }

    public Texture2D CopyTexture(Texture2D copiedTexture)
    {
        float dX, dY;
        Texture2D newTex = new Texture2D(copiedTexture.width, copiedTexture.height);
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newTex.filterMode = FilterMode.Bilinear;
        newTex.wrapMode = TextureWrapMode.Clamp;
        int mX = (int)((mousePos.x - targetCollider.bounds.min.x) * (copiedTexture.width / targetCollider.bounds.size.x));
        int mY = (int)((mousePos.y - targetCollider.bounds.min.y) * (copiedTexture.height / targetCollider.bounds.size.y));

        for (int x = 0; x < newTex.width; x++)
        {
            for (int y = 0; y < newTex.height; y++)
            {
                dX = x - mX;
                dY = y - mY;

                if (dX * dX + dY * dY <= erSize * erSize)
                {
                    newTex.SetPixel(x, y, zeroAlpha);
                }
                else
                {
                    newTex.SetPixel(x, y, copiedTexture.GetPixel(x, y));
                }
            }
        }
        
        newTex.Apply();
        return newTex;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetInstanceID() == target.GetInstanceID())
        {
            erasing = true;

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetInstanceID() == target.GetInstanceID())
        {
            erasing = false;
        }

    }
}
