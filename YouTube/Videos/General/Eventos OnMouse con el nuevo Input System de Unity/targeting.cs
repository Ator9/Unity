using UnityEngine;
using UnityEngine.EventSystems;

// Requirements:
// 1. Attach Physics Raycaster to camera
// 2. Add EventSystem Object

// Layers | Event Mask (enemy, pickable)

public class Targeting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private new Renderer renderer;

    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            print("Layer ID "+ gameObject.layer+ " | Name "+ LayerMask.NameToLayer("Enemy"));
        }

        print("OnPointerEnter " + gameObject.name);
        renderer.material.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("OnPointerExit " + gameObject.name);
        renderer.material.color = Color.gray;
    }
}
