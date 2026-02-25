using VHBurguer3.Exeception;

namespace VHBurguer3.Applications.Regras
{
    public class ValidarDataExpiracaoPromocao
    {
        public static void ValidarDataExpiracao(DateTime dataExpiracao)
        {
            if(dataExpiracao <= DateTime.Now)
            {
                throw new DomainException("Data de expiração deve ser futura!");
            }
        }
    }
}
