using AppChamaGas.Interface;
using AppChamaGas.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AppChamaGas.Services.Azure
{
    public abstract class AzureService<T> where T : IAzureTabela
    {
        MobileServiceClient clienteAzure;
        IMobileServiceTable<T> tabelaAzure;

        //Metodo construtor
        public AzureService()
        {
            //Configura o serviço de conexao do Azure
            clienteAzure = new MobileServiceClient(App.uriAzure);
            //Fazer o acesso a tabela
            tabelaAzure = clienteAzure.GetTable<T>();
        }

        /// <summary>
        /// Metodo de inclusao de um registro no banco de dados Azure
        /// </summary>
        /// <param name="registro">registro</param>
        /// <returns>Retorna verdadeiro ou falso para operacao</returns>
        public async Task<bool> IncluirAsync(T registro)
        {
            try
            {
                await tabelaAzure.InsertAsync(registro);
                
                return true;
            }
            catch (Exception erro)
            {
                Debug.WriteLine($"Erro Azure:{erro}");
                return false;
            }
        }

        /// <summary>
        /// Metodo de alteracao de um registro no banco de dados Azure
        /// </summary>
        /// <param name="registro">registro</param>
        /// <returns>Retorna verdadeiro ou falso para operacao</returns>
        public async Task<bool> AlterarAsync(T registro)
        {
            try
            {
                await tabelaAzure.UpdateAsync(registro);
                return true;
            }
            catch (Exception erro)
            {
                Debug.WriteLine($"Erro Azure:{erro}");
                return false;
            }
        }

        /// <summary>
        /// Metodo de exclusao de um registro no banco de dados Azure
        /// </summary>
        /// <param name="registro">registro</param>
        /// <returns>Retorna verdadeiro ou falso para operacao</returns>
        public async Task<bool> ExcluirAsync(T registro)
        {
            try
            {
                await tabelaAzure.DeleteAsync(registro);
                return true;
            }
            catch (Exception erro)
            {
                Debug.WriteLine($"Erro Azure:{erro}");
                return false;
            }
        }

        /// <summary>
        /// Metodo que lista todos os registros da tabela no banco de dados Azure
        /// </summary>
        /// <returns>Retorna todos os registros da tabela</returns>
        public async Task<IEnumerable<T>> ListarAsync()
        {
            return await tabelaAzure.ToListAsync();
        }

        /// <summary>
        /// Metodo que obtem um registro no banco de dados Azure
        /// </summary>
        /// <param name="registroId">Id do registro</param>
        /// <returns>Retorna o registro ou nulo</returns>
        public async Task<T> ObterAsync(string registroId)
        {
            var tabela = await tabelaAzure.ToListAsync();
            var registro = tabela.FirstOrDefault(r => r.Id == registroId);
            return registro;
        }

    }
}
