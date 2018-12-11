using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Job {

    void SetResource(Item item);

    void SetTargetDestination(GameObject target);
}
