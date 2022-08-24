using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCountWidget : MonoBehaviour
{
    public TMPro.TMP_Text pointText;

    public void Refresh(int pointCount)
    {
        pointText.text = pointCount.ToString();
    }
}
