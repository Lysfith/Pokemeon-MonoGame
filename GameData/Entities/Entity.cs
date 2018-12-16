using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Entities
{
    public class Entity
    {
        private Guid _id;

        public Entity()
        {
            _id = Guid.NewGuid();
        }
    }
}
