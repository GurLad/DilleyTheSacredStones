using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBeatAnimation : MonoBehaviour
{
    public float Speed;
    public float MaxSize;
    public List<Color> Colors;
    private Color color;
    private Vector3 initSize;
    private float count;
    private Renderer renderer;

    public void Activate(float accuracy)
    {
        GameObject clone = Instantiate(gameObject, transform.parent);
        clone.GetComponent<BoomBeatAnimation>().color = accuracy > 0 ? Colors[Mathf.FloorToInt(Mathf.Abs(accuracy - 0.00001f) * (Colors.Count - 1)) + 1] : Colors[0];
        clone.SetActive(true);
    }

    private void Awake()
    {
        initSize = transform.localScale;
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        count += Time.deltaTime * Speed;
        if (count < 1)
        {
            color.a = 1 - count;
            renderer.material.color = color;
            transform.localScale = initSize * (1 + count * (MaxSize - 1));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
