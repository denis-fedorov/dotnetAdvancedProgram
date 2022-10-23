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
            var cartsCollection = db.GetCollection<Cart>(DbMappings.CartsTableName);

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
            var cartsCollection = db.GetCollection<Cart>(DbMappings.CartsTableName);
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
            var cartsCollection = db.GetCollection<Cart>(DbMappings.CartsTableName);

            return cartsCollection.Exists(cart => cart.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There is an error while finding a cart from DB ('{ConnectionString}')", _connectionString);
            throw new DatabaseException(_connectionString!, e);
        }
    }

    public void Delete(string id)
    {
        NullGuard.ThrowIfNull(id);
        
        try
        {
            using var db = new LiteDatabase(_connectionString);
            var cartsCollection = db.GetCollection<Cart>(DbMappings.CartsTableName);
            cartsCollection.Delete(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There is an error while deleting a cart from DB ('{ConnectionString}')", _connectionString);
            throw new DatabaseException(_connectionString!, e);
        }
    }

    public Item? GetItem(string cartId, string itemId)
    {
        NullGuard.ThrowIfNull(cartId);
        NullGuard.ThrowIfNull(itemId);
        
        try
        {
            using var db = new LiteDatabase(_connectionString);
            var cartsCollection = db.GetCollection<Cart>(DbMappings.CartsTableName);

            var cart = cartsCollection
                .Include(c => c.Items)
                .FindById(cartId);
            var item = cart?.Items.SingleOrDefault(i => i.Id == itemId);
            
            return item;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There is an error while getting an item from DB ('{ConnectionString}')", _connectionString);
            throw new DatabaseException(_connectionString!, e);
        }
    }

    public void CreateItem(string cartId, Item item)
    {
        NullGuard.ThrowIfNull(cartId);
        NullGuard.ThrowIfNull(item);
        
        try
        {
            using var db = new LiteDatabase(_connectionString);
            var cartsCollection = db.GetCollection<Cart>(DbMappings.CartsTableName);

            var cart = cartsCollection
                .Include(c => c.Items)
                .FindById(cartId);
            
            cart.AddItem(item);

            cartsCollection.Update(cart);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There is an error while creating an item from DB ('{ConnectionString}')", _connectionString);
            throw new DatabaseException(_connectionString!, e);
        }
    }

    public void DeleteItem(string cartId, string itemId)
    {
        NullGuard.ThrowIfNull(cartId);
        NullGuard.ThrowIfNull(itemId);
        
        try
        {
            using var db = new LiteDatabase(_connectionString);

            var itemsCollection = db.GetCollection<Item>(DbMappings.ItemsTableName);
            itemsCollection.Delete(itemId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There is an error while deleting an cart from DB ('{ConnectionString}')", _connectionString);
            throw new DatabaseException(_connectionString!, e);
        }
    }
}