using InternalRealtimeCSG;
using poetools.Core;
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
                    if (mesh.RenderSurfaceType == RenderSurfaceType.Collider)
                        mesh.gameObject.SetActive(false);

                    if (mesh.RenderSurfaceType == RenderSurfaceType.Normal && !mesh.TryGetComponent(out MeshCollider _))
                        mesh.gameObject.AddComponent<MeshCollider>();

                    if (mesh.RenderSurfaceType == RenderSurfaceType.Normal)
                    {
                        foreach (var kvp in settings.materialToTag)
                        {
                            if (mesh.RenderMaterial == kvp.material)
                                mesh.gameObject.AddTags(kvp.tags);
                        }
                    }
                }
            }
        }
    }
}
