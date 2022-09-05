using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    public PointCountWidget pointCountWidget;
    
    private int _pointAmount;
    
    // Helper function to check if a transaction is possible
    public bool CheckTransaction(int amount)
    {
        if (_pointAmount - amount >= 0)
        {
            return true;
        }

        return false;
    }
    
    public void Transaction(int amount, bool addition)
    {
        if (addition)
        {
            _pointAmount += amount;
            pointCountWidget.Refresh(_pointAmount);
        }
        else
        {
            _pointAmount -= amount;
            pointCountWidget.Refresh(_pointAmount);
        }
    }
}
