using CartingService.Core.Entities;
using CartingService.Core.Interfaces;
using CartingService.Infrastructure.Exceptions;
using CartingService.Infrastructure.Settings;
using CartingService.SharedKernel;
using LiteDB;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CartingService.Infrastructure;

public class CartingRepository : ICartingRepository
{
    private const string CartsTableName = "carts";
    
    private readonly ILogger<CartingRepository> _logger;
    private readonly string? _connectionString;

    public CartingRepository(
        ILogger<CartingRepository> logger,
        IOptions<ConnectionSettings> connectionSettings)
    {
        _logger = logger;
        
        NullGuard.ThrowIfNull(connectionSettings);
        var connectionString = connectionSettings.Value.ConnectionString;
        _connectionString = NullGuard.ThrowIfNull(connectionString);
    }
    
    public Cart? Get(string id)
    {
        NullGuard.ThrowIfNull(id);
        
        try
        {
            using var db = new LiteDatabase(_connectionString);
            var cartsCollection = db.GetCollection<Cart>(CartsTableName);

            var cart = cartsCollection
                .Include(c => c.Items)
                .FindById(id);
            
            return cart;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There is an error while getting a cart from DB ('{ConnectionString}')", _connectionString);
            throw new DatabaseException(_connectionString!, e);
        }
    }

    public void Create(Cart cart)
    {
        NullGuard.ThrowIfNull(cart);
        
        try
        {
            using var db = new LiteDatabase(_connectionString);
            var cartsCollection = db.GetCollection<Cart>(CartsTableName);
            cartsCollection.Insert(cart);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There is an error while saving a cart in DB ('{ConnectionString}')", _connectionString);
            throw new DatabaseException(_connectionString!, e);
        }
    }

    public bool Exists(string id)
    {
        NullGuard.ThrowIfNull(id);
        
        try
        {
            using var db = new LiteDatabase(_connectionString);
            var cartsCollection = db.GetCollection<Cart>(CartsTableName);

            return cartsCollection.Exists(cart => cart.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There is an error while finding a cart from DB ('{ConnectionString}')", _connectionString);
            throw new DatabaseException(_connectionString!, e);
        }
    }

    public void Update(Cart cart)
    {
        NullGuard.ThrowIfNull(cart);
        
        try
        {
            using var db = new LiteDatabase(_connectionString);
            var cartsCollection = db.GetCollection<Cart>(CartsTableName);
            cartsCollection.Update(cart);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There is an error while updating a cart from DB ('{ConnectionString}')", _connectionString);
            throw new DatabaseException(_connectionString!, e);
        }
    }

    public void UpdateItemPrice(string itemId, decimal price)
    {
        NullGuard.ThrowIfNull(itemId);
        
        try
        {
            // a small hack due to DB persistent feature 
            using var db = new LiteDatabase(_connectionString);
            var cartsCollection = db
                .GetCollection<Cart>(CartsTableName)
                .Include(c => c.Items)
                .Find(c => c.Items.Select(i => i.Name).Any(name => name == itemId))
                .ToList();
                
            foreach (var cart in cartsCollection)
            {
                cart.UpdateItemPrice(itemId, price);
            }

            db.GetCollection<Cart>(CartsTableName)
                .Update(cartsCollection);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There is an error while updating an item from DB ('{ConnectionString}')", _connectionString);
            throw new DatabaseException(_connectionString!, e);
        }
    }
}