using api_pos_pizza.Data;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace api_pos_pizza.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DBAPIContext _context;
        private readonly string _connection;
        public CategoriaRepository(DBAPIContext context, IConfiguration configuration)
        {
            _context = context;
            _connection = configuration.GetConnectionString("cadenaSQL");
        }

        public async Task<IEnumerable<Categorium>> GetAll()
        {
            //return await _context.Categoria.ToListAsync();

            using var connection = new SqlConnection(_connection);

            var sql = "select * from Categoria";

            var cate = await connection.QueryAsync<Categorium>(sql);

            return cate;
        }

        public async Task<Categorium?> GetById(int id)
        {
            //return await _context.Categoria.FindAsync(id);

            using var connection = new SqlConnection(_connection);

            var sql = @"select * from Categoria where Id = @Id";

            var cate = await connection.QueryFirstOrDefaultAsync<Categorium>(sql, new { Id = id });

            return cate;
        }

        public async Task<Categorium> Create(Categorium categoria)
        {
            /*_context.Categoria.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;*/

            using var connection = new SqlConnection(_connection);

            var sql = @"insert into Categoria (Descripcion, Estado) 
                        OUTPUT INSERTED.* 
                        values (@Descripcion, @Estado)";

            var cate = await connection.QuerySingleAsync<Categorium>(sql, categoria);

            return cate;
        }

        public async Task<Categorium?> Update(int id, Categorium categoria)
        {
            /*var existingCategoria = await _context.Categoria.FindAsync(id);

            if (existingCategoria == null)
                return null;

            existingCategoria.Descripcion = categoria.Descripcion ?? existingCategoria.Descripcion;
            existingCategoria.Estado = categoria.Estado ?? existingCategoria.Estado;

            await _context.SaveChangesAsync();
            return existingCategoria;*/

            using var connection = new SqlConnection(_connection);

            var sql = @"UPDATE Categoria SET Descripcion = @Descripcion, Estado = @Estado
                        OUTPUT INSERTED.*
                        WHERE Id = @Id";

            var parameters = new
            {
                id,
                categoria.Descripcion,
                categoria.Estado
            };

            var cate = await connection.QuerySingleOrDefaultAsync<Categorium>(sql, parameters);

            return cate;
        }

        public async Task<bool> Delete(int id)
        {
            /*var categoria = await _context.Categoria.FindAsync(id);

            if (categoria == null)
                return false;

            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;*/

            using var connection = new SqlConnection(_connection);

            var sql = @"IF EXISTS (SELECT 1 FROM Categoria WHERE Id = @Id)
                        BEGIN
                            DELETE FROM Categoria WHERE Id = @Id
                            SELECT 1
                        END
                        ELSE
                            SELECT 0";

            var result = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });
            return result == 1;
        }

        public async Task<DataTable> GetCategoriasDataTableAsync()
        {
            DataTable dtCategorias = new DataTable();

            dtCategorias.Columns.Add("Id", typeof(int));
            dtCategorias.Columns.Add("Nombre", typeof(string));
            dtCategorias.Columns.Add("Estado", typeof(bool));

            using (var connection = new SqlConnection(_connection))
            {
                try
                {
                    await connection.OpenAsync();

                    string query = @"SELECT Id, Descripcion, Estado FROM Categoria";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dtTemp = new DataTable();
                            await Task.Run(() => adapter.Fill(dtTemp));

                            foreach (DataRow dr in dtTemp.Rows)
                            {
                                DataRow newRow = dtCategorias.NewRow();
                                newRow["Id"] = dr["Id"];
                                newRow["Nombre"] = dr["Descripcion"];
                                newRow["Estado"] = dr["Estado"];

                                dtCategorias.Rows.Add(newRow);
                            }

                            dtCategorias.AcceptChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            /*
            using (var connection = new SqlConnection(_connection))
            {
                try
                {
                    string query = "SELECT * FROM Categoria";

                    DataTable dtTemp = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    adapter.SelectCommand.CommandType = CommandType.Text;
                    await Task.Run(() => adapter.Fill(dtTemp));

                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        DataRow drNew = dtCategorias.NewRow();
                        drNew["Id"] = dr["Id"];
                        drNew["Nombre"] = dr["Descripcion"];
                        drNew["Estado"] = dr["Estado"];
                        dtCategorias.Rows.Add(drNew);
                    }
                    dtCategorias.AcceptChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }*/

            return dtCategorias;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Categoria.AnyAsync(c => c.Id == id);
        }
    }
}
