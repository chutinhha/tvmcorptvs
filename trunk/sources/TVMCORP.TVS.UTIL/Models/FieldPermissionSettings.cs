using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVMCORP.TVS.UTIL.MODELS
{
    [Serializable]
    public class FieldPermissionSettings : List<FieldPermissions>
    {

        public FieldPermissionSettings()
        {
            
        }

        public void AddItem(FieldPermissions fieldPermissions)
        {
            var old = this[fieldPermissions.FieldId];
            if (old != null)
            {
                old = fieldPermissions;
            }
            else
                base.Add(fieldPermissions);
        }

        public FieldPermissions this[Guid id]
        {
            get
            {
                return this.Where(p => p.FieldId == id).FirstOrDefault();
            }
           
        }

    }
}
