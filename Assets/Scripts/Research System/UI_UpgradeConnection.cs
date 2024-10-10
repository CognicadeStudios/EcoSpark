using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradeConnection : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    private UI_UpgradeButton startButton;
    private RectTransform rectTransform;

    public Color offColor = new(.5f, .5f, .5f);
    public Color onColor = new(.9f, .9f, .95f);
    [SerializeField]
    public static int connectionWidth = 22;
    private bool lit = false;
    private Image image;

    private Upgrade sourceUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        startButton = start.GetComponent<UI_UpgradeButton>();
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        sourceUpgrade = startButton.upgrade;
    }

    public void UpdateVisual()
    {
        if (lit)
        {
            image.color = onColor;  
        }
        else
        {
            image.color = offColor;
        }
    }

    private void PositionSelf()
    {
        startButton = start.GetComponent<UI_UpgradeButton>();
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        sourceUpgrade = startButton.upgrade;

        Vector3[] positions = new Vector3[] { start.transform.position, end.transform.position };
        Vector3 midpoint = positions[0] + (positions[1] - positions[0]) / 2;
        transform.position = midpoint;

        Vector3 delta = positions[1] - midpoint;
        float theta = Mathf.Rad2Deg * Mathf.Atan2(delta.y, delta.x);
        transform.eulerAngles = new Vector3(0, 0, theta + 90);

        Vector2 dist = new(positions[1].x - positions[0].x, positions[1].y - positions[0].y);
        float mult = sourceUpgrade == Upgrade.NonrenewableEnergy ? 1f : sourceUpgrade == Upgrade.Battery2 ? 1.3f : 1.2f;
        float len = dist.magnitude * mult;
        rectTransform.sizeDelta = new Vector2(connectionWidth, len);
    }

    public void Unlock()
    {
        lit = true;
    }
    public Image GetImage()
    {
        return image;
    }
    public Upgrade GetUpgrade()
    {
        return sourceUpgrade;
    }
}
