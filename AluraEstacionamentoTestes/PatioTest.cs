using Xunit;
using AluraEstacionamento.Modelos;

namespace AluraEstacionamento.Testes
{
    public class PatioTest
    {
        private Operador operador;

        public PatioTest()
        {
            operador = new Operador()
            {
                Nome = "José da Silva"
            };
        }

        [Fact]
        public void TestValidacaoFaturamentoComUmVeiculo()
        {
            // Arrange
            var estacionamento = new Patio();

            estacionamento.OperadorPatio = operador;

            var veiculo = new Veiculo()
            {
                Proprietario = "João da Silva",
                Tipo = TipoVeiculo.Automovel,
                Cor = "Verde",
                Modelo = "Fusca",
                Placa = "ABC-9876"
            };

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            // Act
            double faturamento = estacionamento.TotalFaturado();

            // Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("João da Silva", "ABC-9876", "Verde", "Gol")]
        [InlineData("Maria Sousa", "DEF-5432", "Amarelo", "Fusca")]
        [InlineData("José Santos", "GHI-1098", "Amarelo", "Opala")]
        public void TestValidacaoFaturamentoComVariosVeiculos(string proprietario, string placa, string cor, string modelo)
        {
            // Arrange
            var estacionamento = new Patio();
            estacionamento.OperadorPatio = operador;

            var veiculo = new Veiculo()
            {
                Proprietario = proprietario,
                Tipo = TipoVeiculo.Automovel,
                Cor = cor,
                Modelo = modelo,
                Placa = placa
            };

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            // Act
            double faturamento = estacionamento.TotalFaturado();

            // Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("João da Silva", "ABC-9876", "Verde", "Gol")]
        public void LocalizaUmVeiculoNoEstacionamentoComBaseNaPlaca(string proprietario, string placa, string cor, string modelo)
        {
            // Arrange
            var estacionamento = new Patio();
            estacionamento.OperadorPatio = operador;

            var veiculo = new Veiculo()
            {
                Proprietario = proprietario,
                Placa = placa,
                Tipo = TipoVeiculo.Automovel,
                Cor = cor,
                Modelo = modelo,
            };

            veiculo.Acelerar(10);
            veiculo.Frear(10);

            estacionamento.RegistrarEntradaVeiculo(veiculo);

            // Act
            var consultado = estacionamento.PesquisaVeiculo(placa);

            // Assert
            Assert.Equal(placa, consultado.Placa);
        }

    }
}
