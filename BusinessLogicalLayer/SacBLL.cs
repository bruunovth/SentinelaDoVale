using DataAccessLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class SacBLL
    {
        SacDAL sacDAL = new SacDAL();
        public List<Sac> LerTodos()
        {
            return sacDAL.LerTodos();
        }
    }
}
