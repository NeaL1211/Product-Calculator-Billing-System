using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;

namespace ProductCalculatorApp
{
    public static class DataStore
    {
        private static readonly string DataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "ProductCalculatorApp", "data.json");


        private static AppData _cache = new();

        static DataStore()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(DataPath)!);
            }
            catch { /* 忽略 */ }
            Load();
            // 讀檔後統一修補：補客戶 Id、修舊訂單
            EnsureCustomerIds();
            FixMissingCustomerIds();
            SaveAll(); // 若有修補才會寫檔（內部邏輯會避開不必要的 I/O 也可以）
        }

        public static List<Product> Products => _cache.Products;
        public static List<SizeRule> Sizes => _cache.Sizes;
        public static List<Order> Orders => _cache.Orders;
        public static List<Customer> Customers => _cache.Customers;

        // === 客戶查詢 ===
        public static Customer FindCustomerById(string id) =>
            _cache.Customers.FirstOrDefault(c =>
                string.Equals(c.Id?.Trim(), id?.Trim(), StringComparison.OrdinalIgnoreCase));

        public static Customer FindCustomerByName(string name) =>
            _cache.Customers.FirstOrDefault(c =>
                string.Equals(c.Name?.Trim(), name?.Trim(), StringComparison.OrdinalIgnoreCase));

        // === 給新客戶用：產生唯一的字串 Id（遞增數字字串） ===
        public static string NewCustomerId()
        {
            int max = _cache.Customers
                .Select(c => { int n; return int.TryParse(c.Id, out n) ? n : 0; })
                .DefaultIfEmpty(0)
                .Max();
            return (max + 1).ToString();
        }

        // === 存整包客戶 ===
        public static void ReplaceCustomers(List<Customer> list)
        {
            _cache.Customers = list ?? new List<Customer>();
            // 補任何沒有 Id 的客戶
            EnsureCustomerIds();
            // 修補舊訂單（CustomerId 為空但有 CustomerName 的）
            FixMissingCustomerIds();
            SaveAll();
        }

        // === 產生單號：同一客戶 & 同一天序號累加 ===
        public static string NextOrderNo(string customerId, DateTime date)
        {
            string cid = customerId?.Trim() ?? "";
            var sameDay = _cache.Orders
                .Where(o => string.Equals(o.CustomerId?.Trim(), cid, StringComparison.OrdinalIgnoreCase)
                         && o.CreatedAt.Date == date.Date)
                .Select(o => o.OrderNo)
                .Select(no =>
                {
                    if (string.IsNullOrWhiteSpace(no)) return 0;
                    var parts = no.Split('-');
                    return int.TryParse(parts.LastOrDefault(), out var n) ? n : 0;
                });

            var next = sameDay.Any() ? sameDay.Max() + 1 : 1;
            return $"{date:yyyyMMdd}-{next:000}";
        }

        public static void AddOrder(Order order)
        {
            _cache.Orders.Add(order);
            SaveAll();
        }

        public static void Load()
        {
            if (File.Exists(DataPath))
            {
                try
                {
                    var json = File.ReadAllText(DataPath, Encoding.UTF8);
                    _cache = JsonSerializer.Deserialize<AppData>(json) ?? new AppData();
                }
                catch { _cache = new AppData(); }

                _cache.Products ??= new List<Product>();
                _cache.Sizes ??= new List<SizeRule>();
                _cache.Orders ??= new List<Order>();
                _cache.Customers ??= new List<Customer>();

                foreach (var p in _cache.Products)
                {
                    if (!Enum.IsDefined(typeof(ProductKind), p.Kind))
                        p.Kind = ProductKind.Yard;
                }
            }
            else
            {
                // 首次啟動給範例資料（★ 預設就給 Id）
                _cache = new AppData
                {
                    Products = new List<Product>
                    {
                        new Product{ Code="A001", Name="蘋果", BasePrice=30 },
                        new Product{ Code="A002", Name="香蕉", BasePrice=20 }
                    },
                    Sizes = new List<SizeRule>
                    {
                        new SizeRule{ Label="小", Multiplier=1.0 },
                        new SizeRule{ Label="中", Multiplier=1.25 },
                        new SizeRule{ Label="大", Multiplier=1.5 }
                    },
                    Customers = new List<Customer>
                    {
                        new Customer{ Id="1", Name="王小明", Phone="0912-345-678" },
                        new Customer{ Id="2", Name="綠園商行", Phone="02-1234-5678" }
                    },
                    Orders = new List<Order>()
                };
                SaveAll();
            }
        }

        public static void SaveAll()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(DataPath)!);
                // 先寫到暫存檔，再原子替換
                string tmp = DataPath + ".tmp";
                var json = JsonSerializer.Serialize(_cache, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(tmp, json, Encoding.UTF8);

                // 備份舊檔
                if (File.Exists(DataPath))
                {
                    string bak = DataPath + "." + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bak";
                    File.Copy(DataPath, bak, overwrite: false);
                }

                File.Copy(tmp, DataPath, overwrite: true);
                File.Delete(tmp);
            }
            catch
            {
                // 可寫 log
            }
        }


        public static Product? FindProduct(string code) =>
            _cache.Products.FirstOrDefault(p =>
                string.Equals(p.Code?.Trim(), code?.Trim(), StringComparison.OrdinalIgnoreCase));

        public static void UpsertProduct(Product p)
        {
            var existing = FindProduct(p.Code);
            if (existing == null)
                _cache.Products.Add(p);
            else
            {
                existing.Name = p.Name;
                existing.BasePrice = p.BasePrice;
                existing.Kind = p.Kind;
            }
            SaveAll();
        }

        public static void DeleteProduct(string code)
        {
            var p = FindProduct(code);
            if (p != null)
            {
                _cache.Products.Remove(p);
                SaveAll();
            }
        }

        public static void ReplaceSizes(List<SizeRule> sizes)
        {
            _cache.Sizes = sizes ?? new List<SizeRule>();
            SaveAll();
        }

        // === 工具：補所有沒有 Id 的客戶 ===
        private static void EnsureCustomerIds()
        {
            bool changed = false;
            foreach (var c in _cache.Customers)
            {
                if (string.IsNullOrWhiteSpace(c.Id))
                {
                    c.Id = NewCustomerId();
                    changed = true;
                }
            }
            if (changed) SaveAll();
        }

        // === 工具：把 CustomerId 為空的舊訂單，用名稱回填 ===
        private static void FixMissingCustomerIds()
        {
            var map = _cache.Customers
                .Where(c => !string.IsNullOrWhiteSpace(c.Name) && !string.IsNullOrWhiteSpace(c.Id))
                .GroupBy(c => c.Name.Trim())
                .ToDictionary(g => g.Key, g => g.First().Id);

            bool changed = false;
            foreach (var o in _cache.Orders)
            {
                if (string.IsNullOrWhiteSpace(o.CustomerId))
                {
                    var name = o.CustomerName?.Trim();
                    if (!string.IsNullOrEmpty(name) && map.TryGetValue(name, out var id))
                    {
                        o.CustomerId = id;
                        changed = true;
                    }
                }
            }
            if (changed) SaveAll();
        }
        public static void ClearAllOrders()
        {
            _cache.Orders.Clear();
            SaveAll();
        }

    }

}
