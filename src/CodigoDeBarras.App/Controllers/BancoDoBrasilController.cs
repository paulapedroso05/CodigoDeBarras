using BoletoNet;
using CodigoDeBarras.Bussiness.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodigoDeBarras.App.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BancoDoBrasilController : Controller
    {
        private readonly IBancoDoBrasilService _bancoDoBrasilService;
        public BancoDoBrasilController(IBancoDoBrasilService bancoDoBrasilService)
        {
            _bancoDoBrasilService = bancoDoBrasilService;
        }

        [HttpPost]
        public async Task<ActionResult> LinhaDigitavel()
        {
            try
            {

                DateTime vencimento = new DateTime(2020, 6, 14);

                //cedente: cpf/cnpj + nome + agencia + digito agencia + conta + digito conta
                var cedente = new Cedente("00.000.000/0000-00", "Empresa Teste", "0131", "7", "00059127", "0");

                //boleto: dataVencimento + valor do boleto + carteira(numero da carteira do banco) + nosso Numero + cedente
                //nosso numero se encontra em alguma tabela do banco progress
                BoletoNet.Boleto boleto = new BoletoNet.Boleto(vencimento, 1700, "17-019", "18204", cedente);

                boleto.NumeroDocumento = "18204";

                var boletoBancario = new BoletoBancario();

                boletoBancario.CodigoBanco = 1;

                boletoBancario.Boleto = boleto;

                return Ok(boletoBancario);
                
            }
            catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar cadastro. Erro: {ex.Message}");
            }
        }
    }
}
