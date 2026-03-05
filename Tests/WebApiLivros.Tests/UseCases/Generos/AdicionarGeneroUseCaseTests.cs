using LivrosWebApi.Application.UseCases.Generos;
using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Dtos.Requests.Generos;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace LivrosWebApi.Tests.UseCases.Generos
{
    internal class AdicionarGeneroUseCaseTests
    {
        private AdicionarGeneroUseCase adicionarGenero;
        private IGeneroRepository _generoRespository;

        [SetUp]
        public void Setup()
        {
            adicionarGenero = new AdicionarGeneroUseCase(_generoRespository);
        }

        public AdicionarGeneroUseCaseTests()
        {
            _generoRespository = Substitute.For<IGeneroRepository>();

        }

        [TestCase]
        public async Task AdicionarGeneroUseCase_ExecuteAsyn_DeveFalharQuandoNomeInformado()
        {
            //Arrange
            var cadastroRequest = new CadastroGeneroRequest()
            {
                Nome = string.Empty
            };

            //act
            var result = await adicionarGenero.ProcessarAsync(cadastroRequest);

            //Assert
            Assert.Multiple(() =>
            {

                Assert.IsTrue(result.Notificacoes.Any());
                Assert.IsTrue(result.Mensagem.Contains("Nome do gênero deve ser informado"));
            });
        }


        [TestCase]
        public async Task AdicionarGeneroUseCase_ExecuteAsyn_DeveFalharQuandoNomJaExiste()
        {
            //Arrange
            var cadastroRequest = new CadastroGeneroRequest()
            {
                Nome = "Tecnologia"
            };

            _generoRespository.ExistePorNomeAsync(Arg.Any<string>())
                .Returns(true);


            //act
            var result = await adicionarGenero.ProcessarAsync(cadastroRequest);

            //Assert
            Assert.Multiple(() =>
            {

                Assert.IsTrue(result.Notificacoes.Any());
                Assert.IsTrue(result.Mensagem.Contains($"Já existe um gênero cadastrado com o nome {cadastroRequest.Nome}"));
            });
        }


        [TestCase]
        public async Task AdicionarGeneroUseCase_ExecuteAsyn_DeveFalharQuandoNomeInformadoExcedeTamanhoMaximo()
        {
            //Arrange
            var cadastroRequest = new CadastroGeneroRequest()
            {
                Nome = "GfjLHeZKuIcHnRVjfAyRkanDgGRVhMcNshwolfPYvNMCiDrVENUzVYGEThfxAvNFFILvZuCfeqlHEWWgHobKyuhOntMzUWVwEGkqh"
            };

            //act
            var result = await adicionarGenero.ProcessarAsync(cadastroRequest);

            //Assert
            Assert.Multiple(() =>
            {

                Assert.IsTrue(result.Notificacoes.Any());
                Assert.IsTrue(result.Mensagem.Contains("Tamanho do nome excede máximo permitido"));
            });
        }


        [TestCase]
        public async Task AdicionarGeneroUseCase_ExecuteAsyn_DeveFalharQuandoOCorrerExceptions()
        {
            //Arrange
            var cadastroRequest = new CadastroGeneroRequest()
            {
                Nome = "Tecnologia"
            };

            _generoRespository.ExistePorNomeAsync(Arg.Any<string>())
               .Throws(new Exception("Falha ao consultar"));

            //act
            var result = await adicionarGenero.ProcessarAsync(cadastroRequest);

            //Assert
            Assert.Multiple(() =>
            {

                Assert.IsTrue(result.Notificacoes.Count == 2);
                Assert.IsTrue(result.Mensagem.Contains("Falha ao cadastrar novo gênero"));
            });
        }


        [TestCase]
        public async Task AdicionarGeneroUseCase_ExecuteAsyn_DeveFalharNaoPersistirOsDados()
        {
            //Arrange
            var cadastroRequest = new CadastroGeneroRequest()
            {
                Nome = "Tecnologia"
            };

            _generoRespository.SaveChagesAsync()
                .Returns(0);


            //act
            var result = await adicionarGenero.ProcessarAsync(cadastroRequest);

            //Assert
            Assert.Multiple(() =>
            {

                Assert.IsTrue(result.Notificacoes.Any());
                Assert.IsTrue(result.Mensagem.Contains("Não foi possível salvar o cadastro do gênero"));
            });
        }

        [TestCase]
        public async Task AdicionarGeneroUseCase_ExecuteAsync_SucessoAoCadastrar()
        {
            //Arrange
            var cadastroRequest = new CadastroGeneroRequest()
            {
                Nome = "Tecnologia"
            };

            _generoRespository.ExistePorNomeAsync(Arg.Any<string>())
              .Returns(false);

            
            _generoRespository.SaveChagesAsync()
                .Returns(1);


            //act
            var result = await adicionarGenero.ProcessarAsync(cadastroRequest);

            //Assert
            Assert.Multiple(() =>
            {

                Assert.That(result.Notificacoes.Any(), Is.False);
                Assert.That(result.Data, Is.Not.Null);
            });
        }
    }
}
