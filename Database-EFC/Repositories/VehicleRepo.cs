using System;
using System.Text.Json;
using System.Threading.Tasks;
using Database_EFC.Persistence;
using Entity.ModelData;
using Logger.Log;
using Microsoft.EntityFrameworkCore;

namespace Database_EFC.Repositories
{
    public class VehicleRepo : IVehicleRepo
    {
        private readonly CarSharingDbContext _dbContext;

        public VehicleRepo(CarSharingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Vehicle> AddAsync(Vehicle vehicle)
        {
            Log.AddLog($"|Repositories/VehicleRepo.AddAsync| : Request : {JsonSerializer.Serialize(vehicle)}");
            var added = await _dbContext.Vehicles.AddAsync(vehicle);
            await _dbContext.SaveChangesAsync();
            return added.Entity;
        }

        public async Task<Vehicle> GetAsync(string licenseNo)
        {
            try
            {
                Log.AddLog($"|Repositories/VehicleRepo.GetAsync| : Request :  LicenseNo:{licenseNo}");
                Vehicle vehicle = await _dbContext.Vehicles.FirstAsync(vehicle => vehicle.LicenseNo == licenseNo);
                return vehicle;
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/VehicleRepo.GetAsync| : Error : {e.Message}");
                throw new Exception($"Did not find the vehicle with license number of {licenseNo}");
            }
        }

        public async Task<Vehicle> UpdateAsync(Vehicle vehicle)
        {
            try
            {
                _dbContext.Update(vehicle);
                await _dbContext.SaveChangesAsync();
                Log.AddLog($"|Repositories/VehicleRepo.UpdateAsync| : Reply : {JsonSerializer.Serialize(vehicle)}");
                return vehicle;
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/VehicleRepo.UpdateAsync| : Error : {e.Message}");
                throw new Exception($"Did not find vehicle with licenseNo #{vehicle.LicenseNo}");
            }
        }
        

        public async Task<bool> RemoveAsync(string licenseNo)
        {
            Log.AddLog($"|Repositories/VehicleRepo.RemoveAsync| : Request : LicenseNo:{licenseNo}");
            var toRemove = await _dbContext.Vehicles.FirstOrDefaultAsync(v => v.LicenseNo == licenseNo);
            if (toRemove == null) return false;
            
            _dbContext.Vehicles.Remove(toRemove);
            await _dbContext.SaveChangesAsync();
            return true;

        }
    }
}