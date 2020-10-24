using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    public Collider2D boxCollider;
    public Bounds Bounds { get { return boxCollider.bounds; } }
}
