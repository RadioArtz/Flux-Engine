using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flux.Core
{
    public abstract class FScene
    {
        public FScene() { }
        public virtual void OnLoad() { }
        public virtual void OnTick() { }
    }
}