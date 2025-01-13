using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICounter
{
    void Interact(IKitchenObjectParent kitchenObjectParent);

    void Select(bool selected);
}
