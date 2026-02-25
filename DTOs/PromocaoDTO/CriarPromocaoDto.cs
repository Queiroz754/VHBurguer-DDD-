namespace VHBurguer3.DTOs.PromocaoDTO
{
    public class CriarPromocaoDto
    {
        public string Nome { get; set; } = null;

        public DateTime DataExpiracao { get; set; }

        public bool StatusPromocao { get; set; }
    }
}
