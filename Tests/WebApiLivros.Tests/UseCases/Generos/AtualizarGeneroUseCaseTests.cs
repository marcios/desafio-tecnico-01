using LivrosWebApi.Application.UseCases.Generos;
using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Dtos.Requests.Generos;
using LivrosWebApi.Core.Entities;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace LivrosWebApi.Tests.UseCases.Generos
{
    internal class AtualizarGeneroUseCaseTests
    {
        private AtualizarGeneroUseCase atualizarGenero;
        private IGeneroRepository _generoRespository;

        [SetUp]
        public void Setup()
        {
            atualizarGenero = new AtualizarGeneroUseCase(_generoRespository);
        }

        public AtualizarGeneroUseCaseTests()
        {
            _generoRespository = Substitute.For<IGeneroRepository>();
              
        }

        [TestCase]
        public async Task AdicionarGeneroUseCase_ExecuteAsyn_DeveFalharQuandoNomeNaoInformado()
        {
            //Arrange
            var cadastroRequest = new CadastroGeneroRequest()
            {
                Nome = string.Empty
            };

            _generoRespository.ObterPorIdAsync(Arg.Any<int>())
             .Returns(new Genero(cadastroRequest.Nome));

            //act
            var result = await atualizarGenero.ProcessarAsync(cadastroRequest);

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
            var result = await atualizarGenero.ProcessarAsync(cadastroRequest);

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
            var result = await atualizarGenero.ProcessarAsync(cadastroRequest);

            //Assert
            Assert.Multiple(() =>
            {

                Assert.IsTrue(result.Notificacoes.Any());
                Assert.IsTrue(result.Mensagem.Contains("Tamanho do nome excede máximo permitido"));
            });
        }


        [TestCase]
        public async Task AdicionarGeneroUseCase_ExecuteAsync_DeveFalharQuandoOCorrerExceptions()
        {
            //Arrange
            var cadastroRequest = new CadastroGeneroRequest()
            {
                Nome = "Tecnologia"
            };

            _generoRespository.ExistePorNomeAsync(Arg.Any<string>())
               .Throws(new Exception("Falha ao consultar"));

            //act
            var result = await atualizarGenero.ProcessarAsync(cadastroRequest);

            //Assert
            Assert.Multiple(() =>
            {

                Assert.IsTrue(result.Notificacoes.Count == 2);
                Assert.IsTrue(result.Mensagem.Contains("Falha ao atualizar gênero"));
            });
        }


        [TestCase]
        public async Task AdicionarGeneroUseCase_ExecuteAsyn_DeveFalharNaoPersistirOsDados()
        {
            //Arrange
            var cadastroRequest = new CadastroGeneroRequest()
            {
                Nome = "Tecnologia",
                Id =1
            };

            _generoRespository.ObterPorIdAsync(Arg.Any<int>())
                .Returns( new  Genero(cadastroRequest.Nome));

            _generoRespository.ExistePorNomeAsync(Arg.Any<string>()).Returns(false);

            _generoRespository.SaveChagesAsync ()
                .Returns(0);


            //act
            var result = await atualizarGenero.ProcessarAsync(cadastroRequest);

            //Assert
            Assert.Multiple(() =>
            {

                Assert.IsTrue(result.Notificacoes.Any());
                Assert.IsTrue(result.Mensagem.Contains("Não foi possível atualizar o cadastro do gênero"));
            });
        }

        [TestCase]
        public async Task AdicionarGeneroUseCase_ExecuteAsync_DeveFalharAoConsultarGeneroComMesmoNome()
        {
            //Arrange
            var cadastroRequest = new CadastroGeneroRequest()
            {
                Nome = "Tecnologia",
                Id = 1
            };


            _generoRespository.ExistePorNomeAsync(Arg.Any<string>(), Arg.Any<int>()).Returns(true);

            //act
            var result = await atualizarGenero.ProcessarAsync(cadastroRequest);

            //Assert
            Assert.Multiple(() =>
            {

                Assert.That(result.Notificacoes.Any(), Is.True);
                Assert.That(result.Mensagem.Contains("Já existe um gênero cadastrado"));
            });
        }


        [TestCase]
        public async Task AdicionarGeneroUseCase_ExecuteAsync_SucessoAoAtualizarGenero()
        {
            //Arrange
            var cadastroRequest = new CadastroGeneroRequest()
            {
                Nome = "Tecnologia",
                Id = 1
            };


            _generoRespository.ExistePorNomeAsync(Arg.Any<string>(), Arg.Any<int>()).Returns(false);
            _generoRespository.ObterPorIdAsync(Arg.Any<int>())
                .Returns(new Genero(cadastroRequest.Nome));

            _generoRespository.SaveChagesAsync().Returns(1);

            //act
            var result = await atualizarGenero.ProcessarAsync(cadastroRequest);

            //Assert
            Assert.Multiple(() =>
            {

                Assert.That(result.Notificacoes.Any(), Is.False);
                Assert.That(result.Data, Is.Not.Null);
            });
        }
    }
}
