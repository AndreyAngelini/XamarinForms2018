using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using XFAppCEP.Service.Model;
using Newtonsoft.Json;
namespace XFAppCEP.Service
{
    public class viaCEPServico
    {
        private static string EnderecoURL = "http://viacep.com.br/ws/{0}/json/";

        public Address BuscarEnderecoViaCEP(string cep)
        {
            Address endereco = new Address();
            string NovoEnderecoUrl = string.Format(EnderecoURL, cep);
            WebClient wc = new WebClient();
            string conteudo = wc.DownloadString(NovoEnderecoUrl);
            endereco = JsonConvert.DeserializeObject<Address>(conteudo);
            if (endereco.cep == null)
                return null;

            return endereco;
        }
    }
}
