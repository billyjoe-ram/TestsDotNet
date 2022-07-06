namespace AluraEstacionamento.Modelos
{
    public class Patio
    {
        public Patio()
        {
            Faturado = 0;
            veiculos = new List<Veiculo>();
        }
        private List<Veiculo> veiculos;
        private double faturado;

        private Operador _operadorPatio;

        public Operador OperadorPatio { get => _operadorPatio; set => _operadorPatio = value; }

        public double Faturado { get => faturado; set => faturado = value; }
        public List<Veiculo> Veiculos { get => veiculos; set => veiculos = value; }
        public double TotalFaturado()
        {
            return Faturado;
        }

        public string MostrarFaturamento()
        {
            string totalfaturado = String.Format("Total faturado até o momento :::::::::::::::::::::::::::: {0:c}", TotalFaturado());
            return totalfaturado;
        }

        public void RegistrarEntradaVeiculo(Veiculo veiculo)
        {
            veiculo.HoraEntrada = DateTime.Now;
            veiculo.Ticket = GerarTicket(veiculo);
            Veiculos.Add(veiculo);
        }

        public string RegistrarSaidaVeiculo(string placa)
        {
            Veiculo procurado = new Veiculo();
            string informacao = string.Empty;

            foreach (Veiculo v in Veiculos)
            {
                if (v.Placa == placa)
                {
                    v.HoraSaida = DateTime.Now;
                    TimeSpan tempoPermanencia = v.HoraSaida - v.HoraEntrada;
                    double valorASerCobrado = 0;
                    if (v.Tipo == TipoVeiculo.Automovel)
                    {
                        /// o método Math.Ceiling(), aplica o conceito de teto da matemática onde o valor máximo é o inteiro imediatamente posterior a ele.
                        /// Ex.: 0,9999 ou 0,0001 teto = 1
                        /// Obs.: o conceito de chão é inverso e podemos utilizar Math.Floor();
                        valorASerCobrado = Math.Ceiling(tempoPermanencia.TotalHours) * 2;

                    }
                    if (v.Tipo == TipoVeiculo.Motocicleta)
                    {
                        valorASerCobrado = Math.Ceiling(tempoPermanencia.TotalHours) * 1;
                    }
                    informacao = string.Format(" Hora de entrada: {0: HH: mm: ss}\n " +
                                             "Hora de saída: {1: HH:mm:ss}\n " +
                                             "Permanência: {2: HH:mm:ss} \n " +
                                             "Valor a pagar: {3:c}", v.HoraEntrada, v.HoraSaida, new DateTime().Add(tempoPermanencia), valorASerCobrado);
                    procurado = v;
                    Faturado = Faturado + valorASerCobrado;
                    break;
                }

            }
            if (procurado != null)
            {
                Veiculos.Remove(procurado);
            }
            else
            {
                return "Não encontrado veículo com a placa informada.";
            }

            return informacao;
        }

        public Veiculo PesquisaVeiculo(string placa)
        {
            return veiculos.Where(v => v.Placa == placa).SingleOrDefault();
        }

        public Veiculo AlterarDadosVeiculo(Veiculo veiculoAlterado)
        {
            var veiculoAlteracao = (from veiculo in Veiculos
                                    where veiculo.Placa == veiculoAlterado.Placa
                                    select veiculo).SingleOrDefault();

            veiculoAlteracao.AlterarDados(veiculoAlterado);

            return veiculoAlteracao;
        }

        private string  GerarTicket(Veiculo veiculo)
        {
            string identificador = new Guid().ToString().Substring(0, 5);
            veiculo.IdTicket = identificador;

            string ticket = "### Ticket Estacionamento Alura ###" +
                            $">>> Identifcador: {identificador}" +
                            $">>> Data/Hora de entrada: {DateTime.Now}" +
                            $">>> Placa do Veículo:{veiculo.Placa}" +
                            $">>> Operador Patio: {OperadorPatio.Nome}";

            return ticket;
        }
    }
}
