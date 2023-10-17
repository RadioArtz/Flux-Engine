using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flux.Types
{
    public class BaseComponent
    {
        public Actor _parentObject;
        public Actor ParentObject => _parentObject;

        public BaseComponent()
        {
        }
        public virtual void OnComponentAttached(Actor parentObject)
        {
        }

        public virtual void OnComponentDestroyed()
        {
        }

        public Actor GetParent => ParentObject;
    }
}
