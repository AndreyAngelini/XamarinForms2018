using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFAppCEP.Service;
using XFAppCEP.Service.Model;

namespace XFAppCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            imgLogo.Source = "Logo.png";
            btnBuscarCep.Clicked += BtnBuscarCep_Clicked; 
        }

        private void BtnBuscarCep_Clicked(object sender, EventArgs e)
        {
            try
            {
                string cep = txtCep.Text.Trim();

                Address endereco = new Address();
                viaCEPServico buscarCEP = new viaCEPServico();

                string mensagemErro = "";
                if (IsValidCEP(cep, out mensagemErro))
                {
                    endereco = buscarCEP.BuscarEnderecoViaCEP(cep);
                    if (endereco != null)
                    {
                        lblMensagem.Text = string.Format("Endereço: {0}\nBairro: {1}\nCidade: {2}\nUF: {3}\nIBGE: {4}",
                                              endereco.logradouro,
                                              endereco.bairro,
                                              endereco.localidade,
                                              endereco.uf,
                                              endereco.ibge);
                    }
                    else
                    {
                        DisplayAlert("Erro", "Endereço não encontrado para o cep:" + cep , "Ok");
                    }
                }
                else
                {
                    DisplayAlert("Erro", mensagemErro, "Ok");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", "Problemas para buscar o CEP. \nErro: " + ex.Message,"OK");
            }

        }

        private bool IsValidCEP(string cep, out string mesnsagem )
        {
            bool retorno = true;
            mesnsagem = "";
            if (cep.Length != 8)
            {
                mesnsagem += "\nO CEP deve conter 8 dígitos.";
                retorno = false;
            }

            double dcep = new double();
            if (!double.TryParse(cep, out dcep))
            {
                mesnsagem += "\nO CEP deve conter apenas números.";
                retorno = false;
            }

            return retorno;
        }
    }
}
