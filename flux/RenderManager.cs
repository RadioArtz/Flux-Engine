using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flux.Types;

namespace Flux
{
    internal static class RenderManager
    {
        private static List<StaticMeshComponent> _staticMeshComponents = new List<StaticMeshComponent>();
        public static List<StaticMeshComponent> StaticMeshComponents => _staticMeshComponents;
        public static bool RegisterStaticMeshComponent(StaticMeshComponent Component)
        {
            _staticMeshComponents.Add(Component);
            return true;
        }
        public static bool Render()
        {   
            foreach (StaticMeshComponent smc in _staticMeshComponents)
            {
                smc.Render();
            }
            return true;
        }
    }
}