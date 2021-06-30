using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_MP : MPMng
{
    public void SpendMP(int v)
    {
        MP.ChangeMP(-v);
    }
}
