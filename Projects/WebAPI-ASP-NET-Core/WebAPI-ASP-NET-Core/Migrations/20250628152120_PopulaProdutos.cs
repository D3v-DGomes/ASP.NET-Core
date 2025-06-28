using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_ASP_NET_Core.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)"
                + "Values('Coca-Cola Diet','Refrigerante de Cola 350ml',5.45,'cocacoladiet.jpg',50,now(),1)");

            migrationBuilder.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)"
                + "Values('Bic Mac','Hamburguer altamente calórico',29.99,'bigmac.jpg',20,now(),2)");

            migrationBuilder.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)"
                + "Values('Açaí','Polpa de açaí batida com banana',18.90,'acaicbanana.jpg',23,now(),3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Produtos");
        }
    }
}
