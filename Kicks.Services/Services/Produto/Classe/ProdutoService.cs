using Kicks.Data.Database;
using Kicks.Domain.Produto;
using Kicks.Models.Produto;
using Kicks.Services.Exceptions.BadRequest;
using Kicks.Services.Utils;
using Microsoft.EntityFrameworkCore;

namespace Kicks.Services.Services.Produto.Classe
{
    public class ProdutoService : IProdutoService
    {
        #region Dependências
        private readonly KicksDataContext _kicksDataContext;
        #endregion

        #region Construtor
        public ProdutoService(KicksDataContext kicksDataContext)
        {
            _kicksDataContext = kicksDataContext;
        }
        #endregion

        #region Criar Produto
        public async Task<ProdutoModel> CriarProduto(ProdutoModel model)
        {

            var consulta = await _kicksDataContext.Set<ProdutoEntity>()
                .Where(x => x.ProdutoId == model.ProdutoId)
                .FirstOrDefaultAsync();

            if (consulta != null)
            {
                throw new KicksBadRequestException("O produto já existe.");
            }

            foreach (var item in model.Imagens ?? [])
            {
                item.ImagemUrl = ConvertImageBaseToUrl.ImageUrl(item.ImagemUrl ?? "");
            }

            var produto = new ProdutoModel()
            {
                ProdutoId = model.ProdutoId,
                Nome = model.Nome,
                Descricao = model.Descricao,
                Marca = model.Marca,
                SKU = model.SKU,
                QtdEstoque = model.QtdEstoque,
                Preco = model.Preco,
                PrecoPromocao = model.PrecoPromocao,
                Imagens = model.Imagens,
                Tags = model.Tags,
            };

            var context = await _kicksDataContext.AddAsync<ProdutoEntity>(produto);

            _kicksDataContext.SaveChanges();

            return context.Entity;
        }
        #endregion

        #region Editar Produto
        public async Task<ProdutoModel> EditarProduto(Guid produtoId, ProdutoModel model)
        {
            var produto = await _kicksDataContext.Set<ProdutoEntity>()
                .Where(x => x.ProdutoId == produtoId)
                .FirstOrDefaultAsync();

            if (produto == null)
            {
                throw new KicksBadRequestException("Não foi possivel localizar o produto.");
            }

            foreach (var item in model.Imagens ?? [])
            {
                item.ImagemUrl = ConvertImageBaseToUrl.ImageUrl(item.ImagemUrl ?? "");
            }

            produto.ProdutoId = model.ProdutoId;
            produto.Nome = model.Nome;
            produto.Descricao = model.Descricao;
            produto.Marca = model.Marca;
            produto.SKU = model.SKU;
            produto.QtdEstoque = model.QtdEstoque;
            produto.Preco = model.Preco;
            produto.PrecoPromocao = model.PrecoPromocao;

            var context = _kicksDataContext.Update<ProdutoEntity>(produto);

            if (model?.Imagens?.Count > 0)
            {
                foreach (var item in model.Imagens)
                {
                    _kicksDataContext.Update<ProdutoImagemEntity>(item);
                }
            }

            if (model?.Tags?.Count > 0)
            {
                foreach (var item in model.Tags)
                {
                    _kicksDataContext.Update<ProdutoTagEntity>(item);
                }
            }

            _kicksDataContext.SaveChanges();

            return context.Entity;
        }
        #endregion

        #region Obter Todos os Produtos
        public async Task<ICollection<ProdutoModel>> ObterProdutos()
        {
            var produtos = await _kicksDataContext.Set<ProdutoEntity>().ToListAsync();

            var imagens = await _kicksDataContext.Set<ProdutoImagemEntity>().ToListAsync();

            var tags = await _kicksDataContext.Set<ProdutoTagEntity>().ToListAsync();

            var produtosCompletos = produtos.Select(x => new ProdutoModel()
            {
                ProdutoId = x.ProdutoId,
                Nome = x.Nome,
                Descricao = x.Descricao,
                Marca = x.Marca,
                SKU = x.SKU,
                QtdEstoque = x.QtdEstoque,
                Preco = x.Preco,
                PrecoPromocao = x.PrecoPromocao,
                Imagens = imagens.Select(y => new ProdutoImagemModel()
                {
                    ProdutoImagemId = y.ProdutoImagemId,
                    ProdutoId = y.ProdutoImagemId,
                    ImagemUrl = y.ImagemUrl,
                    Descricao = y.Descricao,
                }).ToList(),
                Tags = tags.Select(z => new ProdutoTagModel()
                {
                    ProdutoTagId = z.ProdutoTagId,
                    ProdutoId = z.ProdutoId,
                    Nome = z.Nome,
                    Descricao = z.Descricao,
                }).ToList(),
            }).ToList();

            return produtosCompletos;
        }
        #endregion

        #region Obter Produto Por Id
        public async Task<ProdutoModel> ObterProdutoById(Guid produtoId)
        {
            var imagens = await _kicksDataContext.Set<ProdutoImagemEntity>()
                .Where(x => x.ProdutoId == produtoId)
                .ToListAsync();

            var tags = await _kicksDataContext.Set<ProdutoTagEntity>()
                .Where(x => x.ProdutoId == produtoId)
                .ToListAsync();

            var produto = await _kicksDataContext.Set<ProdutoEntity>()
                .Where(x => x.ProdutoId == produtoId)
                .Select(x => new ProdutoModel()
                {
                    ProdutoId = x.ProdutoId,
                    Nome = x.Nome,
                    Descricao = x.Descricao,
                    Marca = x.Marca,
                    SKU = x.SKU,
                    QtdEstoque = x.QtdEstoque,
                    Preco = x.Preco,
                    PrecoPromocao = x.PrecoPromocao,
                    Imagens = imagens.Select(y => new ProdutoImagemModel()
                    {
                        ProdutoImagemId = y.ProdutoImagemId,
                        ProdutoId = y.ProdutoImagemId,
                        ImagemUrl = y.ImagemUrl,
                        Descricao = y.Descricao,
                    }).ToList(),
                    Tags = tags.Select(z => new ProdutoTagModel()
                    {
                        ProdutoTagId = z.ProdutoTagId,
                        ProdutoId = z.ProdutoId,
                        Nome = z.Nome,
                        Descricao = z.Descricao,
                    }).ToList(),
                })
                .FirstOrDefaultAsync();

            if (produto == null)
            {
                throw new KicksBadRequestException("Nenhum produto foi encontrado.");
            }

            return produto;
        }
        #endregion

        #region Deletar Produto
        public async Task<ProdutoModel> DeletarProduto(Guid produtoId)
        {
            var produto = await _kicksDataContext.Set<ProdutoEntity>()
                .Where(x => x.ProdutoId == produtoId)
                .FirstOrDefaultAsync();

            if (produto == null)
            {
                throw new KicksBadRequestException("Produto não foi encontrado.");
            }

            var imagens = _kicksDataContext.Set<ProdutoImagemEntity>()
                .Where(x => x.ProdutoId == produtoId)
                .ToList();

            var tags = _kicksDataContext.Set<ProdutoTagEntity>()
                .Where(x => x.ProdutoId == produtoId)
                .ToList();

            _kicksDataContext.Remove(produto);

            foreach ( var item in imagens )
            {
                _kicksDataContext.Remove(item);
            }

            foreach ( var item in tags )
            {
                _kicksDataContext.Remove(item);
            }
            
            _kicksDataContext.SaveChanges();

            return produto;
        }
        #endregion
    }
}
