using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshUpdater : MonoBehaviour {
    private static bool needsNavMeshUpdate;
    private NavMeshSurface surface;

    private int skipFrames = 2;

    void Start() {
        surface = GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
    }

    void Update() {
        if (needsNavMeshUpdate) {
            if (skipFrames > 0) {
                skipFrames--;
                return;
            }
            surface.UpdateNavMesh(surface.navMeshData);
            skipFrames = 2;
            needsNavMeshUpdate = false;
        }
    }

    /// <summary> NavMeshSurface that updates only once per frame upon request </summary>
    public static void RequestNavMeshUpdate() => needsNavMeshUpdate = true;
}
