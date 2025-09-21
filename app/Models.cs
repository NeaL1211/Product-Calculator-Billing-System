using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProductCalculatorApp
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Mobile { get; set; } = "";   // ★ 新增：行動電話
        public string Address { get; set; } = "";  // ★ 新增：地址
        public bool Active { get; set; }
    }

    public enum ProductKind
    {
        Yard = 0,   // 碼數商品（單位：碼）
        Roller = 1,  // 捲簾商品（單位：才；數量 = 長 × 寬）
        Labor = 2    // 車工(寬高)
    }

    public class Product
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public double BasePrice { get; set; }

        // ★ 新增：樣品型態（預設「碼數」）
        public ProductKind Kind { get; set; } = ProductKind.Yard;

        // 顯示用（不寫入 JSON）
        [JsonIgnore]
        public string UnitLabel => Kind == ProductKind.Roller ? "才" : "碼";
    }

    public class SizeRule
    {
        public string Label { get; set; } = "";
        public double Multiplier { get; set; }
    }

    public class AppData
    {
        public List<Product> Products { get; set; } = new();
        public List<SizeRule> Sizes { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public List<Customer> Customers { get; set; } = new();
    }

    public class OrderLine
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public double UnitPrice { get; set; }

        // 你原本就有的欄位
        public double Length { get; set; }    // 留存寬/高等
        public double Width { get; set; }
        public string Note { get; set; } = "";

        // ★ 新增
        public double Wage { get; set; }      // 車工金額
        public double Subtotal { get; set; }  // 商品小計（碼數×單價）

        // （可選，之後若要顯示數量/單位）
        public double Quantity { get; set; }  // 商品=碼數；車工=件數
        public string Unit { get; set; } = ""; // 商品="碼"；車工=""


        [JsonIgnore]
        // 只用來顯示在表格裡
        public string DisplaySpec
        {
            get
            {
                if (Unit == "才") // 捲簾
                    return (Length > 0 || Width > 0) ? $"{Length:0.##}*{Width:0.##}" : "";
                // 其他 (窗簾等) → 寬*高
                return (Width > 0 || Length > 0) ? $"{Width:0.##}*{Length:0.##}" : "";
            }
        }
    }


    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CustomerId { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public string OrderNo { get; set; } = "";

        public List<OrderLine> Lines { get; set; } = new();

        // ★ 新增（可選）
        public double TotalSubtotal { get; set; } // 小計合計
        public double TotalWage { get; set; } // 工資合計
        public double Total { get; set; } // 總金額 = 上面相加

        public int LineCount => Lines?.Count ?? 0;
    }



}
