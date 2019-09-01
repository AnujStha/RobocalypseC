
using TMPro;
using UnityEngine;

public class FlyingText : MonoBehaviour
{

    public Color colour;
    public string value;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.color = colour;
        text.text = value;
    }
    public void kill() {
        Destroy(gameObject);
    }
}
