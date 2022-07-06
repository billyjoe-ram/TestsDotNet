using Xunit;
using Xunit.Abstractions;

using System;
using AluraEstacionamento.Modelos;

namespace AluraEstacionamento.Testes
{
    public class VeiculoTest : IDisposable
    {
        private Veiculo veiculo;
        public ITestOutputHelper SaidaConsoleTest;

        public VeiculoTest(ITestOutputHelper _saidaConsoleTest)
        {
            SaidaConsoleTest = _saidaConsoleTest;

            veiculo = new Veiculo()
            {
                Proprietario = "Jo�o da Silva",
                Tipo = TipoVeiculo.Automovel,
                Placa = "ABC-9876",
                Cor = "Verde",
                Modelo = "Gol"
            };
        }

        [Trait("Funcionalidade", "Acelerar")]
        [Fact(DisplayName = "Teste para acelerar ve�culo")]
        public void TestAcelerarVeiculo()
        {
            // Act
            veiculo.Acelerar(10);

            // Assert
            Assert.Equal(100, veiculo.VelocidadeAtual);
        }

        [Trait("Funcionalidade", "Frear")]
        [Fact(DisplayName = "Teste para frear ve�culo")]
        public void TestFrearVeiculo()
        {
            veiculo.Frear(10);

            Assert.Equal(-150, veiculo.VelocidadeAtual);
        }

        [Fact(DisplayName = "Teste para validar o nome do propriet�rio", Skip="Teste com implementa��o n�o finalizada. Ignorar.")]
        public void TestValidacaoNomeProprietario()
        {
        }

        [Fact]
        public void GerarFichadeInforma��odoProprioVeiculo()
        {

            // Act
            var dadosEmString = veiculo.ToString();

            // Assert
            Assert.Contains("Ficha do Ve�culo:", dadosEmString);
        }

        [Fact]
        public void TestAlteracaoDadosVeiculoComBaseNaPlaca()
        {
            // Arrange
            var estacionamento = new Patio();
            estacionamento.OperadorPatio = new Operador
            {
                Nome = "Jo�o da Silva"
            };

            var veiculo = new Veiculo()
            {
                Proprietario = "Jos� Santos",
                Tipo = TipoVeiculo.Automovel,
                Cor = "Amarelo",
                Modelo = "Opala",
                Placa = "GHI-1098"
            };
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            var veiculoAlterado = new Veiculo()
            {
                Proprietario = "Jos� Santos",
                Tipo = TipoVeiculo.Automovel,
                Cor = "Preto",
                Modelo = "Opala",
                Placa = "GHI-1098"
            };

            // Act
            Veiculo alteracaoVeiculo = estacionamento.AlterarDadosVeiculo(veiculoAlterado);

            // Assert
            Assert.Equal(alteracaoVeiculo.Cor, veiculo.Cor);
        }

        [Fact]
        public void TestaNomeProprietarioComDoisCaracteres()
        {
            //Arrange
            string nomeProprietario = "Ab";
            //Assert
            Assert.Throws<FormatException>(
                //Act
                () => new Veiculo(nomeProprietario)
            );
        }

        public void Dispose()
        {
            SaidaConsoleTest.WriteLine("Fim de teste de ve�culo");
        }
    }
}