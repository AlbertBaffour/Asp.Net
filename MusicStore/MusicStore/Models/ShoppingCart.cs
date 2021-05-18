using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Models
{
    public class ShoppingCart
    {
        private StoreContext _context;
        string _cartKey { get; set; }
        public ShoppingCart(HttpContext httpContext, StoreContext context)
        {
            _cartKey = GetCartKey(httpContext);
            _context = context;
        }
        private string GetCartKey(HttpContext httpContext)
        {
            if(httpContext.Session.GetString("CartKey")== null)
            {
                Guid tempCartKey = Guid.NewGuid();
                httpContext.Session.SetString("CartKey", tempCartKey.ToString());
            }
            return httpContext.Session.GetString("CartKey");
        }
        public void AddToCart(Album album)
        {
            var cartItem = _context.CartItems.SingleOrDefault(
                c => c.CartKey == _cartKey && c.AlbumID == album.AlbumID);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    AlbumID = album.AlbumID,
                    CartKey = _cartKey,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
            _context.SaveChanges();
        }
        public void RemoveFromCart(int cartItemId)
        {
            var cartItem = _context.CartItems.SingleOrDefault(
                c => c.CartItemID== cartItemId);
                _context.CartItems.Remove(cartItem);
           
            _context.SaveChanges();
        }
        public void DecreaseItem(int cartItemId)
        {
            var cartItem = _context.CartItems.SingleOrDefault(
                c => c.CartItemID== cartItemId);
            if (cartItem.Count > 1)
            {
                cartItem.Count--;
            }
            else
            {
                _context.CartItems.Remove(cartItem);
            }
            _context.SaveChanges();
        }
        public void IncreaseItem(int cartItemId)
        {
            var cartItem = _context.CartItems.SingleOrDefault(
                c => c.CartItemID== cartItemId);
              cartItem.Count++;
            _context.SaveChanges();
        }
        public void EmptyCart()
        {
            var cartItems = _context.CartItems.Where(
                c => c.CartKey == _cartKey);
           
          foreach(var cartItem in cartItems)
            {
                _context.CartItems.Remove(cartItem);
            }
            _context.SaveChanges();
        }
        public List<CartItem> GetCartItems()
        {
            var cartItems = _context.CartItems
                .Include(c=>c.Album)
                .ThenInclude(c=>c.Artist)
                .Where(c => c.CartKey == _cartKey);
            return cartItems.ToList();
        }
        public int GetCount()
        {
            int? count =(from c in _context.CartItems
                         where c.CartKey==_cartKey
                         select (int?)c.Count).Sum();

            return count ?? 0;
        }
        public void CreateOrderItems(Order order)
        {
            var cartItems = GetCartItems();

            foreach(var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    AlbumID = item.AlbumID,
                    OrderID = order.OrderID,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };
                _context.OrderItems.Add(orderItem);
            }
            _context.SaveChanges();

            EmptyCart();
            return;
        }
    }
}
