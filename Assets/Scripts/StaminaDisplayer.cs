using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaDisplayer : MonoBehaviour
{
    [SerializeField]
    private Image bar;
    // Start is called before the first frame update
    [SerializeField]
    private stats status;
    private float height;
    private float width;

    void Start()
    {
        height = bar.rectTransform.sizeDelta.y;
        width = bar.rectTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        bar.rectTransform.sizeDelta  = new Vector2(width*(status.getFractionOfEnergy()),height);
    }
}
