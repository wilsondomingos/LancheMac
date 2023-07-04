using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesMac.Migrations
{
    public partial class popularCatergoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome,Descricao)"+ 
                "VALUES('Normal','Lanche feito com ingrediente normais')");

            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome,Descricao)" +
               "VALUES('Natural','Lanche feito com ingrediente natuarais')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
