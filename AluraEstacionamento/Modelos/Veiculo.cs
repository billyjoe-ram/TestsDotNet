﻿namespace AluraEstacionamento.Modelos
{
    public class Veiculo
    {
        //Campos    
        private string _placa = "";
        private string _proprietario = "";
        private TipoVeiculo _tipo;
        private string _ticket = "";

        //Propriedades
        public string Ticket { get { return _ticket; } set { _ticket = value; } }
        public string IdTicket { get; set; } = "";
        public string Placa
        {
            get
            {
                return _placa;
            }
            set
            {
                // Checa se o valor possui pelo menos 8 caracteres
                if (value.Length != 8)
                {
                    throw new FormatException(" A placa deve possuir 8 caracteres");
                }
                for (int i = 0; i < 3; i++)
                {
                    //checa se os 3 primeiros caracteres são numeros
                    if (char.IsDigit(value[i]))
                    {
                        throw new FormatException("Os 3 primeiros caracteres devem ser letras!");
                    }
                }
                //checa o Hifem
                if (value[3] != '-')
                {
                    throw new FormatException("O 4° caractere deve ser um hífen");
                }
                //checa se os 3 primeiros caracteres são numeros
                for (int i = 4; i < 8; i++)
                {
                    if (!char.IsDigit(value[i]))
                    {
                        throw new FormatException("Do 5º ao 8º caractere deve-se ter um número!");
                    }
                }
                _placa = value;

            }
        }
        public string Cor { get; set; } = "";
        public double Largura { get; set; }
        public double VelocidadeAtual { get; set; }
        public string Modelo { get; set; } = "";
        public string Proprietario
        {
            get
            {
                return _proprietario;
            }
            set
            {
                if (value.Length < 3)
                {
                    throw new FormatException(" Nome de proprietário deve ter no mínimo 3 caracteres.");
                }
                _proprietario = value;
            }
        }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSaida { get; set; }
        public TipoVeiculo Tipo { get => _tipo; set => _tipo = value; }

        //Métodos
        public void Acelerar(int tempoSeg)
        {
            VelocidadeAtual += (tempoSeg * 10);
        }

        public void Frear(int tempoSeg)
        {
            VelocidadeAtual -= (tempoSeg * 15);
        }

        //Construtor
        public Veiculo()
        {

        }

        public Veiculo(string proprietario)
        {
            Proprietario = proprietario;
        }

        public override string ToString()
        {
            return $"Ficha do Veículo:\n " +
                $"Tipo do Veículo: {Tipo}\n " +
                $"Proprietário: {Proprietario}\n " +
                $"Modelo: {Modelo}\n " +
                $"Cor: {Cor}\n " +
                $"Placa: {Placa}\n";
        }

        public void AlterarDados(Veiculo veiculoAlterado)
        {
            Cor = veiculoAlterado.Cor;
            HoraEntrada = veiculoAlterado.HoraEntrada;
            HoraSaida = veiculoAlterado.HoraSaida;
            Largura = veiculoAlterado.Largura;
            Modelo = veiculoAlterado.Modelo;
            Placa = veiculoAlterado.Placa;
            Proprietario = veiculoAlterado.Proprietario;
            Tipo = veiculoAlterado.Tipo;
            VelocidadeAtual = veiculoAlterado.VelocidadeAtual;
        }
    }
}
