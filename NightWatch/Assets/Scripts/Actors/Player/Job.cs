using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Job {

    void SetResource(ObjectResource item);

    void SetTargetDestination(GameObject target);
}
