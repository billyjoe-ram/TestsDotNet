namespace AluraEstacionamento.Modelos
{
    public class Operador
    {
        // Campos
        private string _matricula = "";
        private string _nome = "";

        //Propriedades
        public string Matricula { get => _matricula; private set => _matricula = value; }
        public string Nome { get => _nome; set => _nome = value; }

        //Construtor
        public Operador()
        {
            Matricula = new Guid().ToString().Substring(0, 8);
        }

        public override string ToString()
        {
            return $"Operador: {Nome} \n" +
                $"Matrícula: {Matricula}";
        }
    }
}
