// using System.Collections.Generic;
// using InternalRealtimeCSG;
// using poetools.Core;
// using UnityEngine;
//
// public class RealtimeCsgSurfaceData
// {
//     private readonly Dictionary<Material, Tag> _tagLookup;
//
//     public RealtimeCsgSurfaceData(Dictionary<Material, Tag> tagLookup)
//     {
//         _tagLookup = tagLookup;
//     }
//
//     private void HandleMeshRebuilt(GeneratedMeshInstance mesh)
//     {
//         if (mesh.RenderSurfaceType == RenderSurfaceType.Normal)
//         {
//             if (_tagLookup.TryGetValue(mesh.RenderMaterial, out var tag))
//                 mesh.gameObject.AddTags(tag);
//         }
//     }
// }
