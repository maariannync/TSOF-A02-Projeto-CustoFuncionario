using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustoFuncionarioApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using CustoFuncionarioApp.Models;
using System.Threading.Tasks;

namespace CustoFuncionarioApp.Controllers.Tests
{
    [TestClass()]
    public class CalculoControllerTests
    {
        private CalculoController controller;

        [TestInitialize]
        public void inicializar()
        {
            controller = new CalculoController();
        }

        [TestMethod]
        [DataRow(2396.02, 5700.00, 220.00, 370.00)]
        [DataRow(7500.00, 0.00, 0.00, 0.00)]
        [DataRow(0.00, 430.00, 150.00, 100.00)]
        [DataRow(7300.00, 2050.00, 670.00, 350.00)]
        [DataRow(3200.00, 0.00, 0.00, 0.00)]
        [DataRow(3000.00, 540.00, 200.00d, 300.00d)]
        [DataRow(1430.00, 100.00, 170.00d, 340.00d)]
        [DataRow(230.00, 240.00, 450.00d, 660.00d)]
        public void TestarCusto(double salarioBruto, double planoSaude, double seguroVida, double outrosBeneficios)
        {
            var custo = new Custo
            {
                SalarioBruto = Convert.ToDecimal(salarioBruto),
                PlanoSaude = Convert.ToDecimal(planoSaude),
                SeguroVida = Convert.ToDecimal(seguroVida),
                OutrosBeneficios = Convert.ToDecimal(outrosBeneficios)
            };

            Assert.AreEqual(Convert.ToDecimal(salarioBruto), custo.SalarioBruto);
        }

        [TestMethod]
        [DataRow(2300.00, 460.00, 200.00, 400.00)]
        [DataRow(1680.00, 230.00, 450.00, 500.00)]
        [DataRow(2080.00, 520.00, 454.00, 250.00)]
        public void Relatorio_EntradaValida_RetornaViewComModelo(double salarioBruto, double planoSaude, double seguroVida, double outrosBeneficios)
        {
            // Arrange
            var custo = new Custo
            {
                SalarioBruto = Convert.ToDecimal(salarioBruto),
                PlanoSaude = Convert.ToDecimal(planoSaude),
                SeguroVida = Convert.ToDecimal(seguroVida),
                OutrosBeneficios = Convert.ToDecimal(outrosBeneficios)
            };

            // Act
            var resultado = controller.Relatorio(custo) as ViewResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(custo, resultado.Model);
        }


        [TestMethod]
        public void requisicao_DeveRetornarTipoProdutos()
        {
            // Arrange
            var esperado = typeof(Custo);
            var custo = new Custo
            {
                SalarioBruto = 3000M,
                PlanoSaude = 500M,
                SeguroVida = 100M,
                OutrosBeneficios = 200M
            };

            // Act
            var resultado = controller.Relatorio(custo);
            var viewResult = resultado as ViewResult;
            var obtido = viewResult?.Model;

            // Assert
            Assert.IsNotNull(viewResult);
            Assert.AreEqual(esperado, obtido?.GetType());
        }





        [TestClass]
        public class CustoTests
        {
            [TestMethod]
            public void Testar_Calculo_CustoTotal()
            {
                // Arrange
                var custo = new Custo
                {
                    SalarioBruto = 3000M,
                    PlanoSaude = 500M,
                    SeguroVida = 100M,
                    OutrosBeneficios = 200M
                };

                // Act
                var custoTotal = custo.getCustoTotal();

                // Assert
                decimal inssEsperado = custo.getINSS_Valor();
                decimal fgtsEsperado = custo.getFGTS();
                decimal decimoTerceiroEsperado = custo.get13oSalario();
                decimal feriasEsperado = custo.getFerias();
                decimal custoTotalEsperado = custo.SalarioBruto + inssEsperado + fgtsEsperado + decimoTerceiroEsperado + feriasEsperado + custo.PlanoSaude + custo.SeguroVida + custo.OutrosBeneficios;

                Assert.AreEqual(custoTotalEsperado, custoTotal);
            }
        }





    }
}