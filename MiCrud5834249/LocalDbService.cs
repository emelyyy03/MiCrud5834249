using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MiCrud5834249
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_suma.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));

            //Le indica al sistema que cree la tabla de nuestro contexto
            _connection.CreateTableAsync<Distancia>();
        }

        //Metodo para listar los registros de nuestra tabla
        public async Task<List<Distancia>> GetDistancia()
        {
            return await _connection.Table<Distancia>().ToListAsync();
        }

        //Metodo para listar los registros por id
        public async Task<Distancia> GetById(int id)
        {
            return await _connection.Table<Distancia>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //Metodo para crear registro 
        public async Task Create(Distancia distancia)
        {
            await _connection.InsertAsync(distancia);
        }

        //Metodo para actualizar 
        public async Task Update(Distancia distancia)
        {
            await _connection.UpdateAsync(distancia);
        }

        //Metodo para eliminar
        public async Task Delete(Distancia distancia)
        {
            await _connection.DeleteAsync(distancia);
        }
    }
}
