using System;
using InternalRealtimeCSG;
using RealtimeCSG;
using UnityEngine;

public static class PoetoolsRealtimeCsgHook
{
    public static void HandleRebuild()
    {
        var settings = Resources.Load<PoetoolsRealtimeCsgSettings>("CSG Settings");

        Debug.Log("Poetools custom logic!");

        foreach (var model in CSGModelManager.GetAllModels())
        {
            GeneratedMeshInstance[] meshes = MeshInstanceManager.GetAllModelMeshInstances(model.generatedMeshes);

            if (meshes != null)
            {
                foreach (var mesh in meshes)
                {
                    if (mesh.RenderSurfaceType == RenderSurfaceType.Collider && mesh.gameObject.activeSelf)
                        mesh.gameObject.SetActive(false);

                    if (mesh.RenderSurfaceType == RenderSurfaceType.Normal)
                        mesh.gameObject.AddComponent<MeshCollider>();

                    if (mesh.RenderSurfaceType == RenderSurfaceType.Normal)
                    {
                        if (_tagLookup.TryGetValue(mesh.RenderMaterial, out var tag))
                            mesh.gameObject.AddTags(tag);
                    }
                }
            }
        }
    }
}
