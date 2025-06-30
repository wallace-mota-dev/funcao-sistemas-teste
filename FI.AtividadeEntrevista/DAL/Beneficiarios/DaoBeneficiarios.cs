using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DAL
{
    internal class DaoBeneficiarios : AcessoDados
    {
        /// <summary>
        /// Inclui novos beneficiários
        /// </summary>
        /// <param name="beneficiarios">Objeto de beneficiarios</param>
        internal long Incluir(List<DML.Beneficiarios> beneficiarios)
        {
            long ret = 0;

            foreach (DML.Beneficiarios data in beneficiarios)
            {
                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

                parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", data.Nome));
                parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", data.CPF));
                parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", data.IdCliente));
                
                DataSet ds = base.Consultar("FI_SP_IncBenef", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                    long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            }

            return ret;
        }

        /// <summary>
        /// Conculta os beneficiários pelo id do cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal List<DML.Beneficiarios> Consultar(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", Id));

            DataSet ds = base.Consultar("FI_SP_ConsBenef", parametros);
            List<DML.Beneficiarios> beneficiarios = Converter(ds);

            return beneficiarios;
        }

        private List<DML.Beneficiarios> Converter(DataSet ds)
        {
            List<DML.Beneficiarios> lista = new List<DML.Beneficiarios>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Beneficiarios beneficiarios = new DML.Beneficiarios();
                    beneficiarios.Id = row.Field<long>("Id");
                    beneficiarios.CPF = row.Field<string>("CPF");
                    beneficiarios.Nome = row.Field<string>("Nome");
                    beneficiarios.IdCliente = row.Field<long>("IdCliente");
                    lista.Add(beneficiarios);
                }
            }

            return lista;
        }
    }
}
