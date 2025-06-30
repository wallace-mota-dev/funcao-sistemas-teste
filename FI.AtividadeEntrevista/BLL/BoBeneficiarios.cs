using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiarios
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(List<DML.Beneficiarios> beneficiarios)
        {
            DAL.DaoBeneficiarios ben = new DAL.DaoBeneficiarios();
            return ben.Incluir(beneficiarios);
        }

        /// <summary>
        /// Consulta o beneficiario pelo id do cliente
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public List<DML.Beneficiarios> Consultar(long id)
        {
            DAL.DaoBeneficiarios ben = new DAL.DaoBeneficiarios();
            return ben.Consultar(id);
        }
    }
}
