using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workers
{
    public partial class Worker
    {
        public string ChiefName { get
            {
                if(ChiefId!= null && ChiefId != Guid.Empty)
                {
                    using (var db = new WorkersdbEntities())
                    {
                        return db.Workers.First(item => item.Id == ChiefId).Firstname;
                    }
                }else
                {
                    return "";
                }
                
            }
        }

    }
}