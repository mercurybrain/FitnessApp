using FitnessApp.Models;
using SQLite;

namespace FitnessApp.Services
{
    public class LocalDBService : IDisposable
    {
        private const string DB_NAME = "fitness.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDBService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<User>();
            _connection.CreateTableAsync<BodyMetrics>();
            _connection.CreateTableAsync<Tracker>();
            _connection.CreateTableAsync<Exercise>();
            _connection.CreateTableAsync<ExerciseData>();
        }

        public async Task DropAll() { 
            await _connection.DropTableAsync<User>();
            await _connection.DropTableAsync<BodyMetrics>();
            await _connection.DropTableAsync<Tracker>();
            await _connection.DropTableAsync<Exercise>();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _connection.Table<User>().ToListAsync();
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _connection.Table<User>().Where(x => x.Username == username).FirstOrDefaultAsync();
        }
        public async Task<bool> VerifyPass(string username, string pass)
        {
            User user = await _connection.Table<User>().Where(x => x.Username == username).FirstOrDefaultAsync();
            if (user is not null) return Encoder.VerifyHash(pass, "SHA-512", user.PasswordHash);
            return false;
        }

        public async Task CreateUser(User user)
        {
            user.PasswordHash = Encoder.ComputeHash(user.PasswordHash, "SHA-512", null);
            await _connection.InsertAsync(user);
        }
        public async Task UpdateUser(User user)
        {
            await _connection.UpdateAsync(user);
        }
        public async Task DeleteUser(User user)
        {
            await _connection.DeleteAsync(user);
        }

        public async Task<BodyMetrics> GetBodyMetrics(string username) {
            return await _connection.Table<BodyMetrics>().Where(x => x.Username == username).FirstOrDefaultAsync();
        }

        public async Task CreateMetrics(BodyMetrics metrics) {
            await _connection.InsertAsync(metrics);
        }

        public async Task UpdateMetrics(BodyMetrics metrics) {
            await _connection.UpdateAsync(metrics);
        }

        public async Task<List<Tracker>> GetHistory(string username, int N)
        {
            var query = _connection.Table<Tracker>()
                .Where(x => x.Username == username)
                .OrderByDescending(x => x.DateTrack);

            var trackers = await query.ToListAsync(); // Используем IQueryable

            return trackers.TakeLast(N).ToList(); // Используем TakeLast
        }

        public async Task<Tracker> GetTracker(string username) {
            return await _connection.Table<Tracker>().Where(x => x.Username == username).FirstOrDefaultAsync();
        }

        public async Task InsertTrack(Tracker tracker) {
            await _connection.InsertAsync(tracker);
        }

        public async Task UpdateTracker(Tracker tracker) { 
            await _connection.UpdateAsync(tracker);
        }

        public async Task<List<ExerciseData>> GetExercisesData() {
           return await _connection.Table<ExerciseData>().ToListAsync();
        }

        public async Task InsertExerciseData(ExerciseData data) {
            await _connection.InsertAsync(data);
        }

        public async Task DropExercisesData() {
            await _connection.DropTableAsync<ExerciseData>();
        }

        public async Task<Exercise> GetExercise(string username)
        {
            return await _connection.Table<Exercise>().Where(x => x.Username == username).FirstOrDefaultAsync();
        }

        public async Task InsertExercise(Exercise exercise)
        {
            await _connection.InsertAsync(exercise);
        }

        public async Task<List<Exercise>> GetExercises(string username)
        {
            return await _connection.Table<Exercise>().Where(x => x.Username == username).ToListAsync();
        }

        public void Dispose() { 
            _connection.CloseAsync();
        }
    }
}
