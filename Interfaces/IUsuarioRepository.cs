using VHBurguer3.Domains;

namespace VHBurguer3.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();

        // pode ser que não venha nenhum usuário na busca,
        //então colocamos "?" para permitir que seja nulo
        Usuario? ObterPorId(int id);

        Usuario? ObterPorEmail(string email);

        bool EmailExiste(string email);

        void Adicionar (Usuario usuario);

        void Atualizar(Usuario usuario);

        void Remover(int id);
    }
}
