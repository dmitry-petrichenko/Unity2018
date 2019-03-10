using Scripts.Extensions;
using UnityEngine;

public class Disposable : AutofacDisposable
{
    protected override void DisposeInternal()
    {
        //Debug.Log(GetType().Name + " Disposed");
    }
}