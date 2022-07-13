using ConsoleTables;
using ConsoleTools;
using Model;
using Repository.Interface;
using Sharprompt;

namespace Services
{
    public class MenuService
    {
        private readonly IRepository<Term> _englishTermRepository;
        private readonly IRepository<Term> _portugueseTermRepository;

        public MenuService(IEnglishTermRepository englishTermRepository, IPortugueseTermRepository portugueseTermRepository)
        {
            _englishTermRepository = englishTermRepository;
            _portugueseTermRepository = portugueseTermRepository;
        }
        public void InitializeMenu()
        {
            var menu = new ConsoleMenu()
                .Add("1 - Inglês", () => SubMenu("Dicionário de Inglês", _englishTermRepository))
                .Add("2 - Português", () => SubMenu("Dicionário de Português", _portugueseTermRepository))
                .Add("3 - Sair", ConsoleMenu.Close)
                .ConfigureMenu("Selecione o idioma");

            menu.Show();
        }

        public void SubMenu(string title, IRepository<Term> repository)
        {
            var menu = new ConsoleMenu()
                .Add("1 - Buscar", () => Saerch(repository))
                .Add("2 - Adicionar termo", () => AddTerm(repository))
                .Add("3 - Remover termo", () => RemoveTerm(repository))
                .Add("4 - Alterar termo", () => ChangeTerm(repository))
                .Add("5 - Sair", ConsoleMenu.Close)
                .ConfigureMenu(title);

            menu.Show();
        }

        private void ChangeTerm(IRepository<Term> repository)
        {
            Console.Clear();
            var terms = repository.GetAll();

            if(terms.Count == 0)
            {
                Console.WriteLine("A lista de termos está vazia");
                Console.ReadKey();
                return;
            }

            var term = Prompt.Select("Selecione o termo a ser alterado", terms);
            repository.Update(term);
        }

        private void RemoveTerm(IRepository<Term> repository)
        {
            Console.Clear();
            var terms = repository.GetAll();

            if (terms.Count == 0)
            {
                Console.WriteLine("A lista de termos está vazia");
                Console.ReadKey();
                return;
            }

            var term = Prompt.Select("Selecione o termo a ser removido", terms);
            repository.Delete(term);
        }

        private void AddTerm(IRepository<Term> repository)
        {
            Console.Clear();
            var term = Prompt.Bind<Term>();
            repository.Add(term);
        }

        public void Saerch(IRepository<Term> repository)
        {
            Console.Clear();
            Console.Write("Qual termo deseja pesquisar? ");
            var term = Console.ReadLine();

            var terms = repository.FindAll(term);

            ConsoleTable
                .From(terms)
                .Write(Format.Minimal);

            Console.ReadKey();
        }



    }
}
