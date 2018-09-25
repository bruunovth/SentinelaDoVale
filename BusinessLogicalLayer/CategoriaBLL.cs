using DataAccessLayer;
using DataAccessLayer.Impl;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class CategoriaBLL
    {
        CategoriaDAL categoriaDAL = new CategoriaDAL();
        public List<Categoria> LerTodos()
        {
            return categoriaDAL.LerTodos();
        }

        public Categoria GetByID(Ocorrencia ocorrencia)
        {
            return categoriaDAL.GetByID(ocorrencia);
        }

        public Categoria GetByID(Sac sac)
        {
            return categoriaDAL.GetByID(sac);
        }
    }
}
